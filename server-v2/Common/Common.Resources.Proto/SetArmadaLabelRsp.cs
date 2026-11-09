using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetArmadaLabelRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2841
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "LABEL_ERROR")]
		LabelError,
		[ProtoEnum(Name = "PREFIX_ERROR")]
		PrefixError,
		[ProtoEnum(Name = "FORMAT_ERROR")]
		FormatError,
		[ProtoEnum(Name = "HEXIE_OPEN")]
		HexieOpen,
		[ProtoEnum(Name = "SENSITIVE_WORDS")]
		SensitiveWords
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "label")]
	public ArmadaLabel Label { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
