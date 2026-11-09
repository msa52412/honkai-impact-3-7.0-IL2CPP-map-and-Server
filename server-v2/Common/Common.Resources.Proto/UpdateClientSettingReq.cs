using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateClientSettingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1271
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ClientSettingType;

	private bool? __pbn__IsWeeklyGuideSwitchOn;

	[ProtoMember(1, Name = "client_setting_type")]
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

	[ProtoMember(2, Name = "is_weekly_guide_switch_on")]
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

	[ProtoMember(3, Name = "sns_show_priviledge_list")]
	public List<SnsShowPriviledge> SnsShowPriviledgeLists { get; } = new List<SnsShowPriviledge>();


	[ProtoMember(4, Name = "avatar_artifact_switch")]
	public AvatarArtifactSwitch AvatarArtifactSwitch { get; set; }

	[ProtoMember(5, Name = "master_pupil_tag_switch")]
	public MasterPupilTagSwitch MasterPupilTagSwitch { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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
