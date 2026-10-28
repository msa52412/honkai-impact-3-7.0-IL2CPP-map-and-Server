using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOtherPlayerClientSettingRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1274
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__ClientSettingType;

	private bool? __pbn__IsWeeklyGuideSwitchOn;

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

	[ProtoMember(3, Name = "client_setting_type")]
	public uint ClientSettingType
	{
		get
		{
			return __pbn__ClientSettingType.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientSettingType = value;
		}
	}

	[ProtoMember(4, Name = "is_weekly_guide_switch_on")]
	public bool IsWeeklyGuideSwitchOn
	{
		get
		{
			return __pbn__IsWeeklyGuideSwitchOn.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWeeklyGuideSwitchOn = value;
		}
	}

	[ProtoMember(5, Name = "sns_show_priviledge_list")]
	public List<SnsShowPriviledge> SnsShowPriviledgeLists { get; } = new List<SnsShowPriviledge>();


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

	public bool ShouldSerializeClientSettingType()
	{
		return __pbn__ClientSettingType.HasValue;
	}

	public void ResetClientSettingType()
	{
		__pbn__ClientSettingType = null;
	}

	public bool ShouldSerializeIsWeeklyGuideSwitchOn()
	{
		return __pbn__IsWeeklyGuideSwitchOn.HasValue;
	}

	public void ResetIsWeeklyGuideSwitchOn()
	{
		__pbn__IsWeeklyGuideSwitchOn = null;
	}
}
