using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum PrivilegeProductType
{
	[ProtoEnum(Name = "PRIVILEGE_PRODUCT_ALL")]
	PrivilegeProductAll,
	[ProtoEnum(Name = "PRIVILEGE_PRODUCT_NEWBIE")]
	PrivilegeProductNewbie,
	[ProtoEnum(Name = "PRIVILEGE_PRODUCT_GOBACK")]
	PrivilegeProductGoback,
	[ProtoEnum(Name = "PRIVILEGE_PRODUCT_ADVENTURE")]
	PrivilegeProductAdventure
}
