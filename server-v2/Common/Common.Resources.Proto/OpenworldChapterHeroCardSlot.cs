using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterHeroCardSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotIdx;

	private uint? __pbn__CardId;

	[ProtoMember(1, Name = "slot_idx")]
	public uint SlotIdx
	{
		get
		{
			return __pbn__SlotIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotIdx = value;
		}
	}

	[ProtoMember(2, Name = "card_id")]
	public uint CardId
	{
		get
		{
			return __pbn__CardId.GetValueOrDefault();
		}
		set
		{
			__pbn__CardId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlotIdx()
	{
		return __pbn__SlotIdx.HasValue;
	}

	public void ResetSlotIdx()
	{
		__pbn__SlotIdx = null;
	}

	public bool ShouldSerializeCardId()
	{
		return __pbn__CardId.HasValue;
	}

	public void ResetCardId()
	{
		__pbn__CardId = null;
	}
}
