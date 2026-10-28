using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4700
	}

	private IExtension __pbn__extensionData;

	private GachaType? __pbn__Type;

	private uint? __pbn__Num;

	private bool? __pbn__IsUseHcoin;

	private uint? __pbn__GachaRandom;

	private uint? __pbn__SimulateMagic;

	private bool? __pbn__IsUseFreeGacha;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType Type
	{
		get
		{
			return __pbn__Type ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(3, Name = "is_use_hcoin")]
	public bool IsUseHcoin
	{
		get
		{
			return __pbn__IsUseHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseHcoin = value;
		}
	}

	[ProtoMember(4, Name = "gacha_random")]
	public uint GachaRandom
	{
		get
		{
			return __pbn__GachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaRandom = value;
		}
	}

	[ProtoMember(5, Name = "simulate_magic")]
	public uint SimulateMagic
	{
		get
		{
			return __pbn__SimulateMagic.GetValueOrDefault();
		}
		set
		{
			__pbn__SimulateMagic = value;
		}
	}

	[ProtoMember(6, Name = "is_use_free_gacha")]
	public bool IsUseFreeGacha
	{
		get
		{
			return __pbn__IsUseFreeGacha.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseFreeGacha = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeIsUseHcoin()
	{
		return __pbn__IsUseHcoin.HasValue;
	}

	public void ResetIsUseHcoin()
	{
		__pbn__IsUseHcoin = null;
	}

	public bool ShouldSerializeGachaRandom()
	{
		return __pbn__GachaRandom.HasValue;
	}

	public void ResetGachaRandom()
	{
		__pbn__GachaRandom = null;
	}

	public bool ShouldSerializeSimulateMagic()
	{
		return __pbn__SimulateMagic.HasValue;
	}

	public void ResetSimulateMagic()
	{
		__pbn__SimulateMagic = null;
	}

	public bool ShouldSerializeIsUseFreeGacha()
	{
		return __pbn__IsUseFreeGacha.HasValue;
	}

	public void ResetIsUseFreeGacha()
	{
		__pbn__IsUseFreeGacha = null;
	}
}
