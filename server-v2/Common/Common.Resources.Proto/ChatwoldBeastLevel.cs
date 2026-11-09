using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ChatwoldBeastLevel
{
	[ProtoEnum(Name = "CHATWORLD_BEAST_SENIOR")]
	ChatworldBeastSenior = 1,
	[ProtoEnum(Name = "CHATWORLD_BEAST_JUNIOR")]
	ChatworldBeastJunior
}
