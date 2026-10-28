using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixLottery : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryId;

	private uint? __pbn__LotteryNum;

	private uint? __pbn__LeftStep;

	[ProtoMember(1, Name = "lottery_id")]
	public uint LotteryId
	{
		get
		{
			return __pbn__LotteryId.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryId = value;
		}
	}

	[ProtoMember(2, Name = "lottery_num")]
	public uint LotteryNum
	{
		get
		{
			return __pbn__LotteryNum.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryNum = value;
		}
	}

	[ProtoMember(3, Name = "left_step")]
	public uint LeftStep
	{
		get
		{
			return __pbn__LeftStep.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftStep = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLotteryId()
	{
		return __pbn__LotteryId.HasValue;
	}

	public void ResetLotteryId()
	{
		__pbn__LotteryId = null;
	}

	public bool ShouldSerializeLotteryNum()
	{
		return __pbn__LotteryNum.HasValue;
	}

	public void ResetLotteryNum()
	{
		__pbn__LotteryNum = null;
	}

	public bool ShouldSerializeLeftStep()
	{
		return __pbn__LeftStep.HasValue;
	}

	public void ResetLeftStep()
	{
		__pbn__LeftStep = null;
	}
}
