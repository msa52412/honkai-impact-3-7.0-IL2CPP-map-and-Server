using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class ClientData
{
	public static readonly SqlTable<ClientDataScheme> collection = Global.Table<ClientDataScheme>("ClientDatas", ("owner_uid", "INTEGER", x => x.OwnerUid, false), ("type", "INTEGER", x => x.Type, false), ("client_data_id", "INTEGER", x => x.ClientDataId, false));

	public static ClientDataScheme? GetClientData(uint uid, ClientDataType type, uint id)
	{
		return (from c in collection.All()
			where c.OwnerUid == uid && (int)c.Type == (int)type && c.ClientDataId == id
			select c).FirstOrDefault();
	}

	public static void SetClientData(uint uid, ClientDataType type, uint id, byte[] data)
	{
		// 原来是 Mongo 的 upsert：存在就更新，不存在就插入。SQLite 这边直接表达成同一件事。
		ClientDataScheme row = GetClientData(uid, type, id);
		if (row == null)
		{
			collection.InsertOne(new ClientDataScheme
			{
				OwnerUid = uid,
				Type = type,
				ClientDataId = id,
				Data = data
			});
			return;
		}
		row.Type = type;
		row.ClientDataId = id;
		row.Data = data;
		collection.ReplaceOne(row);
	}
}
