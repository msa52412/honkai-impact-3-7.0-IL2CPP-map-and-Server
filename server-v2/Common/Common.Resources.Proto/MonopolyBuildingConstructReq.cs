using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyBuildingConstructReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3854
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__BuildingType;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(2, Name = "building_type")]
	public uint BuildingType
	{
		get
		{
			return __pbn__BuildingType.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeBuildingType()
	{
		return __pbn__BuildingType.HasValue;
	}

	public void ResetBuildingType()
	{
		__pbn__BuildingType = null;
	}
}
