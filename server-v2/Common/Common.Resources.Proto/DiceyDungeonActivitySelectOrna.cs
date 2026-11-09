using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DiceyDungeonActivitySelectOrnamentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6516
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private uint? __pbn__SelectOrnamentId;

	private uint? __pbn__ReplaceOrnamentId;

	private bool? __pbn__IsAbandon;

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

	[ProtoMember(3, Name = "select_ornament_id")]
	public uint SelectOrnamentId
	{
		get
		{
			return __pbn__SelectOrnamentId.GetValueOrDefault();
		}
		set
		{
			__pbn__SelectOrnamentId = value;
		}
	}

	[ProtoMember(4, Name = "replace_ornament_id")]
	public uint ReplaceOrnamentId
	{
		get
		{
			return __pbn__ReplaceOrnamentId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReplaceOrnamentId = value;
		}
	}

	[ProtoMember(5, Name = "is_abandon")]
	public bool IsAbandon
	{
		get
		{
			return __pbn__IsAbandon.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAbandon = value;
		}
	}

	[ProtoMember(6, Name = "dungeon_info")]
	public DiceyDungeonInfo DungeonInfo { get; set; }

	[ProtoMember(7, Name = "dungeon_type")]
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

	public bool ShouldSerializeSelectOrnamentId()
	{
		return __pbn__SelectOrnamentId.HasValue;
	}

	public void ResetSelectOrnamentId()
	{
		__pbn__SelectOrnamentId = null;
	}

	public bool ShouldSerializeReplaceOrnamentId()
	{
		return __pbn__ReplaceOrnamentId.HasValue;
	}

	public void ResetReplaceOrnamentId()
	{
		__pbn__ReplaceOrnamentId = null;
	}

	public bool ShouldSerializeIsAbandon()
	{
		return __pbn__IsAbandon.HasValue;
	}

	public void ResetIsAbandon()
	{
		__pbn__IsAbandon = null;
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
public class DiceyDungeonActivitySelectOrnamentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6517
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
		[ProtoEnum(Name = "SELECT_ORNAMENT_ERROR")]
		SelectOrnamentError,
		[ProtoEnum(Name = "REPLACE_ORNAMENT_ERROR")]
		ReplaceOrnamentError,
		[ProtoEnum(Name = "DUNGEON_NOT_BEGIN")]
		DungeonNotBegin,
		[ProtoEnum(Name = "DUNGEON_ROOM_ERROR")]
		DungeonRoomError,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "WEAPON_ERROR")]
		WeaponError,
		[ProtoEnum(Name = "NO_REPLACE_ORNAMENT")]
		NoReplaceOrnament
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private bool? __pbn__IsAbandon;

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

	[ProtoMember(3, Name = "is_abandon")]
	public bool IsAbandon
	{
		get
		{
			return __pbn__IsAbandon.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAbandon = value;
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

	public bool ShouldSerializeIsAbandon()
	{
		return __pbn__IsAbandon.HasValue;
	}

	public void ResetIsAbandon()
	{
		__pbn__IsAbandon = null;
	}
}
