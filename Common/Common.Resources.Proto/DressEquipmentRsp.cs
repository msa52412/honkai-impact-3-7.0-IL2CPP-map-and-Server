using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DressEquipmentRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 40
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
		[ProtoEnum(Name = "EQUIPMENT_NOT_EXIST")]
		EquipmentNotExist,
		[ProtoEnum(Name = "EQUIPMENT_DRESSED")]
		EquipmentDressed,
		[ProtoEnum(Name = "EQUIPMENT_SLOT_ERROR")]
		EquipmentSlotError,
		[ProtoEnum(Name = "COST_LACK")]
		CostLack,
		[ProtoEnum(Name = "AFFIX_NOT_IDENTIFY")]
		AffixNotIdentify
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
