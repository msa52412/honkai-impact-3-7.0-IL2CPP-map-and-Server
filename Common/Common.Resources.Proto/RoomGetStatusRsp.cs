using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomGetStatusRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3182
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TOO_FREQUENT")]
		TooFrequent,
		[ProtoEnum(Name = "ROOM_NOT_EXIST")]
		RoomNotExist
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "player_room_status")]
	public PlayerRoomStatus PlayerRoomStatus { get; set; }

	[ProtoMember(3, Name = "member_list")]
	public List<RoomMember> MemberLists { get; } = new List<RoomMember>();


	[ProtoMember(4, Name = "room_info")]
	public RoomInfo RoomInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
