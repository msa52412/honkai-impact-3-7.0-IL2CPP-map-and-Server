using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardEnterRoomRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7907
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 3,
		[ProtoEnum(Name = "NOT_IN_SITE")]
		NotInSite = 4,
		[ProtoEnum(Name = "UNFINISHED_ROOM")]
		UnfinishedRoom = 5,
		[ProtoEnum(Name = "ROOM_ERROR")]
		RoomError = 6,
		[ProtoEnum(Name = "ALREADY_DEAD")]
		AlreadyDead = 7,
		[ProtoEnum(Name = "WAIT_SELECT_EVENT_OPTION")]
		WaitSelectEventOption = 8
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(4, Name = "cur_site")]
	public MahouCardSiteInfo CurSite { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
