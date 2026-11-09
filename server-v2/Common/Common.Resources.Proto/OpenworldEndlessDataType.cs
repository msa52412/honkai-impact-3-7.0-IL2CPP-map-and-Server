using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum OpenworldEndlessDataType
{
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_DATA_TYPE_INVAISON")]
	OpenworldEndlessDataTypeInvaison = 1,
	[ProtoEnum(Name = "OPENWORLD_ENDLESS_DATA_TYPE_SCENE_GEAR")]
	OpenworldEndlessDataTypeSceneGear
}
