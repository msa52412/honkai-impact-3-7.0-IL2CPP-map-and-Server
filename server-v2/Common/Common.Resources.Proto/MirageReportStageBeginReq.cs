using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MirageReportStageBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7004
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__StageId;

	private uint? __pbn__BattleAvatarId;

	private uint? __pbn__DifficultyId;

	[ProtoMember(1, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "stage_id")]
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

	[ProtoMember(3, Name = "battle_avatar_id")]
	public uint BattleAvatarId
	{
		get
		{
			return __pbn__BattleAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleAvatarId = value;
		}
	}

	[ProtoMember(4, Name = "support_avatar_list")]
	public uint[] SupportAvatarLists { get; set; }

	[ProtoMember(5, Name = "core_skill_list")]
	public uint[] CoreSkillLists { get; set; }

	[ProtoMember(6, Name = "difficulty_id")]
	public uint DifficultyId
	{
		get
		{
			return __pbn__DifficultyId.GetValueOrDefault();
		}
		set
		{
			__pbn__DifficultyId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBattleAvatarId()
	{
		return __pbn__BattleAvatarId.HasValue;
	}

	public void ResetBattleAvatarId()
	{
		__pbn__BattleAvatarId = null;
	}

	public bool ShouldSerializeDifficultyId()
	{
		return __pbn__DifficultyId.HasValue;
	}

	public void ResetDifficultyId()
	{
		__pbn__DifficultyId = null;
	}
}
