using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AdventureDecorationSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__FurnitureId;

	private uint? __pbn__Direction;

	[ProtoMember(1, Name = "slot_id")]
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

	[ProtoMember(2, Name = "furniture_id")]
	public uint FurnitureId
	{
		get
		{
			return __pbn__FurnitureId.GetValueOrDefault();
		}
		set
		{
			__pbn__FurnitureId = value;
		}
	}

	[ProtoMember(3, Name = "direction")]
	public uint Direction
	{
		get
		{
			return __pbn__Direction.GetValueOrDefault();
		}
		set
		{
			__pbn__Direction = value;
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

	public bool ShouldSerializeFurnitureId()
	{
		return __pbn__FurnitureId.HasValue;
	}

	public void ResetFurnitureId()
	{
		__pbn__FurnitureId = null;
	}

	public bool ShouldSerializeDirection()
	{
		return __pbn__Direction.HasValue;
	}

	public void ResetDirection()
	{
		__pbn__Direction = null;
	}
}
