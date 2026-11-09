using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeEquipforge
{
	[ProtoEnum(Name = "CMD_GET_EQUIPMENT_FORGE_DATA_REQ")]
	CmdGetEquipmentForgeDataReq = 3650,
	[ProtoEnum(Name = "CMD_GET_EQUIPMENT_FORGE_DATA_RSP")]
	CmdGetEquipmentForgeDataRsp = 3651,
	[ProtoEnum(Name = "CMD_FORGE_EQUIPMENT_REQ")]
	CmdForgeEquipmentReq = 3652,
	[ProtoEnum(Name = "CMD_FORGE_EQUIPMENT_RSP")]
	CmdForgeEquipmentRsp = 3653,
	[ProtoEnum(Name = "CMD_EXTRACT_EQUIPMENT_REQ")]
	CmdExtractEquipmentReq = 3654,
	[ProtoEnum(Name = "CMD_EXTRACT_EQUIPMENT_RSP")]
	CmdExtractEquipmentRsp = 3655,
	[ProtoEnum(Name = "CMD_REFORGE_EQUIPMENT_REQ")]
	CmdReforgeEquipmentReq = 3657,
	[ProtoEnum(Name = "CMD_REFORGE_EQUIPMENT_RSP")]
	CmdReforgeEquipmentRsp = 3658,
	[ProtoEnum(Name = "CMD_GET_EXTRACT_REFORGE_ACTIVITY_REQ")]
	CmdGetExtractReforgeActivityReq = 3659,
	[ProtoEnum(Name = "CMD_GET_EXTRACT_REFORGE_ACTIVITY_RSP")]
	CmdGetExtractReforgeActivityRsp = 3660,
	[ProtoEnum(Name = "CMD_GET_EQUIPMENT_BACK_REQ")]
	CmdGetEquipmentBackReq = 3661,
	[ProtoEnum(Name = "CMD_GET_EQUIPMENT_BACK_RSP")]
	CmdGetEquipmentBackRsp = 3662,
	[ProtoEnum(Name = "CMD_BACK_EQUIPMENT_REQ")]
	CmdBackEquipmentReq = 3663,
	[ProtoEnum(Name = "CMD_BACK_EQUIPMENT_RSP")]
	CmdBackEquipmentRsp = 3664
}
