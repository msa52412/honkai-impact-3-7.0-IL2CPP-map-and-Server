using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum CreditBehavior
{
	[ProtoEnum(Name = "CREDIT_UNCONFIRM")]
	CreditUnconfirm = 1,
	[ProtoEnum(Name = "CREDIT_ON_HOOK")]
	CreditOnHook = 2,
	[ProtoEnum(Name = "CREDIT_TEAM_MATCH")]
	CreditTeamMatch = 3,
	[ProtoEnum(Name = "CREDIT_WIN")]
	CreditWin = 4,
	[ProtoEnum(Name = "CREDIT_LOSE")]
	CreditLose = 5,
	[ProtoEnum(Name = "CREDIT_DAILY_REFRESH")]
	CreditDailyRefresh = 99
}
