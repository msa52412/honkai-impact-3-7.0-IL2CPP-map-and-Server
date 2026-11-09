using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardExitRoomRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7909
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
		[ProtoEnum(Name = "ROOM_ERROR")]
		RoomError,
		[ProtoEnum(Name = "NO_BATTLE_HP")]
		NoBattleHp,
		[ProtoEnum(Name = "WAIT_SELECT_PROPHESY")]
		WaitSelectProphesy,
		[ProtoEnum(Name = "WAIT_CHOOSE_OPTION_EVENT")]
		WaitChooseOptionEvent,
		[ProtoEnum(Name = "WAIT_FINISH_BATTLE")]
		WaitFinishBattle
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "cur_site")]
	public MahouCardSiteInfo CurSite { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
