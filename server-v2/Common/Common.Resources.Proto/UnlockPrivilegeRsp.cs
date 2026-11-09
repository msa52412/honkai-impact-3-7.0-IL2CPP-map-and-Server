using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockPrivilegeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1643
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "NEWBIE_SCHEDULE_ERROR")]
		NewbieScheduleError,
		[ProtoEnum(Name = "NEWBIE_DAY_LACK")]
		NewbieDayLack,
		[ProtoEnum(Name = "BUY_TIMES_LACK")]
		BuyTimesLack,
		[ProtoEnum(Name = "MCOIN_LACK")]
		McoinLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
