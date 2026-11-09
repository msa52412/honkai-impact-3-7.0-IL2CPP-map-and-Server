using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__ChoosedEffectId;

	[ProtoMember(1, Name = "slot_id")]
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

	[ProtoMember(2, Name = "level")]
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

	[ProtoMember(3, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(4, Name = "choosed_effect_id")]
	public uint ChoosedEffectId
	{
		get
		{
			return __pbn__ChoosedEffectId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChoosedEffectId = value;
		}
	}

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

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeChoosedEffectId()
	{
		return __pbn__ChoosedEffectId.HasValue;
	}

	public void ResetChoosedEffectId()
	{
		__pbn__ChoosedEffectId = null;
	}
}
