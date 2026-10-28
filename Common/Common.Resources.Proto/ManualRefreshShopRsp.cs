using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ManualRefreshShopRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6709
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SHOP_NOT_EXIST")]
		ShopNotExist,
		[ProtoEnum(Name = "REFRESH_TIMES_LACK")]
		RefreshTimesLack,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "SHOP_CLOSE")]
		ShopClose,
		[ProtoEnum(Name = "NO_NEED_REFRESH")]
		NoNeedRefresh,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
