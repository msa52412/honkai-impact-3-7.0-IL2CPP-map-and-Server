using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardExitRoomReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7908
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
