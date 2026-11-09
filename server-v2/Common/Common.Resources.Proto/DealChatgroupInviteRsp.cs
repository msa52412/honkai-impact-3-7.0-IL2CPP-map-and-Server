using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DealChatgroupInviteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2215
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "INVITE_NOT_EXIST")]
		InviteNotExist,
		[ProtoEnum(Name = "ALREADY_IN")]
		AlreadyIn,
		[ProtoEnum(Name = "JOIN_MAX")]
		JoinMax,
		[ProtoEnum(Name = "CHATGROUP_FULL")]
		ChatgroupFull
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
