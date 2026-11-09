using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChooseOptionalGachaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4717
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private GachaType? __pbn__GachaType;

	private uint? __pbn__SubGachaId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(3, Name = "sub_gacha_id")]
	public uint SubGachaId
	{
		get
		{
			return __pbn__SubGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__SubGachaId = value;
		}
	}

	[ProtoMember(4, Name = "ex_fragment")]
	public GachaExFragment ExFragment { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeSubGachaId()
	{
		return __pbn__SubGachaId.HasValue;
	}

	public void ResetSubGachaId()
	{
		__pbn__SubGachaId = null;
	}
}
