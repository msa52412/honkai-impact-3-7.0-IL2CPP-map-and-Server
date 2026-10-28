using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaStageRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2674
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Hp1Percent;

	private uint? __pbn__Hp2Percent;

	private uint? __pbn__Hp3Percent;

	private bool? __pbn__IsTake;

	private uint? __pbn__KillRewardMaterialId;

	private uint? __pbn__KillRewardMaterialNum;

	private uint? __pbn__LastDamageRate;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hp_1_percent")]
	public uint Hp1Percent
	{
		get
		{
			return __pbn__Hp1Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp1Percent = value;
		}
	}

	[ProtoMember(3, Name = "hp_2_percent")]
	public uint Hp2Percent
	{
		get
		{
			return __pbn__Hp2Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp2Percent = value;
		}
	}

	[ProtoMember(4, Name = "hp_3_percent")]
	public uint Hp3Percent
	{
		get
		{
			return __pbn__Hp3Percent.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp3Percent = value;
		}
	}

	[ProtoMember(5, Name = "is_take")]
	public bool IsTake
	{
		get
		{
			return __pbn__IsTake.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTake = value;
		}
	}

	[ProtoMember(6, Name = "kill_reward_material_id")]
	public uint KillRewardMaterialId
	{
		get
		{
			return __pbn__KillRewardMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__KillRewardMaterialId = value;
		}
	}

	[ProtoMember(7, Name = "kill_reward_material_num")]
	public uint KillRewardMaterialNum
	{
		get
		{
			return __pbn__KillRewardMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__KillRewardMaterialNum = value;
		}
	}

	[ProtoMember(8, Name = "last_damage_rate")]
	public uint LastDamageRate
	{
		get
		{
			return __pbn__LastDamageRate.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDamageRate = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHp1Percent()
	{
		return __pbn__Hp1Percent.HasValue;
	}

	public void ResetHp1Percent()
	{
		__pbn__Hp1Percent = null;
	}

	public bool ShouldSerializeHp2Percent()
	{
		return __pbn__Hp2Percent.HasValue;
	}

	public void ResetHp2Percent()
	{
		__pbn__Hp2Percent = null;
	}

	public bool ShouldSerializeHp3Percent()
	{
		return __pbn__Hp3Percent.HasValue;
	}

	public void ResetHp3Percent()
	{
		__pbn__Hp3Percent = null;
	}

	public bool ShouldSerializeIsTake()
	{
		return __pbn__IsTake.HasValue;
	}

	public void ResetIsTake()
	{
		__pbn__IsTake = null;
	}

	public bool ShouldSerializeKillRewardMaterialId()
	{
		return __pbn__KillRewardMaterialId.HasValue;
	}

	public void ResetKillRewardMaterialId()
	{
		__pbn__KillRewardMaterialId = null;
	}

	public bool ShouldSerializeKillRewardMaterialNum()
	{
		return __pbn__KillRewardMaterialNum.HasValue;
	}

	public void ResetKillRewardMaterialNum()
	{
		__pbn__KillRewardMaterialNum = null;
	}

	public bool ShouldSerializeLastDamageRate()
	{
		return __pbn__LastDamageRate.HasValue;
	}

	public void ResetLastDamageRate()
	{
		__pbn__LastDamageRate = null;
	}
}
