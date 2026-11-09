using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistBuffSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__BuffId;

	private bool? __pbn__IsUsed;

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

	[ProtoMember(2, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(3, Name = "is_used")]
	public bool IsUsed
	{
		get
		{
			return __pbn__IsUsed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUsed = value;
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

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeIsUsed()
	{
		return __pbn__IsUsed.HasValue;
	}

	public void ResetIsUsed()
	{
		__pbn__IsUsed = null;
	}
}
