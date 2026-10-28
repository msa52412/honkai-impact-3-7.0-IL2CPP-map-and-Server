using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgGetPointRankRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6065
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule,
		[ProtoEnum(Name = "NOT_IN_BATTLE")]
		NotInBattle,
		[ProtoEnum(Name = "POINT_ERROR")]
		PointError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleScheduleId;

	private uint? __pbn__PointId;

	private uint? __pbn__PlayerScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "battle_schedule_id")]
	public uint BattleScheduleId
	{
		get
		{
			return __pbn__BattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(4, Name = "player_score")]
	public uint PlayerScore
	{
		get
		{
			return __pbn__PlayerScore.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerScore = value;
		}
	}

	[ProtoMember(5, Name = "rank_list")]
	public List<UserRankData> RankLists { get; } = new List<UserRankData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleScheduleId()
	{
		return __pbn__BattleScheduleId.HasValue;
	}

	public void ResetBattleScheduleId()
	{
		__pbn__BattleScheduleId = null;
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializePlayerScore()
	{
		return __pbn__PlayerScore.HasValue;
	}

	public void ResetPlayerScore()
	{
		__pbn__PlayerScore = null;
	}
}
