using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistSelectBuffReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5945
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BuffSlotId;

	private uint? __pbn__WaitSelectBuffSlotId;

	[ProtoMember(1, Name = "buff_slot_id")]
	public uint BuffSlotId
	{
		get
		{
			return __pbn__BuffSlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffSlotId = value;
		}
	}

	[ProtoMember(2, Name = "wait_select_buff_slot_id")]
	public uint WaitSelectBuffSlotId
	{
		get
		{
			return __pbn__WaitSelectBuffSlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitSelectBuffSlotId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBuffSlotId()
	{
		return __pbn__BuffSlotId.HasValue;
	}

	public void ResetBuffSlotId()
	{
		__pbn__BuffSlotId = null;
	}

	public bool ShouldSerializeWaitSelectBuffSlotId()
	{
		return __pbn__WaitSelectBuffSlotId.HasValue;
	}

	public void ResetWaitSelectBuffSlotId()
	{
		__pbn__WaitSelectBuffSlotId = null;
	}
}
