using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzBattleEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6555
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "BATTLE_MISMATCH")]
		BattleMismatch,
		[ProtoEnum(Name = "INVALID_TILE")]
		InvalidTile,
		[ProtoEnum(Name = "RPG_SITE_CANNOT_ENTER")]
		RpgSiteCannotEnter,
		[ProtoEnum(Name = "LACK_TICKET")]
		LackTicket,
		[ProtoEnum(Name = "TILE_SITE_MISMATCH")]
		TileSiteMismatch,
		[ProtoEnum(Name = "END_STATUS_INVALID")]
		EndStatusInvalid
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__TowerId;

	private uint? __pbn__FloorId;

	private PvzBattleEndStatus? __pbn__EndStatus;

	private uint? __pbn__FinishWave;

	private uint? __pbn__MaxFinishWave;

	private uint? __pbn__SiteId;

	private uint? __pbn__CostTime;

	private uint? __pbn__LeftHp;

	private uint? __pbn__UsedCoinNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
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

	[ProtoMember(3, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "floor_id")]
	public uint FloorId
	{
		get
		{
			return __pbn__FloorId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorId = value;
		}
	}

	[ProtoMember(5, Name = "end_status")]
	[DefaultValue(PvzBattleEndStatus.PvzBattleEndNone)]
	public PvzBattleEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(6, Name = "finish_wave")]
	public uint FinishWave
	{
		get
		{
			return __pbn__FinishWave.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishWave = value;
		}
	}

	[ProtoMember(7, Name = "max_finish_wave")]
	public uint MaxFinishWave
	{
		get
		{
			return __pbn__MaxFinishWave.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFinishWave = value;
		}
	}

	[ProtoMember(8, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(9, Name = "qavatar_list")]
	public List<PvzQAvatar> QavatarLists { get; } = new List<PvzQAvatar>();


	[ProtoMember(10, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(11, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(12, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(13, Name = "used_coin_num")]
	public uint UsedCoinNum
	{
		get
		{
			return __pbn__UsedCoinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__UsedCoinNum = value;
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

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeFloorId()
	{
		return __pbn__FloorId.HasValue;
	}

	public void ResetFloorId()
	{
		__pbn__FloorId = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeFinishWave()
	{
		return __pbn__FinishWave.HasValue;
	}

	public void ResetFinishWave()
	{
		__pbn__FinishWave = null;
	}

	public bool ShouldSerializeMaxFinishWave()
	{
		return __pbn__MaxFinishWave.HasValue;
	}

	public void ResetMaxFinishWave()
	{
		__pbn__MaxFinishWave = null;
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeUsedCoinNum()
	{
		return __pbn__UsedCoinNum.HasValue;
	}

	public void ResetUsedCoinNum()
	{
		__pbn__UsedCoinNum = null;
	}
}
