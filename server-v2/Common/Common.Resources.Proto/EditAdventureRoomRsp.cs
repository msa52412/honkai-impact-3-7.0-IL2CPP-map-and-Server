using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EditAdventureRoomRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3921
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SLOT_UNLOCK")]
		SlotUnlock,
		[ProtoEnum(Name = "FURNITURE_LACK")]
		FurnitureLack,
		[ProtoEnum(Name = "FURNITURE_TYPE_ERROR")]
		FurnitureTypeError,
		[ProtoEnum(Name = "COLLECTION_TYPE_ERROR")]
		CollectionTypeError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "slot_id")]
	public uint SlotId
	{
		get
		{
			return __pbn__SlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlotId()
	{
		return __pbn__SlotId.HasValue;
	}

	public void ResetSlotId()
	{
		__pbn__SlotId = null;
	}
}
