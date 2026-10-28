using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ElfUpgradeLevelPeriodRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2110
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ELF_NOT_EXIST")]
		ElfNotExist,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "EXP_NOT_ENOUGH")]
		ExpNotEnough,
		[ProtoEnum(Name = "LEVEL_CAN_NOT_UPGRADE_PERIOD")]
		LevelCanNotUpgradePeriod,
		[ProtoEnum(Name = "PLAYER_LEVEL_NOT_ENOUGH")]
		PlayerLevelNotEnough
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
