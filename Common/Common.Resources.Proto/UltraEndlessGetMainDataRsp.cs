using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessGetMainDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5203
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PLAYER_LEVEL_LACK")]
		PlayerLevelLack,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "IN_OLD_ENDLESS")]
		InOldEndless,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__GroupLevel;

	private uint? __pbn__TopGroupLevel;

	private uint? __pbn__CupNum;

	private bool? __pbn__IsJustJoin;

	private uint? __pbn__DynamicHardLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "group_level")]
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

	[ProtoMember(4, Name = "top_group_level")]
	public uint TopGroupLevel
	{
		get
		{
			return __pbn__TopGroupLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TopGroupLevel = value;
		}
	}

	[ProtoMember(5, Name = "cup_num")]
	public uint CupNum
	{
		get
		{
			return __pbn__CupNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CupNum = value;
		}
	}

	[ProtoMember(6, Name = "main_data")]
	public UltraEndlessMainData MainData { get; set; }

	[ProtoMember(7, Name = "last_settle_info")]
	public UltraEndlessSettleInfo LastSettleInfo { get; set; }

	[ProtoMember(8, Name = "is_just_join")]
	public bool IsJustJoin
	{
		get
		{
			return __pbn__IsJustJoin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsJustJoin = value;
		}
	}

	[ProtoMember(9, Name = "dynamic_hard_level")]
	public uint DynamicHardLevel
	{
		get
		{
			return __pbn__DynamicHardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__DynamicHardLevel = value;
		}
	}

	[ProtoMember(10, Name = "endless_player_list")]
	public List<UltraEndlessPlayer> EndlessPlayerLists { get; } = new List<UltraEndlessPlayer>();


	[ProtoMember(11, Name = "brief_data_list")]
	public List<PlayerFriendBriefData> BriefDataLists { get; } = new List<PlayerFriendBriefData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeGroupLevel()
	{
		return __pbn__GroupLevel.HasValue;
	}

	public void ResetGroupLevel()
	{
		__pbn__GroupLevel = null;
	}

	public bool ShouldSerializeTopGroupLevel()
	{
		return __pbn__TopGroupLevel.HasValue;
	}

	public void ResetTopGroupLevel()
	{
		__pbn__TopGroupLevel = null;
	}

	public bool ShouldSerializeCupNum()
	{
		return __pbn__CupNum.HasValue;
	}

	public void ResetCupNum()
	{
		__pbn__CupNum = null;
	}

	public bool ShouldSerializeIsJustJoin()
	{
		return __pbn__IsJustJoin.HasValue;
	}

	public void ResetIsJustJoin()
	{
		__pbn__IsJustJoin = null;
	}

	public bool ShouldSerializeDynamicHardLevel()
	{
		return __pbn__DynamicHardLevel.HasValue;
	}

	public void ResetDynamicHardLevel()
	{
		__pbn__DynamicHardLevel = null;
	}
}
