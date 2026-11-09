using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChallengeWarActivity : IExtensible
{
	[ProtoContract]
	public class StageAvatarTeam : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__StageId;

		[ProtoMember(1, Name = "stage_id")]
		public uint StageId
		{
			get
			{
				return __pbn__StageId.GetValueOrDefault();
			}
			set
			{
				__pbn__StageId = value;
			}
		}

		[ProtoMember(2, Name = "avatar_id_list")]
		public uint[] AvatarIdLists { get; set; }

		[ProtoMember(3, Name = "support_avatar_id_list")]
		public uint[] SupportAvatarIdLists { get; set; }

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeStageId()
		{
			return __pbn__StageId.HasValue;
		}

		public void ResetStageId()
		{
			__pbn__StageId = null;
		}
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_group_schedule_list")]
	public List<GeneralActivityStageGroupScheduleInfo> StageGroupScheduleLists { get; } = new List<GeneralActivityStageGroupScheduleInfo>();


	[ProtoMember(2, Name = "team_list")]
	public List<StageAvatarTeam> TeamLists { get; } = new List<StageAvatarTeam>();


	[ProtoMember(3, Name = "stage_list")]
	public List<ChallengeWarStage> StageLists { get; } = new List<ChallengeWarStage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
