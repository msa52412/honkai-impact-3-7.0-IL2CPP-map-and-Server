using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeRoomBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7611
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TaleId;

	private uint? __pbn__ActivityId;

	private MatchThreeRoomType? __pbn__RoomType;

	private uint? __pbn__RoomId;

	private uint? __pbn__RoleId;

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

	[ProtoMember(3, Name = "room_type")]
	[DefaultValue(MatchThreeRoomType.MatchThreeRoomNone)]
	public MatchThreeRoomType RoomType
	{
		get
		{
			return __pbn__RoomType.GetValueOrDefault();
		}
		set
		{
			__pbn__RoomType = value;
		}
	}

	[ProtoMember(4, Name = "room_id")]
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

	[ProtoMember(5, Name = "role_id")]
	public uint RoleId
	{
		get
		{
			return __pbn__RoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoleId = value;
		}
	}

	[ProtoMember(6, Name = "item_list")]
	public uint[] ItemLists { get; set; }

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

	public bool ShouldSerializeRoomType()
	{
		return __pbn__RoomType.HasValue;
	}

	public void ResetRoomType()
	{
		__pbn__RoomType = null;
	}

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}

	public bool ShouldSerializeRoleId()
	{
		return __pbn__RoleId.HasValue;
	}

	public void ResetRoleId()
	{
		__pbn__RoleId = null;
	}
}
