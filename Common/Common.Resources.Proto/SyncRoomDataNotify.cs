using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncRoomDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3152
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "player_room_status")]
	public PlayerRoomStatus PlayerRoomStatus { get; set; }

	[ProtoMember(2, Name = "member_list")]
	public List<RoomMember> MemberLists { get; } = new List<RoomMember>();


	[ProtoMember(3, Name = "room_info")]
	public RoomInfo RoomInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
