using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixTriggerEventRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2331
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "POS_INVALID")]
		PosInvalid,
		[ProtoEnum(Name = "EVENT_NOT_EXIST")]
		EventNotExist,
		[ProtoEnum(Name = "IN_FLOOR_EVENT")]
		InFloorEvent
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
