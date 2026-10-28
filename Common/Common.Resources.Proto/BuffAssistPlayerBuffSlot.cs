using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistPlayerBuffSlot : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__BuffId;

	private bool? __pbn__IsReplace;

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

	[ProtoMember(3, Name = "is_replace")]
	public bool IsReplace
	{
		get
		{
			return __pbn__IsReplace.GetValueOrDefault();
		}
		set
		{
			__pbn__IsReplace = value;
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

	public bool ShouldSerializeIsReplace()
	{
		return __pbn__IsReplace.HasValue;
	}

	public void ResetIsReplace()
	{
		__pbn__IsReplace = null;
	}
}
