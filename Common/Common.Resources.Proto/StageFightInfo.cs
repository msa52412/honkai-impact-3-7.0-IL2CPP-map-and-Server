using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StageFightInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Damage;

	private uint? __pbn__ElfDamage;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "damage")]
	public uint Damage
	{
		get
		{
			return __pbn__Damage.GetValueOrDefault();
		}
		set
		{
			__pbn__Damage = value;
		}
	}

	[ProtoMember(2, Name = "elf_damage")]
	public uint ElfDamage
	{
		get
		{
			return __pbn__ElfDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__ElfDamage = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDamage()
	{
		return __pbn__Damage.HasValue;
	}

	public void ResetDamage()
	{
		__pbn__Damage = null;
	}

	public bool ShouldSerializeElfDamage()
	{
		return __pbn__ElfDamage.HasValue;
	}

	public void ResetElfDamage()
	{
		__pbn__ElfDamage = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
