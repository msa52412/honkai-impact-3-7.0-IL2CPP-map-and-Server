using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BwWorldCampActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "schedule_id")]
	public uint ScheduleId { get; set; }

	[ProtoMember(2, Name = "camp_type")]
	public BwWorldCampActivityCampType CampType { get; set; }

	[ProtoMember(3, Name = "daily_stage_drop_contribution")]
	public uint DailyStageDropContribution { get; set; }

	[ProtoMember(4, Name = "total_stage_drop_contribution")]
	public uint TotalStageDropContribution { get; set; }

	[ProtoMember(5, Name = "wait_take_assist_contribution")]
	public uint WaitTakeAssistContribution { get; set; }

	[ProtoMember(6, Name = "total_assist_contribution")]
	public uint TotalAssistContribution { get; set; }

	[ProtoMember(7, Name = "assist_rune_list")]
	public List<BwWorldCampActivityAssistRune> AssistRuneLists { get; } = new List<BwWorldCampActivityAssistRune>();


	[ProtoMember(8, Name = "publish_rune_unique_id")]
	public uint PublishRuneUniqueId { get; set; }

	[ProtoMember(9, Name = "global_phas")]
	public uint GlobalPhas { get; set; }

	[ProtoMember(10, Name = "personal_phase")]
	public uint PersonalPhase { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
