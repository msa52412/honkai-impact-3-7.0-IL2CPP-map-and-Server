using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CreateRoomReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3153
	}

	private IExtension __pbn__extensionData;

	private RoomMode? __pbn__RoomMode;

	[ProtoMember(1, Name = "room_mode")]
	[DefaultValue(RoomMode.RoomModeInvalid)]
	public RoomMode RoomMode
	{
		get
		{
			return __pbn__RoomMode.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomMode = value;
		}
	}

	[ProtoMember(2, Name = "member_data")]
	public RoomMember MemberData { get; set; }

	[ProtoMember(3, Name = "room_info")]
	public RoomInfo RoomInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoomMode()
	{
		return __pbn__RoomMode.HasValue;
	}

	public void ResetRoomMode()
	{
		__pbn__RoomMode = null;
	}
}
