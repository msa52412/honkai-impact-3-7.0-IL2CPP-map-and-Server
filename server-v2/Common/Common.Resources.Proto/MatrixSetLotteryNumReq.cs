using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixSetLotteryNumReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2351
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryNum;

	[ProtoMember(1, Name = "lottery_num")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLotteryNum()
	{
		return __pbn__LotteryNum.HasValue;
	}

	public void ResetLotteryNum()
	{
		__pbn__LotteryNum = null;
	}
}
