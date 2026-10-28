using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__DailyChallengeHasDropNum;

	private uint? __pbn__CurDailyChallengeScheduleId;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "qavatar_list")]
	public List<PlatformShooterQAvatar> QavatarLists { get; } = new List<PlatformShooterQAvatar>();


	[ProtoMember(3, Name = "weapon_list")]
	public List<PlatformShooterWeapon> WeaponLists { get; } = new List<PlatformShooterWeapon>();


	[ProtoMember(4, Name = "daily_challenge_has_drop_num")]
	public uint DailyChallengeHasDropNum
	{
		get
		{
			return __pbn__DailyChallengeHasDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyChallengeHasDropNum = value;
		}
	}

	[ProtoMember(5, Name = "daily_stage_info_list")]
	public List<PlatformShooterDailyStageInfo> DailyStageInfoLists { get; } = new List<PlatformShooterDailyStageInfo>();


	[ProtoMember(6, Name = "cur_daily_challenge_schedule_id")]
	public uint CurDailyChallengeScheduleId
	{
		get
		{
			return __pbn__CurDailyChallengeScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurDailyChallengeScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeDailyChallengeHasDropNum()
	{
		return __pbn__DailyChallengeHasDropNum.HasValue;
	}

	public void ResetDailyChallengeHasDropNum()
	{
		__pbn__DailyChallengeHasDropNum = null;
	}

	public bool ShouldSerializeCurDailyChallengeScheduleId()
	{
		return __pbn__CurDailyChallengeScheduleId.HasValue;
	}

	public void ResetCurDailyChallengeScheduleId()
	{
		__pbn__CurDailyChallengeScheduleId = null;
	}
}
