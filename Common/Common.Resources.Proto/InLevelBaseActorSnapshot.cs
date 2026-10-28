using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelBaseActorSnapshot : IExtensible
{
	private IExtension __pbn__extensionData;

	private float? __pbn__BaseMaxHp;

	private float? __pbn__BaseMaxSp;

	private float? __pbn__MaxHp;

	private float? __pbn__MaxSp;

	private float? __pbn__Hp;

	private float? __pbn__Sp;

	private uint? __pbn__Level;

	private float? __pbn__Attack;

	private float? __pbn__Defense;

	private bool? __pbn__IsAlive;

	private float? __pbn__TotalOverkillDamage;

	[ProtoMember(1, Name = "base_max_hp")]
	public float BaseMaxHp
	{
		get
		{
			return __pbn__BaseMaxHp.GetValueOrDefault();
		}
		set
		{
			__pbn__BaseMaxHp = value;
		}
	}

	[ProtoMember(2, Name = "base_max_sp")]
	public float BaseMaxSp
	{
		get
		{
			return __pbn__BaseMaxSp.GetValueOrDefault();
		}
		set
		{
			__pbn__BaseMaxSp = value;
		}
	}

	[ProtoMember(3, Name = "max_hp")]
	public float MaxHp
	{
		get
		{
			return __pbn__MaxHp.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxHp = value;
		}
	}

	[ProtoMember(4, Name = "max_sp")]
	public float MaxSp
	{
		get
		{
			return __pbn__MaxSp.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxSp = value;
		}
	}

	[ProtoMember(5, Name = "hp")]
	public float Hp
	{
		get
		{
			return __pbn__Hp.GetValueOrDefault();
		}
		set
		{
			__pbn__Hp = value;
		}
	}

	[ProtoMember(6, Name = "sp")]
	public float Sp
	{
		get
		{
			return __pbn__Sp.GetValueOrDefault();
		}
		set
		{
			__pbn__Sp = value;
		}
	}

	[ProtoMember(7, Name = "level")]
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

	[ProtoMember(8, Name = "attack")]
	public float Attack
	{
		get
		{
			return __pbn__Attack.GetValueOrDefault();
		}
		set
		{
			__pbn__Attack = value;
		}
	}

	[ProtoMember(9, Name = "defense")]
	public float Defense
	{
		get
		{
			return __pbn__Defense.GetValueOrDefault();
		}
		set
		{
			__pbn__Defense = value;
		}
	}

	[ProtoMember(10, Name = "is_alive")]
	public bool IsAlive
	{
		get
		{
			return __pbn__IsAlive.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAlive = value;
		}
	}

	[ProtoMember(11, Name = "total_overkill_damage")]
	public float TotalOverkillDamage
	{
		get
		{
			return __pbn__TotalOverkillDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalOverkillDamage = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBaseMaxHp()
	{
		return __pbn__BaseMaxHp.HasValue;
	}

	public void ResetBaseMaxHp()
	{
		__pbn__BaseMaxHp = null;
	}

	public bool ShouldSerializeBaseMaxSp()
	{
		return __pbn__BaseMaxSp.HasValue;
	}

	public void ResetBaseMaxSp()
	{
		__pbn__BaseMaxSp = null;
	}

	public bool ShouldSerializeMaxHp()
	{
		return __pbn__MaxHp.HasValue;
	}

	public void ResetMaxHp()
	{
		__pbn__MaxHp = null;
	}

	public bool ShouldSerializeMaxSp()
	{
		return __pbn__MaxSp.HasValue;
	}

	public void ResetMaxSp()
	{
		__pbn__MaxSp = null;
	}

	public bool ShouldSerializeHp()
	{
		return __pbn__Hp.HasValue;
	}

	public void ResetHp()
	{
		__pbn__Hp = null;
	}

	public bool ShouldSerializeSp()
	{
		return __pbn__Sp.HasValue;
	}

	public void ResetSp()
	{
		__pbn__Sp = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeAttack()
	{
		return __pbn__Attack.HasValue;
	}

	public void ResetAttack()
	{
		__pbn__Attack = null;
	}

	public bool ShouldSerializeDefense()
	{
		return __pbn__Defense.HasValue;
	}

	public void ResetDefense()
	{
		__pbn__Defense = null;
	}

	public bool ShouldSerializeIsAlive()
	{
		return __pbn__IsAlive.HasValue;
	}

	public void ResetIsAlive()
	{
		__pbn__IsAlive = null;
	}

	public bool ShouldSerializeTotalOverkillDamage()
	{
		return __pbn__TotalOverkillDamage.HasValue;
	}

	public void ResetTotalOverkillDamage()
	{
		__pbn__TotalOverkillDamage = null;
	}
}
