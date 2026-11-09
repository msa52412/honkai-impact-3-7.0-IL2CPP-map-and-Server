using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaSlotChooseEffectReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4405
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__EffectId;

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

	[ProtoMember(2, Name = "effect_id")]
	public uint EffectId
	{
		get
		{
			return __pbn__EffectId.GetValueOrDefault();
		}
		set
		{
			__pbn__EffectId = value;
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

	public bool ShouldSerializeEffectId()
	{
		return __pbn__EffectId.HasValue;
	}

	public void ResetEffectId()
	{
		__pbn__EffectId = null;
	}
}
