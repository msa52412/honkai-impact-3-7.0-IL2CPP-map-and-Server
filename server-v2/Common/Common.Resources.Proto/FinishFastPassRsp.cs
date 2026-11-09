using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishFastPassRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1491
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "QUEST_NOT_EXIST")]
		QuestNotExist,
		[ProtoEnum(Name = "QUEST_NOT_DOING")]
		QuestNotDoing,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound,
		[ProtoEnum(Name = "NO_USE_TIMES")]
		NoUseTimes,
		[ProtoEnum(Name = "STAGE_NOT_EXIST")]
		StageNotExist,
		[ProtoEnum(Name = "STAGE_CANT_FAST_PASS")]
		StageCantFastPass,
		[ProtoEnum(Name = "NO_ENTER_TIMES")]
		NoEnterTimes,
		[ProtoEnum(Name = "STAMINA_LACK")]
		StaminaLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "GENERATE_DROP_FAILED")]
		GenerateDropFailed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FastPassType;

	private uint? __pbn__PlayerExpReward;

	private uint? __pbn__ScoinReward;

	private uint? __pbn__ExpConvertScoin;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "fast_pass_type")]
	public uint FastPassType
	{
		get
		{
			return __pbn__FastPassType.GetValueOrDefault();
		}
		set
		{
			__pbn__FastPassType = value;
		}
	}

	[ProtoMember(4, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(5, Name = "fast_pass_para_ow")]
	public FastPassParaOw FastPassParaOw { get; set; }

	[ProtoMember(6, Name = "fast_pass_para_weekday")]
	public FastPassParaWeekday FastPassParaWeekday { get; set; }

	[ProtoMember(7, Name = "player_exp_reward")]
	public uint PlayerExpReward
	{
		get
		{
			return __pbn__PlayerExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerExpReward = value;
		}
	}

	[ProtoMember(8, Name = "scoin_reward")]
	public uint ScoinReward
	{
		get
		{
			return __pbn__ScoinReward.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinReward = value;
		}
	}

	[ProtoMember(9, Name = "exp_convert_scoin")]
	public uint ExpConvertScoin
	{
		get
		{
			return __pbn__ExpConvertScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpConvertScoin = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFastPassType()
	{
		return __pbn__FastPassType.HasValue;
	}

	public void ResetFastPassType()
	{
		__pbn__FastPassType = null;
	}

	public bool ShouldSerializePlayerExpReward()
	{
		return __pbn__PlayerExpReward.HasValue;
	}

	public void ResetPlayerExpReward()
	{
		__pbn__PlayerExpReward = null;
	}

	public bool ShouldSerializeScoinReward()
	{
		return __pbn__ScoinReward.HasValue;
	}

	public void ResetScoinReward()
	{
		__pbn__ScoinReward = null;
	}

	public bool ShouldSerializeExpConvertScoin()
	{
		return __pbn__ExpConvertScoin.HasValue;
	}

	public void ResetExpConvertScoin()
	{
		__pbn__ExpConvertScoin = null;
	}
}
