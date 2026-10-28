using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SideMissionCond
{
	[ProtoEnum(Name = "SIDE_MISSION_COND_AVATAR_GOODFEEL")]
	SideMissionCondAvatarGoodfeel = 1
}
