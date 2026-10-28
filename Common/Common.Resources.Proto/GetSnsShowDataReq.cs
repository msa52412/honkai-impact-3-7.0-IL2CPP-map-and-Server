using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetSnsShowDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1266
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SnsShowType;

	private uint? __pbn__TargetUid;

	private bool? __pbn__IsFriend;

	private uint? __pbn__ExBossScheduleId;

	[ProtoMember(1, Name = "sns_show_type")]
	public uint SnsShowType
	{
		get
		{
			return __pbn__SnsShowType.GetValueOrDefault();
		}
		set
		{
			__pbn__SnsShowType = value;
		}
	}

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

	[ProtoMember(3, Name = "is_friend")]
	public bool IsFriend
	{
		get
		{
			return __pbn__IsFriend.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFriend = value;
		}
	}

	[ProtoMember(4, Name = "ex_boss_schedule_id")]
	public uint ExBossScheduleId
	{
		get
		{
			return __pbn__ExBossScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExBossScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSnsShowType()
	{
		return __pbn__SnsShowType.HasValue;
	}

	public void ResetSnsShowType()
	{
		__pbn__SnsShowType = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeIsFriend()
	{
		return __pbn__IsFriend.HasValue;
	}

	public void ResetIsFriend()
	{
		__pbn__IsFriend = null;
	}

	public bool ShouldSerializeExBossScheduleId()
	{
		return __pbn__ExBossScheduleId.HasValue;
	}

	public void ResetExBossScheduleId()
	{
		__pbn__ExBossScheduleId = null;
	}
}
