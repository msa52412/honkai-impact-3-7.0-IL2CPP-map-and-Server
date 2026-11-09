using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChapterId;

	private uint? __pbn__Level;

	private uint? __pbn__LevelExp;

	private bool? __pbn__IsTakeDailyReward;

	private uint? __pbn__PlayerLevel;

	private uint? __pbn__DailyRewardId;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId
	{
		get
		{
			return __pbn__ChapterId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChapterId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "level_exp")]
	public uint LevelExp
	{
		get
		{
			return __pbn__LevelExp.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelExp = value;
		}
	}

	[ProtoMember(4, Name = "has_take_reward_level_list")]
	public uint[] HasTakeRewardLevelLists { get; set; }

	[ProtoMember(5, Name = "is_take_daily_reward")]
	public bool IsTakeDailyReward
	{
		get
		{
			return __pbn__IsTakeDailyReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeDailyReward = value;
		}
	}

	[ProtoMember(6, Name = "boss_list")]
	public List<ChapterActivityBoss> BossLists { get; } = new List<ChapterActivityBoss>();


	[ProtoMember(7, Name = "player_level")]
	public uint PlayerLevel
	{
		get
		{
			return __pbn__PlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerLevel = value;
		}
	}

	[ProtoMember(8, Name = "daily_reward_id")]
	public uint DailyRewardId
	{
		get
		{
			return __pbn__DailyRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyRewardId = value;
		}
	}

	[ProtoMember(10, Name = "stage_list")]
	public List<ChapterActivityStage> StageLists { get; } = new List<ChapterActivityStage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChapterId()
	{
		return __pbn__ChapterId.HasValue;
	}

	public void ResetChapterId()
	{
		__pbn__ChapterId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeLevelExp()
	{
		return __pbn__LevelExp.HasValue;
	}

	public void ResetLevelExp()
	{
		__pbn__LevelExp = null;
	}

	public bool ShouldSerializeIsTakeDailyReward()
	{
		return __pbn__IsTakeDailyReward.HasValue;
	}

	public void ResetIsTakeDailyReward()
	{
		__pbn__IsTakeDailyReward = null;
	}

	public bool ShouldSerializePlayerLevel()
	{
		return __pbn__PlayerLevel.HasValue;
	}

	public void ResetPlayerLevel()
	{
		__pbn__PlayerLevel = null;
	}

	public bool ShouldSerializeDailyRewardId()
	{
		return __pbn__DailyRewardId.HasValue;
	}

	public void ResetDailyRewardId()
	{
		__pbn__DailyRewardId = null;
	}
}
