using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum ReunionCookBookType
{
	[ProtoEnum(Name = "REUNION_COOK_BOOK_NORMAL")]
	ReunionCookBookNormal = 1,
	[ProtoEnum(Name = "REUNION_COOK_BOOK_RECYCLE")]
	ReunionCookBookRecycle
}
