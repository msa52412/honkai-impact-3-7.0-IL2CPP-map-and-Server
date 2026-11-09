using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UpdateScDLCFeverSuiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6305
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords,
		[ProtoEnum(Name = "SLOT_LACK")]
		SlotLack,
		[ProtoEnum(Name = "INVALID_SKILL_CHIP")]
		InvalidSkillChip,
		[ProtoEnum(Name = "NAME_FORMAT_ERROR")]
		NameFormatError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
