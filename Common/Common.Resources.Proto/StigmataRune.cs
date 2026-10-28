using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class StigmataRune : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RuneId;

	private uint? __pbn__StrengthPercent;

	[ProtoMember(1, Name = "rune_id")]
	public uint RuneId
	{
		get
		{
			return __pbn__RuneId.GetValueOrDefault();
		}
		set
		{
			__pbn__RuneId = value;
		}
	}

	[ProtoMember(2, Name = "strength_percent")]
	public uint StrengthPercent
	{
		get
		{
			return __pbn__StrengthPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__StrengthPercent = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRuneId()
	{
		return __pbn__RuneId.HasValue;
	}

	public void ResetRuneId()
	{
		__pbn__RuneId = null;
	}

	public bool ShouldSerializeStrengthPercent()
	{
		return __pbn__StrengthPercent.HasValue;
	}

	public void ResetStrengthPercent()
	{
		__pbn__StrengthPercent = null;
	}
}
