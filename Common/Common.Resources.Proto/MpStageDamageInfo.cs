using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageDamageInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HitDamage1;

	private uint? __pbn__HitDamage2;

	private uint? __pbn__HitDamage3;

	[ProtoMember(1, Name = "hit_damage_1")]
	public uint HitDamage1
	{
		get
		{
			return __pbn__HitDamage1.GetValueOrDefault();
		}
		set
		{
			__pbn__HitDamage1 = value;
		}
	}

	[ProtoMember(2, Name = "hit_damage_2")]
	public uint HitDamage2
	{
		get
		{
			return __pbn__HitDamage2.GetValueOrDefault();
		}
		set
		{
			__pbn__HitDamage2 = value;
		}
	}

	[ProtoMember(3, Name = "hit_damage_3")]
	public uint HitDamage3
	{
		get
		{
			return __pbn__HitDamage3.GetValueOrDefault();
		}
		set
		{
			__pbn__HitDamage3 = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHitDamage1()
	{
		return __pbn__HitDamage1.HasValue;
	}

	public void ResetHitDamage1()
	{
		__pbn__HitDamage1 = null;
	}

	public bool ShouldSerializeHitDamage2()
	{
		return __pbn__HitDamage2.HasValue;
	}

	public void ResetHitDamage2()
	{
		__pbn__HitDamage2 = null;
	}

	public bool ShouldSerializeHitDamage3()
	{
		return __pbn__HitDamage3.HasValue;
	}

	public void ResetHitDamage3()
	{
		__pbn__HitDamage3 = null;
	}
}
