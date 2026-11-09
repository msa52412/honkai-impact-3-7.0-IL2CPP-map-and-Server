using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaReunionActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2835
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "mission_list")]
	public List<ArmadaReunionMission> MissionLists { get; } = new List<ArmadaReunionMission>();


	[ProtoMember(4, Name = "player_list")]
	public List<ArmadaReunionPlayer> PlayerLists { get; } = new List<ArmadaReunionPlayer>();


	[ProtoMember(5, Name = "reward_list")]
	public List<ChatworldDishReward> RewardLists { get; } = new List<ChatworldDishReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}
}
