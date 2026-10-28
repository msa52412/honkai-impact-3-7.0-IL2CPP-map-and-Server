using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeTradingCardRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4265
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
		[ProtoEnum(Name = "NOT_JOIN_ARMADA")]
		NotJoinArmada,
		[ProtoEnum(Name = "TRADE_NUM_LIMIT")]
		TradeNumLimit,
		[ProtoEnum(Name = "LACK_MATERIAL")]
		LackMaterial
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TradeNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(3, Name = "trade_num")]
	public uint TradeNum
	{
		get
		{
			return __pbn__TradeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TradeNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTradeNum()
	{
		return __pbn__TradeNum.HasValue;
	}

	public void ResetTradeNum()
	{
		__pbn__TradeNum = null;
	}
}
