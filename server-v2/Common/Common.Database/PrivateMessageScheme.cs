using Common.Resources.Proto;
using Newtonsoft.Json;

namespace Common.Database;

public class PrivateMessageScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint TargetUid { get; set; }

	public uint SenderUid { get; set; }

	/// <summary>
	/// 真正落库的是 SerializedMsg（字符串），Msg 只是它的强类型视图。
	/// 原来用 [BsonIgnore] 把它排除在 Mongo 文档外，这里换成 [JsonIgnore] —— 等价。
	/// </summary>
	[JsonIgnore]
	public ChatMsg Msg
	{
		get
		{
			return JsonConvert.DeserializeObject<ChatMsg>(SerializedMsg) ?? new ChatMsg();
		}
		set
		{
			SerializedMsg = JsonConvert.SerializeObject(value);
		}
	}

	public string SerializedMsg { get; set; }

	public long TimeSent { get; set; }
}
