using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FacilityLevel : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FacilityType;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "facility_type")]
	public uint FacilityType
	{
		get
		{
			return __pbn__FacilityType.GetValueOrDefault();
		}
		set
		{
			__pbn__FacilityType = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFacilityType()
	{
		return __pbn__FacilityType.HasValue;
	}

	public void ResetFacilityType()
	{
		__pbn__FacilityType = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
