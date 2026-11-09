using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivityRoomEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6511
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_EXIST")]
		ActivityNotExist,
		[ProtoEnum(Name = "DUNGEON_NOT_BEGIN")]
		DungeonNotBegin,
		[ProtoEnum(Name = "DUNGEON_ROOM_ERROR")]
		DungeonRoomError,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError,
		[ProtoEnum(Name = "CHECK_EVENT_FAIL")]
		CheckEventFail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__DungeonId;

	private uint? __pbn__DungeonRoomId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "dungeon_id")]
	public uint DungeonId
	{
		get
		{
			return __pbn__DungeonId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonId = value;
		}
	}

	[ProtoMember(4, Name = "dungeon_room_id")]
	public uint DungeonRoomId
	{
		get
		{
			return __pbn__DungeonRoomId.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonRoomId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeDungeonId()
	{
		return __pbn__DungeonId.HasValue;
	}

	public void ResetDungeonId()
	{
		__pbn__DungeonId = null;
	}

	public bool ShouldSerializeDungeonRoomId()
	{
		return __pbn__DungeonRoomId.HasValue;
	}

	public void ResetDungeonRoomId()
	{
		__pbn__DungeonRoomId = null;
	}
}
