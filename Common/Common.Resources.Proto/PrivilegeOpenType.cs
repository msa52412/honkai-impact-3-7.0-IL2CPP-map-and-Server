using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PrivilegeOpenType
{
	[ProtoEnum(Name = "PRIVILEGE_OPEN_NORMAL")]
	PrivilegeOpenNormal,
	[ProtoEnum(Name = "PRIVILEGE_OPEN_SCHEDULED")]
	PrivilegeOpenScheduled
}
