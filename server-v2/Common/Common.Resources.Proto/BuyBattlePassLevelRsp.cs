using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyBattlePassLevelRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3757
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "LEVEL_ERROR")]
		LevelError,
		[ProtoEnum(Name = "HCOIN_ERROR")]
		HcoinError,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HcoinCost;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hcoin_cost")]
	public uint HcoinCost
	{
		get
		{
			return __pbn__HcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__HcoinCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}
}
