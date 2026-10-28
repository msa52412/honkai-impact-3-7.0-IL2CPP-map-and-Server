using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteToMultiChatgroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2217
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "JOIN_MAX_NUM")]
		JoinMaxNum,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private bool? __pbn__IsStranger;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
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

	[ProtoMember(3, Name = "chatgroup_id_list")]
	public uint[] ChatgroupIdLists { get; set; }

	[ProtoMember(4, Name = "is_stranger")]
	public bool IsStranger
	{
		get
		{
			return __pbn__IsStranger.GetValueOrDefault();
		}
		set
		{
			__pbn__IsStranger = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeIsStranger()
	{
		return __pbn__IsStranger.HasValue;
	}

	public void ResetIsStranger()
	{
		__pbn__IsStranger = null;
	}
}
