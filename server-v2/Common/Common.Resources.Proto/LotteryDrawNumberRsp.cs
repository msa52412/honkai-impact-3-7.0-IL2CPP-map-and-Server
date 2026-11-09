using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LotteryDrawNumberRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4182
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "TIMES_LIMIT")]
		TimesLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LotteryId;

	private uint? __pbn__LotteryNumber;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "lottery_id")]
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

	[ProtoMember(3, Name = "lottery_number")]
	public uint LotteryNumber
	{
		get
		{
			return __pbn__LotteryNumber.GetValueOrDefault();
		}
		set
		{
			__pbn__LotteryNumber = value;
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

	public bool ShouldSerializeLotteryNumber()
	{
		return __pbn__LotteryNumber.HasValue;
	}

	public void ResetLotteryNumber()
	{
		__pbn__LotteryNumber = null;
	}
}
