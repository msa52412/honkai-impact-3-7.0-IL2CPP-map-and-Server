using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishFarmProduceReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1391
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private bool? __pbn__IsFinishAll;

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

	[ProtoMember(2, Name = "is_finish_all")]
	public bool IsFinishAll
	{
		get
		{
			return __pbn__IsFinishAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFinishAll = value;
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

	public bool ShouldSerializeIsFinishAll()
	{
		return __pbn__IsFinishAll.HasValue;
	}

	public void ResetIsFinishAll()
	{
		__pbn__IsFinishAll = null;
	}
}
