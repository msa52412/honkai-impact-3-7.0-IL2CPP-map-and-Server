using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CGType
{
	[ProtoEnum(Name = "CG_NONE")]
	CgNone,
	[ProtoEnum(Name = "CG_START")]
	CgStart,
	[ProtoEnum(Name = "CG_SEVEN_CHAPTER")]
	CgSevenChapter
}
