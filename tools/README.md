# tools\ — 协议定义工具

改 `common\` 里的协议字段之前，先在这里查清楚「这个字段号在官方定义里是什么」。
零第三方依赖，纯 Python 标准库。

## 文件

| 文件 | 说明 |
|---|---|
| `SeaRelWin8.4.0.proto` | **权威协议定义**，1.16 MB / 48,439 行 / 4320 message / 13,821 字段 / 2,206 enum。从 `千乐铃音_7.0\KianaBH\KianaProto.dll` 内嵌的 `FileDescriptorProto` 还原 |
| `kiana.desc` | 991 KB 原始 `FileDescriptorProto` 二进制（上面那份 `.proto` 的输入） |
| `desc2proto.py` | `FileDescriptorProto` 二进制 → `.proto` 文本。手写 wire 解析，7 KB |
| `compare_proto.py` | TSV（`--dump-proto` 导出）vs descriptor，输出 5 类差异 |
| `out\` | 导出结果放这里 |

## 用法

### 查某个字段号是什么

```bat
findstr /n "total_login_days" tools\SeaRelWin8.4.0.proto
```

或者找某个 message 定义：

```bat
findstr /n "message GetMainDataRsp" tools\SeaRelWin8.4.0.proto
```

### 导出现有 Common.dll 的全部定义

```bat
run\ExtHost.exe --dump-proto tools\out\now.tsv
```

TSV 两种行：

```
M<TAB>类型<TAB>tag<TAB>proto名<TAB>proto类型<TAB>label<TAB>packed<TAB>cs类型
E<TAB>枚举<TAB>值<TAB>成员名
```

### 和权威定义比差异

```bat
python tools\compare_proto.py tools\kiana.desc tools\out\now.tsv
```

输出 5 类：同 tag 不同名 / 同 tag 不同类型 / 不同 label / 缺字段 / 多字段，
外加枚举同名成员值不同。**注意**：比对的是 7.0 的 `Common.dll` 与 8.4.0 的官方定义，
版本位移导致的差异是正常的；真正要担心的是「同 tag 不同 wire type」。

实测结果：2796 个同名 message，20+12+8 处差异全部是版本位移或命名风格，**0 处 wire type 冲突**。

### 从 KianaProto.dll 重新还原 proto（一般不需要）

`kiana.desc`（991 KB 的 `FileDescriptorProto`）是从 `KianaProto.dll` 里那段
`SeaRelWin840Reflection` 的 22,562 段 base64 分片拼起来解码得到的。有了 `.desc` 之后：

```bat
python tools\desc2proto.py tools\kiana.desc tools\out\out.proto
python tools\desc2proto.py tools\kiana.desc --stats      :: 只看统计不写文件
```

## 为什么需要这个

`Common.dll` 是 protobuf-net 生成的，字段名和字段号都编进程序集了，但：
- 反编译出来的 `[ProtoMember(39, Name = "total_login_days")]` 只告诉你**它是什么**，
  不告诉你**官方本来该是什么**
- 加新字段时要挑一个没被占用的号，得先知道现有号段
- 怀疑某个 handler 返回错，先确认字段号有没有对错位

权威 proto 就是这份「本来该是什么」的答案。
