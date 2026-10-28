using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishFarmProduceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1392
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SLOT_LOCKED")]
		SlotLocked,
		[ProtoEnum(Name = "NOT_IN_PRODUCE")]
		NotInProduce,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private bool? __pbn__IsFinishAll;

	private uint? __pbn__RewardFarmExp;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "slot_id")]
	public uint SlotId
	{
		get
		{
			return __pbn__SlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotId = value;
		}
	}

	[ProtoMember(3, Name = "is_finish_all")]
	public bool IsFinishAll
	{
		get
		{
			return __pbn__IsFinishAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishAll = value;
		}
	}

	[ProtoMember(4, Name = "reward_farm_exp")]
	public uint RewardFarmExp
	{
		get
		{
			return __pbn__RewardFarmExp.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardFarmExp = value;
		}
	}

	[ProtoMember(5, Name = "produce_material_list")]
	public List<Material> ProduceMaterialLists { get; } = new List<Material>();


	[ProtoMember(6, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlotId()
	{
		return __pbn__SlotId.HasValue;
	}

	public void ResetSlotId()
	{
		__pbn__SlotId = null;
	}

	public bool ShouldSerializeIsFinishAll()
	{
		return __pbn__IsFinishAll.HasValue;
	}

	public void ResetIsFinishAll()
	{
		__pbn__IsFinishAll = null;
	}

	public bool ShouldSerializeRewardFarmExp()
	{
		return __pbn__RewardFarmExp.HasValue;
	}

	public void ResetRewardFarmExp()
	{
		__pbn__RewardFarmExp = null;
	}
}
