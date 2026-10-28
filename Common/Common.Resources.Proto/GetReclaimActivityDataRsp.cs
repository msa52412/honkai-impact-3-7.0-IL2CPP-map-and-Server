using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetReclaimActivityDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1168
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private ReclaimActivityDataType? __pbn__DataType;

	private uint? __pbn__Id;

	private uint? __pbn__Level;

	private uint? __pbn__LevelExp;

	private uint? __pbn__TotalExp;

	private uint? __pbn__CandidateGachaBoxId;

	private uint? __pbn__BoomEnergy;

	private uint? __pbn__EnterBoomStageTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "data_type")]
	[DefaultValue(ReclaimActivityDataType.ReclaimActivityDataAll)]
	public ReclaimActivityDataType DataType
	{
		get
		{
			return __pbn__DataType.GetValueOrDefault();
		}
		set
		{
			__pbn__DataType = value;
		}
	}

	[ProtoMember(3, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(4, Name = "level")]
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

	[ProtoMember(5, Name = "has_take_reward_level_list")]
	public uint[] HasTakeRewardLevelLists { get; set; }

	[ProtoMember(6, Name = "reclaim_avatar_list")]
	public List<ReclaimAvatar> ReclaimAvatarLists { get; } = new List<ReclaimAvatar>();


	[ProtoMember(7, Name = "reclaim_weapon_id_list")]
	public uint[] ReclaimWeaponIdLists { get; set; }

	[ProtoMember(8, Name = "reclaim_stigmata_id_list")]
	public uint[] ReclaimStigmataIdLists { get; set; }

	[ProtoMember(9, Name = "reclaim_avatar_team_list")]
	public uint[] ReclaimAvatarTeamLists { get; set; }

	[ProtoMember(10, Name = "level_exp")]
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

	[ProtoMember(11, Name = "total_exp")]
	public uint TotalExp
	{
		get
		{
			return __pbn__TotalExp.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalExp = value;
		}
	}

	[ProtoMember(12, Name = "candidate_item_list")]
	public uint[] CandidateItemLists { get; set; }

	[ProtoMember(13, Name = "candidate_gacha_box_id")]
	public uint CandidateGachaBoxId
	{
		get
		{
			return __pbn__CandidateGachaBoxId.GetValueOrDefault();
		}
		set
		{
			__pbn__CandidateGachaBoxId = value;
		}
	}

	[ProtoMember(14, Name = "boom_energy")]
	public uint BoomEnergy
	{
		get
		{
			return __pbn__BoomEnergy.GetValueOrDefault();
		}
		set
		{
			__pbn__BoomEnergy = value;
		}
	}

	[ProtoMember(15, Name = "enter_boom_stage_times")]
	public uint EnterBoomStageTimes
	{
		get
		{
			return __pbn__EnterBoomStageTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__EnterBoomStageTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDataType()
	{
		return __pbn__DataType.HasValue;
	}

	public void ResetDataType()
	{
		__pbn__DataType = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
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

	public bool ShouldSerializeTotalExp()
	{
		return __pbn__TotalExp.HasValue;
	}

	public void ResetTotalExp()
	{
		__pbn__TotalExp = null;
	}

	public bool ShouldSerializeCandidateGachaBoxId()
	{
		return __pbn__CandidateGachaBoxId.HasValue;
	}

	public void ResetCandidateGachaBoxId()
	{
		__pbn__CandidateGachaBoxId = null;
	}

	public bool ShouldSerializeBoomEnergy()
	{
		return __pbn__BoomEnergy.HasValue;
	}

	public void ResetBoomEnergy()
	{
		__pbn__BoomEnergy = null;
	}

	public bool ShouldSerializeEnterBoomStageTimes()
	{
		return __pbn__EnterBoomStageTimes.HasValue;
	}

	public void ResetEnterBoomStageTimes()
	{
		__pbn__EnterBoomStageTimes = null;
	}
}
