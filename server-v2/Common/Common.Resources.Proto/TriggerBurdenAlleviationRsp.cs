using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TriggerBurdenAlleviationRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4355
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "PLAYER_LEVEL_LIMIT")]
		PlayerLevelLimit,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "STAGE_NOT_EXIST")]
		StageNotExist,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "NO_ENTER_TIMES")]
		NoEnterTimes,
		[ProtoEnum(Name = "EXTRA_MATERIAL_LACK")]
		ExtraMaterialLack,
		[ProtoEnum(Name = "RAID_IS_PASS")]
		RaidIsPass,
		[ProtoEnum(Name = "WEEKDAY_STAGE_ID_NOT_MATCH")]
		WeekdayStageIdNotMatch,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AlleviationId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "alleviation_id")]
	public uint AlleviationId
	{
		get
		{
			return __pbn__AlleviationId.GetValueOrDefault();
		}
		set
		{
			__pbn__AlleviationId = value;
		}
	}

	[ProtoMember(3, Name = "para_list")]
	public uint[] ParaLists { get; set; }

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(5, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAlleviationId()
	{
		return __pbn__AlleviationId.HasValue;
	}

	public void ResetAlleviationId()
	{
		__pbn__AlleviationId = null;
	}
}
