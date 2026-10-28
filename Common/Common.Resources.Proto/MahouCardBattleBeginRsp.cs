using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardBattleBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7931
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
		[ProtoEnum(Name = "NOT_IN_SITE")]
		NotInSite,
		[ProtoEnum(Name = "NOT_IN_ROOM")]
		NotInRoom,
		[ProtoEnum(Name = "NO_NEED_TO_BATTLE")]
		NoNeedToBattle,
		[ProtoEnum(Name = "NO_BATTLE_HP")]
		NoBattleHp,
		[ProtoEnum(Name = "RETRY_TIMES_LIMIT")]
		RetryTimesLimit,
		[ProtoEnum(Name = "WAIT_CHOOSE_OPTION_EVENT")]
		WaitChooseOptionEvent
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
