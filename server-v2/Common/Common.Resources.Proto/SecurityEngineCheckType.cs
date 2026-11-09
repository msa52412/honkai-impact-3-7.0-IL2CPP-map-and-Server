using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SecurityEngineCheckType
{
	[ProtoEnum(Name = "SECURITY_ENGINE_CHECK_NONE")]
	SecurityEngineCheckNone,
	[ProtoEnum(Name = "SECURITY_ENGINE_CHECK_LOGIN")]
	SecurityEngineCheckLogin,
	[ProtoEnum(Name = "SECURITY_ENGINE_CHECK_STAGE")]
	SecurityEngineCheckStage
}
