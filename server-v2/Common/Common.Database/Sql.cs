using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace Common.Database;

/// <summary>
/// 所有落库对象都要有行号（SQLite 的 id 主键）。
/// 原来 Mongo 版的 ObjectId 就扮演这个角色，但它把「存储引擎」泄露到了业务类型里；
/// 换成 long 之后 scheme 只依赖 IRow 这个自己的抽象。
/// </summary>
public interface IRow
{
    long Id { get; set; }

    /// <summary>行创建时间（Unix 秒）。原来靠 Mongo ObjectId 里嵌的时间戳，现在自己存。</summary>
    long CreatedAt { get; set; }
}

/// <summary>
/// SQLite 存储引擎。一个进程一个实例，内部加锁串行化写入（SQLite 单写者模型）。
///
/// 设计取舍：
///   * 表结构 = id + 若干「业务键列」+ payload(JSON)。
///     键列只用来做唯一约束和人工排查，真正的过滤仍然走内存 LINQ ——
///     这份存档只有 573 KB / 415 条，内存过滤比拼 SQL 更快也更不容易出错。
///   * payload 用 JSON 而不是 protobuf：可以直接用任意 SQLite 工具打开看玩家数据，
///     而且绕开了「派生类 + protobuf 继承」的坑。
///   * 写入全部包在事务里 —— 这正是 Mongo standalone 给不了的东西。
/// </summary>
public sealed class SqliteStore
{
    private readonly object _gate = new();
    private readonly SqliteConnection _conn;
    private readonly Dictionary<string, object> _tables = new();
    private readonly HashSet<string> _created = new();
    // 事务进行中时，这条连接上所有命令都必须挂同一个事务，否则 Sqlite 会报
    // "ExecuteNonQuery requires the command to have a transaction"
    private SqliteTransaction _tx;

    public string FilePath { get; }

    public SqliteStore(string filePath)
    {
        FilePath = Path.GetFullPath(filePath);
        string dir = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(dir))
        {
            Directory.CreateDirectory(dir);
        }

        _conn = new SqliteConnection(new SqliteConnectionStringBuilder
        {
            DataSource = FilePath,
            Mode = SqliteOpenMode.ReadWriteCreate,
            Cache = SqliteCacheMode.Shared,
        }.ToString());
        _conn.Open();

        Exec("PRAGMA journal_mode=WAL;");
        Exec("PRAGMA synchronous=NORMAL;");
        Exec("PRAGMA foreign_keys=ON;");
        Exec("PRAGMA busy_timeout=5000;");
    }

    private SqliteCommand NewCommand(string sql, (string name, object value)[] args)
    {
        SqliteCommand cmd = _conn.CreateCommand();
        cmd.CommandText = sql;
        if (_tx != null)
        {
            cmd.Transaction = _tx;
        }
        foreach ((string name, object value) in args)
        {
            cmd.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }
        return cmd;
    }

    internal void Exec(string sql, params (string name, object value)[] args)
    {
        lock (_gate)
        {
            using SqliteCommand cmd = NewCommand(sql, args);
            cmd.ExecuteNonQuery();
        }
    }

    internal List<(long id, string payload)> Query(string sql, params (string name, object value)[] args)
    {
        lock (_gate)
        {
            using SqliteCommand cmd = NewCommand(sql, args);
            List<(long, string)> list = new();
            using SqliteDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add((r.GetInt64(0), r.IsDBNull(1) ? null : r.GetString(1)));
            }
            return list;
        }
    }

    internal long Insert(string sql, params (string name, object value)[] args)
    {
        lock (_gate)
        {
            using SqliteCommand cmd = NewCommand(sql + "; SELECT last_insert_rowid();", args);
            object o = cmd.ExecuteScalar();
            return Convert.ToInt64(o);
        }
    }

    /// <summary>一批写操作放在同一个事务里 —— 要么全成，要么全不成。</summary>
    public void InTransaction(Action body)
    {
        lock (_gate)
        {
            if (_tx != null)
            {
                // 已经在事务里（嵌套调用），直接并进去
                body();
                return;
            }
            using SqliteTransaction tx = _conn.BeginTransaction();
            _tx = tx;
            try
            {
                body();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            finally
            {
                _tx = null;
            }
        }
    }

    internal void EnsureTable(string name, string ddl, string indexDdl)
    {
        if (_created.Contains(name))
        {
            return;
        }
        Exec(ddl);
        if (!string.IsNullOrEmpty(indexDdl))
        {
            Exec(indexDdl);
        }
        _created.Add(name);
    }

    internal void Cache(string name, object table)
    {
        lock (_gate)
        {
            _tables[name] = table;
        }
    }

    internal object GetCached(string name)
    {
        lock (_gate)
        {
            return _tables.TryGetValue(name, out object t) ? t : null;
        }
    }

    public long TableRowCount(string table)
    {
        lock (_gate)
        {
            using SqliteCommand cmd = _conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM \"" + table + "\";";
            return Convert.ToInt64(cmd.ExecuteScalar());
        }
    }

    public IEnumerable<string> TableNames()
    {
        return Query("SELECT 0, name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%' ORDER BY name;")
            .Select(x => x.payload);
    }
}

/// <summary>
/// 一张表的强类型视图。业务代码看到的仍是「一个集合」，
/// 只是底下从 Mongo 换成了 SQLite + JSON 行。
/// </summary>
public sealed class SqlTable<T> where T : class, IRow, new()
{
    private readonly SqliteStore _store;
    private readonly string _name;
    private readonly (string column, string type, Func<T, object> get, bool unique)[] _cols;
    private readonly JsonSerializerSettings _json = new()
    {
        NullValueHandling = NullValueHandling.Include,
        // 必须是 Auto（Json.NET 默认）。原来写的是 Replace，会导致**只读集合属性被整段跳过**：
        // 协议类里大量形如 `public List<X> FooLists { get; } = new List<X>();`
        // （Common.Resources.Proto 下有 1013 个），没有 setter，
        // Replace 模式下 Json.NET 想「换一个新实例」但赋不进去，于是直接不读这个字段。
        // 结果就是：写库正常、读库把这些列表全丢了 —— 圣痕词缀
        // （Stigmata.RuneLists / WaitSelectRuneGroupLists）每次重登都消失就是这个原因。
        // 用 Auto：集合有 getter 就往现有实例里 Add，反序列化对象每次都是 new 出来的，
        // 不会重复。已用 15 张表 / 1245 条真实存档做过「反序列化→序列化」往返比对，
        // Auto 下逐字节一致，Replace 下带词缀的那条会丢。
        ObjectCreationHandling = ObjectCreationHandling.Auto,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    public string Name => _name;

    internal SqlTable(SqliteStore store, string name,
                      (string, string, Func<T, object>, bool)[] cols)
    {
        _store = store;
        _name = name;
        _cols = cols;

        List<string> defs = new() { "id INTEGER PRIMARY KEY AUTOINCREMENT", "payload TEXT NOT NULL" };
        List<string> idx = new();
        foreach ((string column, string type, Func<T, object> get, bool unique) in cols)
        {
            defs.Add("\"" + column + "\" " + type);
            if (unique)
            {
                idx.Add("CREATE UNIQUE INDEX IF NOT EXISTS \"ux_" + name + "_" + column + "\" ON \"" + name + "\"(\"" + column + "\");");
            }
            else
            {
                idx.Add("CREATE INDEX IF NOT EXISTS \"ix_" + name + "_" + column + "\" ON \"" + name + "\"(\"" + column + "\");");
            }
        }
        _store.EnsureTable(name,
            "CREATE TABLE IF NOT EXISTS \"" + name + "\" (" + string.Join(", ", defs) + ");",
            string.Join(" ", idx));
    }

    private string Serialize(T item) => JsonConvert.SerializeObject(item, _json);

    /// <summary>
    /// v1 用 MongoDB 的 C# 驱动，它有一条默认约定：属性名 <c>Id</c> 自动映射到元素名 <c>_id</c>。
    /// 所以 Mongo 存档里武器 / 圣痕 / 材料 / 宿舍房间的「物品 ID」都叫 <c>_id</c>：
    /// <code>{"UniqueId": 101, "_id": 20001, "Level": 1, ...}</code>
    /// v2 换成 Newtonsoft + JSON，没有这层约定，<c>_id</c> 会被当成未知字段丢掉，
    /// 于是 <c>Id</c> 变成 0 —— 客户端拿不到物品 ID，武器/圣痕/材料/家具全部显示不出来。
    ///
    /// 迁移脚本（_launch\migrate_v1_to_v2.py）已经在写库时把 <c>_id</c> 改名成 <c>Id</c>，
    /// 这里再兜一层：只要 payload 里还有 <c>"_id"</c>，读的时候就认成 <c>Id</c>。
    /// 只对下面这两张表生效 —— 它们的 payload 全是结构化数据，不含玩家自由文本，
    /// 做字符串替换没有副作用。
    /// </summary>
    private static readonly HashSet<string> MongoIdTables = new() { "Equipments", "Dorms" };

    private static readonly Regex MongoIdKey = new("\"_id\"\\s*:", RegexOptions.Compiled);

    private T Deserialize(long id, string payload)
    {
        if (payload != null && MongoIdTables.Contains(_name) && payload.Contains("\"_id\""))
        {
            payload = MongoIdKey.Replace(payload, "\"Id\":");
        }
        T item = JsonConvert.DeserializeObject<T>(payload ?? "{}", _json) ?? new T();
        item.Id = id;
        return item;
    }

    /// <summary>整表读出来（数据量小，直接内存里过滤）。按 id 排序以保持插入顺序。</summary>
    public List<T> All()
    {
        return _store.Query("SELECT id, payload FROM \"" + _name + "\" ORDER BY id;")
            .Select(r => Deserialize(r.id, r.payload))
            .ToList();
    }

    public List<T> Where(Func<T, bool> predicate) => All().Where(predicate).ToList();

    public T FirstOrDefault(Func<T, bool> predicate) => All().FirstOrDefault(predicate);

    public long Count() => _store.TableRowCount(_name);

    public void InsertOne(T item)
    {
        if (item.CreatedAt <= 0)
        {
            item.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }
        List<(string, object)> args = new() { ("$payload", Serialize(item)) };
        List<string> names = new() { "payload" };
        foreach ((string column, string _, Func<T, object> get, bool _) in _cols)
        {
            names.Add("\"" + column + "\"");
            args.Add(("$" + column, get(item)));
        }
        string sql = "INSERT INTO \"" + _name + "\" (" + string.Join(", ", names) + ") VALUES ("
                     + string.Join(", ", names.Select(n => "$" + n.Trim('"'))) + ");";
        item.Id = _store.Insert(sql, args.ToArray());
    }

    public void InsertMany(IEnumerable<T> items)
    {
        _store.InTransaction(() =>
        {
            foreach (T item in items)
            {
                InsertOne(item);
            }
        });
    }

    public void ReplaceOne(T item)
    {
        if (item.Id <= 0)
        {
            InsertOne(item);
            return;
        }
        List<(string, object)> args = new() { ("$id", item.Id), ("$payload", Serialize(item)) };
        List<string> sets = new() { "payload = $payload" };
        foreach ((string column, string _, Func<T, object> get, bool _) in _cols)
        {
            sets.Add("\"" + column + "\" = $" + column);
            args.Add(("$" + column, get(item)));
        }
        _store.Exec("UPDATE \"" + _name + "\" SET " + string.Join(", ", sets) + " WHERE id = $id;", args.ToArray());
    }

    public void BulkWrite(IEnumerable<T> items)
    {
        _store.InTransaction(() =>
        {
            foreach (T item in items)
            {
                ReplaceOne(item);
            }
        });
    }

    public void DeleteOne(T item)
    {
        if (item.Id > 0)
        {
            _store.Exec("DELETE FROM \"" + _name + "\" WHERE id = $id;", ("$id", item.Id));
        }
    }

    public void DeleteAll() => _store.Exec("DELETE FROM \"" + _name + "\";");
}
