using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OffLineTagType
{
	[ProtoEnum(Name = "OFFLINE_TAG_TYPE_NORMAL")]
	OfflineTagTypeNormal,
	[ProtoEnum(Name = "OFFLINE_TAG_TYPE_SMURF")]
	OfflineTagTypeSmurf
}
