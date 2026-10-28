using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EquipDLCAvatarTalentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3555
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "TALENT_NOT_EXIST")]
		TalentNotExist,
		[ProtoEnum(Name = "TALENT_TYPE_ERROR")]
		TalentTypeError,
		[ProtoEnum(Name = "TALENT_TAG_REPEAT")]
		TalentTagRepeat
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
