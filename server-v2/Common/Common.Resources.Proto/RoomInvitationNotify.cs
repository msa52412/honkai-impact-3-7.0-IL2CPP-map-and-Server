using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomInvitationNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3162
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private RoomMode? __pbn__RoomMode;

	private uint? __pbn__RoomId;

	[ProtoMember(1, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(2, Name = "room_mode")]
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

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId
	{
		get
		{
			return __pbn__RoomId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomId = value;
		}
	}

	[ProtoMember(4, Name = "from_player")]
	public RoomMember FromPlayer { get; set; }

	[ProtoMember(5, Name = "room_info")]
	public RoomInfo RoomInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}

	public bool ShouldSerializeRoomMode()
	{
		return __pbn__RoomMode.HasValue;
	}

	public void ResetRoomMode()
	{
		__pbn__RoomMode = null;
	}

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}
}
