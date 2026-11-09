using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeySetSkillRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 764
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_ACTIVE")]
		NotActive,
		[ProtoEnum(Name = "SKILL_LOCKED")]
		SkillLocked,
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
