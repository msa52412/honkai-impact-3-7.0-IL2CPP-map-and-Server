using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityGetShopReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6466
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BuildingUniqueId;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "building_unique_id")]
	public uint BuildingUniqueId
	{
		get
		{
			return __pbn__BuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuildingUniqueId()
	{
		return __pbn__BuildingUniqueId.HasValue;
	}

	public void ResetBuildingUniqueId()
	{
		__pbn__BuildingUniqueId = null;
	}
}
