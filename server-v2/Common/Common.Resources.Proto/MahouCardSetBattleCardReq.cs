using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardSetBattleCardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7919
	}

	[ProtoContract]
	public enum Direction
	{
		[ProtoEnum(Name = "NONE")]
		None,
		[ProtoEnum(Name = "LEFT")]
		Left,
		[ProtoEnum(Name = "RIGHT")]
		Right
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId { get; set; }

	[ProtoMember(4, Name = "unique_id")]
	public uint UniqueId { get; set; }

	[ProtoMember(5, Name = "slot")]
	public uint Slot { get; set; }

	[ProtoMember(6)]
	public Direction direction { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
