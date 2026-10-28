using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LotteryDrawNumberReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4181
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryId;

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
}
