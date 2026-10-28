using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixLotteryDrawNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2353
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WinNum;

	[ProtoMember(1, Name = "lottery")]
	public MatrixLottery Lottery { get; set; }

	[ProtoMember(2, Name = "win_num")]
	public uint WinNum
	{
		get
		{
			return __pbn__WinNum.GetValueOrDefault();
		}
		set
		{
			__pbn__WinNum = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWinNum()
	{
		return __pbn__WinNum.HasValue;
	}

	public void ResetWinNum()
	{
		__pbn__WinNum = null;
	}
}
