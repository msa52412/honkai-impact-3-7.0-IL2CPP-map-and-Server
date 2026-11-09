using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivitySellBuildingReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6458
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SellBuildingUniqueId;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "sell_building_unique_id")]
	public uint SellBuildingUniqueId
	{
		get
		{
			return __pbn__SellBuildingUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__SellBuildingUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSellBuildingUniqueId()
	{
		return __pbn__SellBuildingUniqueId.HasValue;
	}

	public void ResetSellBuildingUniqueId()
	{
		__pbn__SellBuildingUniqueId = null;
	}
}
