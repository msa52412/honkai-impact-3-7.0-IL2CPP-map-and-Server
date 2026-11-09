using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KickChatgroupMemberRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2213
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_CREATER")]
		NotCreater,
		[ProtoEnum(Name = "KICK_SELF")]
		KickSelf,
		[ProtoEnum(Name = "NOT_MEMBER")]
		NotMember
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	private uint? __pbn__TargetUid;

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

	[ProtoMember(3, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
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

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}
}
