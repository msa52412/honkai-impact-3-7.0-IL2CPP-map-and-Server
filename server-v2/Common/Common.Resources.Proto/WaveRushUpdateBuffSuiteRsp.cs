using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WaveRushUpdateBuffSuiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5968
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "BUFF_ERROR")]
		BuffError,
		[ProtoEnum(Name = "SLOT_LACK")]
		SlotLack,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError,
		[ProtoEnum(Name = "SUITE_ID_ERROR")]
		SuiteIdError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
