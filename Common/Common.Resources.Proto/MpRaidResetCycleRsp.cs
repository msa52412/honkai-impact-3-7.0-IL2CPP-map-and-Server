using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpRaidResetCycleRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 361
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "RESET_TIMES_LACK")]
		ResetTimesLack,
		[ProtoEnum(Name = "COIN_LACK")]
		CoinLack,
		[ProtoEnum(Name = "HAVE_REWARD_NOT_TAKEN")]
		HaveRewardNotTaken,
		[ProtoEnum(Name = "NOT_ACTIVE")]
		NotActive
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
