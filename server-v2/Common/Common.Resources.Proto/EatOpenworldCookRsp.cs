using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EatOpenworldCookRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4493
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CYCLE_NOT_OPEN")]
		CycleNotOpen,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "QUEST_LEVEL_LACK")]
		QuestLevelLack,
		[ProtoEnum(Name = "EAT_LIMIT")]
		EatLimit,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "AVATAR_HP_FULL")]
		AvatarHpFull,
		[ProtoEnum(Name = "AVATAR_SP_FULL")]
		AvatarSpFull,
		[ProtoEnum(Name = "AVATAR_DIE")]
		AvatarDie,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
