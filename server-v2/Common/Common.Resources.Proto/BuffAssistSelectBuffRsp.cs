using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistSelectBuffRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5946
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NO_BUFF")]
		NoBuff,
		[ProtoEnum(Name = "BUFF_SLOT_ERROR")]
		BuffSlotError,
		[ProtoEnum(Name = "BUFF_SLOT_REPLACED")]
		BuffSlotReplaced,
		[ProtoEnum(Name = "WAIT_SELECT_BUFF_SLOT_ERROR")]
		WaitSelectBuffSlotError,
		[ProtoEnum(Name = "SELECT_TIMES_LACK")]
		SelectTimesLack,
		[ProtoEnum(Name = "BUFF_SLOT_LOCKED")]
		BuffSlotLocked
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
