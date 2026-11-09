using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public enum SpecialStageId
{
	[ProtoEnum(Name = "STAGE_ID_ENDLESS")]
	StageIdEndless = 900101,
	[ProtoEnum(Name = "STAGE_ID_ROGUE")]
	StageIdRogue = 900201,
	[ProtoEnum(Name = "STAGE_ID_ARMADA")]
	StageIdArmada = 900301,
	[ProtoEnum(Name = "STAGE_ID_ARMADA_MP")]
	StageIdArmadaMp = 900302,
	[ProtoEnum(Name = "STAGE_ID_OPENWORLD")]
	StageIdOpenworld = 900401,
	[ProtoEnum(Name = "STAGE_ID_OPENWORLD_BOSS")]
	StageIdOpenworldBoss = 900402,
	[ProtoEnum(Name = "STAGE_ID_OPENWORLD_DEFENSE")]
	StageIdOpenworldDefense = 900408,
	[ProtoEnum(Name = "STAGE_ID_EXBOSS")]
	StageIdExboss = 900501,
	[ProtoEnum(Name = "STAGE_ID_ARMADA_MATRIX")]
	StageIdArmadaMatrix = 900601,
	[ProtoEnum(Name = "STAGE_ID_SC_DLC_TOWER")]
	StageIdScDlcTower = 900701,
	[ProtoEnum(Name = "STAGE_ID_SEVEN_CHAPTER_CG")]
	StageIdSevenChapterCg = 901000,
	[ProtoEnum(Name = "STAGE_ID_ARMADA_BOSS")]
	StageIdArmadaBoss = 100000000
}
