using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMonopolyActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3851
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MonopolyId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "monopoly_id")]
	public uint MonopolyId
	{
		get
		{
			return __pbn__MonopolyId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonopolyId = value;
		}
	}

	[ProtoMember(3, Name = "area_list")]
	public List<MonopolyArea> AreaLists { get; } = new List<MonopolyArea>();


	[ProtoMember(4, Name = "goods_list")]
	public List<MonopolyShopGoods> GoodsLists { get; } = new List<MonopolyShopGoods>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonopolyId()
	{
		return __pbn__MonopolyId.HasValue;
	}

	public void ResetMonopolyId()
	{
		__pbn__MonopolyId = null;
	}
}
