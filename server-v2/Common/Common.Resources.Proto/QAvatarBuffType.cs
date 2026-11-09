using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum QAvatarBuffType
{
	[ProtoEnum(Name = "QAVATAR_BUFF_NONE")]
	QavatarBuffNone = 0,
	[ProtoEnum(Name = "QAVATAR_BUFF_MAXHP")]
	QavatarBuffMaxhp = 1,
	[ProtoEnum(Name = "QAVATAR_BUFF_ATTACK")]
	QavatarBuffAttack = 2,
	[ProtoEnum(Name = "QAVATAR_BUFF_MOVESPEED")]
	QavatarBuffMovespeed = 3,
	[ProtoEnum(Name = "QAVATAR_BUFF_HEALHP")]
	QavatarBuffHealhp = 4,
	[ProtoEnum(Name = "QAVATAR_BUFF_CHANGESKILL")]
	QavatarBuffChangeskill = 5,
	[ProtoEnum(Name = "QAVATAR_BUFF_HEALSP")]
	QavatarBuffHealsp = 6,
	[ProtoEnum(Name = "QAVATAR_BUFF_ADDEDDAMAGERATIO")]
	QavatarBuffAddeddamageratio = 7,
	[ProtoEnum(Name = "QAVATAR_BUFF_DAMAGETAKENRATIO")]
	QavatarBuffDamagetakenratio = 8,
	[ProtoEnum(Name = "QAVATAR_BUFF_CHANGESKILLCD")]
	QavatarBuffChangeskillcd = 9,
	[ProtoEnum(Name = "QAVATAR_BUFF_CRITICALRATE")]
	QavatarBuffCriticalrate = 10,
	[ProtoEnum(Name = "QAVATAR_BUFF_CRITICALDAMAGERATIO")]
	QavatarBuffCriticaldamageratio = 11,
	[ProtoEnum(Name = "QAVATAR_BUFF_CLIENT_ONLY")]
	QavatarBuffClientOnly = 21
}
