using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CmdTypeReport
{
	[ProtoEnum(Name = "CMD_DUMMY")]
	CmdDummy
}
