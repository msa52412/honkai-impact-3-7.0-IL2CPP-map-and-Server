using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivitySellBrickReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6462
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SellBrickIndex;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "sell_brick_index")]
	public uint SellBrickIndex
	{
		get
		{
			return __pbn__SellBrickIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__SellBrickIndex = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSellBrickIndex()
	{
		return __pbn__SellBrickIndex.HasValue;
	}

	public void ResetSellBrickIndex()
	{
		__pbn__SellBrickIndex = null;
	}
}
