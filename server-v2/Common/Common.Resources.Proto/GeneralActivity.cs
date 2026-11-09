using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "general_basic_info")]
	public GeneralActivityBasicInfo GeneralBasicInfo { get; set; }

	[ProtoMember(2, Name = "settle_info")]
	public GeneralActivitySettleInfo SettleInfo { get; set; }

	[ProtoMember(3, Name = "push_tower_info")]
	public PushTowerActivityInfo PushTowerInfo { get; set; }

	[ProtoMember(4, Name = "team_assault_info")]
	public TeamAssaultActivityInfo TeamAssaultInfo { get; set; }

	[ProtoMember(5, Name = "activity_stage")]
	public GeneralActivityStage ActivityStage { get; set; }

	[ProtoMember(6, Name = "couple_tower")]
	public CoupleTowerActivity CoupleTower { get; set; }

	[ProtoMember(7, Name = "endless_single")]
	public EndlessSingleModeActivity EndlessSingle { get; set; }

	[ProtoMember(8, Name = "single_raid")]
	public SingleRaidActivity SingleRaid { get; set; }

	[ProtoMember(9, Name = "optional_buff")]
	public OptionalBuffActivity OptionalBuff { get; set; }

	[ProtoMember(10, Name = "challenge_war")]
	public ChallengeWarActivity ChallengeWar { get; set; }

	[ProtoMember(11, Name = "crisis_mode")]
	public CrisisModeActivity CrisisMode { get; set; }

	[ProtoMember(12, Name = "scheduled_map_site")]
	public ScheduledMapSiteActivity ScheduledMapSite { get; set; }

	[ProtoMember(13, Name = "boss_challenge")]
	public BossChallengeActivity BossChallenge { get; set; }

	[ProtoMember(14, Name = "front_endless")]
	public FrontEndlessActivity FrontEndless { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
