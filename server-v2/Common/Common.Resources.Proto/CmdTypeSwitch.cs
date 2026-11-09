using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeSwitch
{
	[ProtoEnum(Name = "CMD_SWITCH_DATA_NOTIFY")]
	CmdSwitchDataNotify = 3700,
	[ProtoEnum(Name = "CMD_SWITCH_CHANGE_DATA_NOTIFY")]
	CmdSwitchChangeDataNotify
}
