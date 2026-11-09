using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyActivateSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 760
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MATERIAL_LIMIT")]
		MaterialLimit,
		[ProtoEnum(Name = "REPEAT_ACTIVATE")]
		RepeatActivate,
		[ProtoEnum(Name = "SKILL_LOCK")]
		SkillLock,
		[ProtoEnum(Name = "NUM_LIMIT")]
		NumLimit
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
