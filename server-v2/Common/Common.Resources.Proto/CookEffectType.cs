using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CookEffectType
{
	[ProtoEnum(Name = "COOK_ADD_BUFFER")]
	CookAddBuffer = 1,
	[ProtoEnum(Name = "COOK_REVIVE")]
	CookRevive,
	[ProtoEnum(Name = "COOK_RECOVER_ONE_HP")]
	CookRecoverOneHp,
	[ProtoEnum(Name = "COOK_RECOVER_ALL_HP")]
	CookRecoverAllHp,
	[ProtoEnum(Name = "COOK_RECOVER_ONE_SP")]
	CookRecoverOneSp,
	[ProtoEnum(Name = "COOK_RECOVER_ALL_SP")]
	CookRecoverAllSp
}
