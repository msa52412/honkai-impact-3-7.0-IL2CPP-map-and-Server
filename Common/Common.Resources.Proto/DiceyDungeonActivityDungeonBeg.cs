using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivityDungeonBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6504
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

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

	[ProtoMember(3, Name = "dungeon_info")]
	public DiceyDungeonInfo DungeonInfo { get; set; }

	[ProtoMember(4, Name = "dungeon_type")]
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
public class DiceyDungeonActivityDungeonBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6505
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
		[ProtoEnum(Name = "ROLE_NOT_EXIST")]
		RoleNotExist,
		[ProtoEnum(Name = "WEAPON_NOT_EXIST")]
		WeaponNotExist,
		[ProtoEnum(Name = "ROLE_RESTRICT")]
		RoleRestrict,
		[ProtoEnum(Name = "TICKET_LACK")]
		TicketLack,
		[ProtoEnum(Name = "DUNGEON_TYPE_ERROR")]
		DungeonTypeError,
		[ProtoEnum(Name = "SCHEDULE_ERROR")]
		ScheduleError,
		[ProtoEnum(Name = "ORNAMENT_NUM_ERROR")]
		OrnamentNumError,
		[ProtoEnum(Name = "ORNAMENT_ERROR")]
		OrnamentError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__DungeonId;

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
}
