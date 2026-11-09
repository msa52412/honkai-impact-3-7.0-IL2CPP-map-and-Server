using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LeaveChatgroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2209
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_MEMBER")]
		NotMember
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatgroup_id")]
	public uint ChatgroupId
	{
		get
		{
			return __pbn__ChatgroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}
}
