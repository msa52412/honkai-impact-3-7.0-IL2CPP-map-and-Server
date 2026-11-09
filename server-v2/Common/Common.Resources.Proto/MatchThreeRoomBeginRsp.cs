using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatchThreeRoomBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7612
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
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError,
		[ProtoEnum(Name = "ITEM_ERROR")]
		ItemError,
		[ProtoEnum(Name = "ROOM_ERROR")]
		RoomError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__RoomId;

	private string __pbn__TransactionStr;

	private uint? __pbn__DynamicLevel;

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

	[ProtoMember(4, Name = "transaction_str")]
	[DefaultValue("")]
	public string TransactionStr
	{
		get
		{
			return __pbn__TransactionStr ?? "";
		}
		set
		{
			__pbn__TransactionStr = value;
		}
	}

	[ProtoMember(5, Name = "dynamic_level")]
	public uint DynamicLevel
	{
		get
		{
			return __pbn__DynamicLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__DynamicLevel = value;
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

	public bool ShouldSerializeRoomId()
	{
		return __pbn__RoomId.HasValue;
	}

	public void ResetRoomId()
	{
		__pbn__RoomId = null;
	}

	public bool ShouldSerializeTransactionStr()
	{
		return __pbn__TransactionStr != null;
	}

	public void ResetTransactionStr()
	{
		__pbn__TransactionStr = null;
	}

	public bool ShouldSerializeDynamicLevel()
	{
		return __pbn__DynamicLevel.HasValue;
	}

	public void ResetDynamicLevel()
	{
		__pbn__DynamicLevel = null;
	}
}
