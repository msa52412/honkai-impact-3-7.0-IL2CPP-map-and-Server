using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CabinLevel : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CabinType;

	private uint? __pbn__Level;

	private uint? __pbn__ExtendGrade;

	[ProtoMember(1, Name = "cabin_type")]
	public uint CabinType
	{
		get
		{
			return __pbn__CabinType.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinType = value;
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

	[ProtoMember(3, Name = "extend_grade")]
	public uint ExtendGrade
	{
		get
		{
			return __pbn__ExtendGrade.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtendGrade = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCabinType()
	{
		return __pbn__CabinType.HasValue;
	}

	public void ResetCabinType()
	{
		__pbn__CabinType = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExtendGrade()
	{
		return __pbn__ExtendGrade.HasValue;
	}

	public void ResetExtendGrade()
	{
		__pbn__ExtendGrade = null;
	}
}
