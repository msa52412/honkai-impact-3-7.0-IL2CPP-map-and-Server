using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CrisisModeActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_group_schedule_list")]
	public List<GeneralActivityStageGroupScheduleInfo> StageGroupScheduleLists { get; } = new List<GeneralActivityStageGroupScheduleInfo>();


	[ProtoMember(2, Name = "stage_reward_taken_list")]
	public uint[] StageRewardTakenLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
