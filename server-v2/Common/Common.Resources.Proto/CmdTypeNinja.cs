using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeNinja
{
	[ProtoEnum(Name = "CMD_GET_NINJA_ACTIVITY_REQ")]
	CmdGetNinjaActivityReq = 4400,
	[ProtoEnum(Name = "CMD_GET_NINJA_ACTIVITY_RSP")]
	CmdGetNinjaActivityRsp = 4401,
	[ProtoEnum(Name = "CMD_NINJA_SLOT_STRENGTHEN_REQ")]
	CmdNinjaSlotStrengthenReq = 4402,
	[ProtoEnum(Name = "CMD_NINJA_SLOT_STRENGTHEN_RSP")]
	CmdNinjaSlotStrengthenRsp = 4403,
	[ProtoEnum(Name = "CMD_NINJA_ACTIVITY_NOTIFY")]
	CmdNinjaActivityNotify = 4404,
	[ProtoEnum(Name = "CMD_NINJA_SLOT_CHOOSE_EFFECT_REQ")]
	CmdNinjaSlotChooseEffectReq = 4405,
	[ProtoEnum(Name = "CMD_NINJA_SLOT_CHOOSE_EFFECT_RSP")]
	CmdNinjaSlotChooseEffectRsp = 4406,
	[ProtoEnum(Name = "CMD_NINJA_DUMMY")]
	CmdNinjaDummy = 4449
}
