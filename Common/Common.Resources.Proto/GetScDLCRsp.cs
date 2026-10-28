using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetScDLCRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6301
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

	private string __pbn__Name;

	private uint? __pbn__MaxChallengeRewardTakeProgress;

	private uint? __pbn__MaxActiveFeverAbilityGroupNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "fever")]
	public ScDLCFever Fever { get; set; }

	[ProtoMember(3, Name = "avatar_list")]
	public List<ScDLCAvatar> AvatarLists { get; } = new List<ScDLCAvatar>();


	[ProtoMember(4, Name = "npc_list")]
	public List<ScDLCSupportNPC> NpcLists { get; } = new List<ScDLCSupportNPC>();


	[ProtoMember(5, Name = "daily_quest_data")]
	public ScDLCDailyQuestData DailyQuestData { get; set; }

	[ProtoMember(6, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(7, Name = "achievement_list")]
	public List<ScDLCAchievement> AchievementLists { get; } = new List<ScDLCAchievement>();


	[ProtoMember(8, Name = "max_challenge_reward_take_progress")]
	public uint MaxChallengeRewardTakeProgress
	{
		get
		{
			return __pbn__MaxChallengeRewardTakeProgress.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxChallengeRewardTakeProgress = value;
		}
	}

	[ProtoMember(9, Name = "active_fever_ability_group_id_list")]
	public uint[] ActiveFeverAbilityGroupIdLists { get; set; }

	[ProtoMember(10, Name = "max_active_fever_ability_group_num")]
	public uint MaxActiveFeverAbilityGroupNum
	{
		get
		{
			return __pbn__MaxActiveFeverAbilityGroupNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxActiveFeverAbilityGroupNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeMaxChallengeRewardTakeProgress()
	{
		return __pbn__MaxChallengeRewardTakeProgress.HasValue;
	}

	public void ResetMaxChallengeRewardTakeProgress()
	{
		__pbn__MaxChallengeRewardTakeProgress = null;
	}

	public bool ShouldSerializeMaxActiveFeverAbilityGroupNum()
	{
		return __pbn__MaxActiveFeverAbilityGroupNum.HasValue;
	}

	public void ResetMaxActiveFeverAbilityGroupNum()
	{
		__pbn__MaxActiveFeverAbilityGroupNum = null;
	}
}
