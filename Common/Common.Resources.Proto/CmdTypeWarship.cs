using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeWarship
{
	[ProtoEnum(Name = "CMD_GET_WARSHIP_ITEM_DATA_REQ")]
	CmdGetWarshipItemDataReq = 5450,
	[ProtoEnum(Name = "CMD_GET_WARSHIP_ITEM_DATA_RSP")]
	CmdGetWarshipItemDataRsp,
	[ProtoEnum(Name = "CMD_GET_WARSHIP_TRIAL_DATA_REQ")]
	CmdGetWarshipTrialDataReq,
	[ProtoEnum(Name = "CMD_GET_WARSHIP_TRIAL_DATA_RSP")]
	CmdGetWarshipTrialDataRsp,
	[ProtoEnum(Name = "CMD_GET_WARSHIP_DATA_REQ")]
	CmdGetWarshipDataReq,
	[ProtoEnum(Name = "CMD_GET_WARSHIP_DATA_RSP")]
	CmdGetWarshipDataRsp,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_REQ")]
	CmdSetWarshipReq,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_RSP")]
	CmdSetWarshipRsp,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_COMPONENT_REQ")]
	CmdSetWarshipComponentReq,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_COMPONENT_RSP")]
	CmdSetWarshipComponentRsp,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_SETTING_REQ")]
	CmdSetWarshipSettingReq,
	[ProtoEnum(Name = "CMD_SET_WARSHIP_SETTING_RSP")]
	CmdSetWarshipSettingRsp,
	[ProtoEnum(Name = "CMD_WARSHIP_ITEM_RETURN_MATERIAL_NOTIFY")]
	CmdWarshipItemReturnMaterialNotify
}
