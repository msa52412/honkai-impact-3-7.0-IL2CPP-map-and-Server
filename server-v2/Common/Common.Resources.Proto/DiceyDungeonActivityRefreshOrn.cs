using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivityRefreshOrnamentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6518
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private uint? __pbn__RefreshOrnamentRandom;

	private DiceyDungeonType? __pbn__DungeonType;

	[ProtoMember(1, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

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

	[ProtoMember(3, Name = "refresh_ornament_random")]
	public uint RefreshOrnamentRandom
	{
		get
		{
			return __pbn__RefreshOrnamentRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshOrnamentRandom = value;
		}
	}

	[ProtoMember(4, Name = "dungeon_info")]
	public DiceyDungeonInfo DungeonInfo { get; set; }

	[ProtoMember(5, Name = "dungeon_type")]
	[DefaultValue(DiceyDungeonType.DiceyDungeonNone)]
	public DiceyDungeonType DungeonType
	{
		get
		{
			return __pbn__DungeonType.GetValueOrDefault();
		}
		set
		{
			__pbn__DungeonType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeRefreshOrnamentRandom()
	{
		return __pbn__RefreshOrnamentRandom.HasValue;
	}

	public void ResetRefreshOrnamentRandom()
	{
		__pbn__RefreshOrnamentRandom = null;
	}

	public bool ShouldSerializeDungeonType()
	{
		return __pbn__DungeonType.HasValue;
	}

	public void ResetDungeonType()
	{
		__pbn__DungeonType = null;
	}
}
[ProtoContract]
public class DiceyDungeonActivityRefreshOrnamentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6519
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
		[ProtoEnum(Name = "SELECT_ROOM_ERROR")]
		SelectRoomError,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError,
		[ProtoEnum(Name = "DUNGEON_ROOM_ERROR")]
		DungeonRoomError,
		[ProtoEnum(Name = "REFRESH_TIMES_LACK")]
		RefreshTimesLack,
		[ProtoEnum(Name = "CANNOT_REFRESH")]
		CannotRefresh
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

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
}
