using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UserRankWeeklyReportUltraEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__CupNumBefore;

	private uint? __pbn__CupNumAfterScheduleSettle;

	private uint? __pbn__BattleConfigId;

	[ProtoMember(1, Name = "group_level")]
	public uint GroupLevel
	{
		get
		{
			return __pbn__GroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupLevel = value;
		}
	}

	[ProtoMember(2, Name = "cup_num_before")]
	public uint CupNumBefore
	{
		get
		{
			return __pbn__CupNumBefore.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumBefore = value;
		}
	}

	[ProtoMember(3, Name = "cup_num_after_schedule_settle")]
	public uint CupNumAfterScheduleSettle
	{
		get
		{
			return __pbn__CupNumAfterScheduleSettle.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNumAfterScheduleSettle = value;
		}
	}

	[ProtoMember(4, Name = "battle_config_id")]
	public uint BattleConfigId
	{
		get
		{
			return __pbn__BattleConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleConfigId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeCupNumBefore()
	{
		return __pbn__CupNumBefore.HasValue;
	}

	public void ResetCupNumBefore()
	{
		__pbn__CupNumBefore = null;
	}

	public bool ShouldSerializeCupNumAfterScheduleSettle()
	{
		return __pbn__CupNumAfterScheduleSettle.HasValue;
	}

	public void ResetCupNumAfterScheduleSettle()
	{
		__pbn__CupNumAfterScheduleSettle = null;
	}

	public bool ShouldSerializeBattleConfigId()
	{
		return __pbn__BattleConfigId.HasValue;
	}

	public void ResetBattleConfigId()
	{
		__pbn__BattleConfigId = null;
	}
}
