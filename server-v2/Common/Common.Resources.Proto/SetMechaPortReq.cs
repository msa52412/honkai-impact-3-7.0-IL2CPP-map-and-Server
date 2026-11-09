using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetMechaPortReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4518
	}

	private IExtension __pbn__extensionData;

	private MechaSlot? __pbn__Slot;

	private uint? __pbn__MechaId;

	[ProtoMember(1, Name = "slot")]
	[DefaultValue(MechaSlot.MechaSlotNone)]
	public MechaSlot Slot
	{
		get
		{
			return __pbn__Slot.GetValueOrDefault();
		}
		set
		{
			__pbn__Slot = value;
		}
	}

	[ProtoMember(2, Name = "mecha_id")]
	public uint MechaId
	{
		get
		{
			return __pbn__MechaId.GetValueOrDefault();
		}
		set
		{
			__pbn__MechaId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlot()
	{
		return __pbn__Slot.HasValue;
	}

	public void ResetSlot()
	{
		__pbn__Slot = null;
	}

	public bool ShouldSerializeMechaId()
	{
		return __pbn__MechaId.HasValue;
	}

	public void ResetMechaId()
	{
		__pbn__MechaId = null;
	}
}
