using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetScDLCTowerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6333
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private uint? __pbn__CurFloor;

	private uint? __pbn__MaxFloor;

	private uint? __pbn__MaxScore;

	private uint? __pbn__AvatarReviveTimes;

	private uint? __pbn__ScDlcFeverScore;

	private uint? __pbn__ScDlcClimaxScore;

	private uint? __pbn__MaxCanSweepFloor;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_id")]
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

	[ProtoMember(3, Name = "cur_floor")]
	public uint CurFloor
	{
		get
		{
			return __pbn__CurFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloor = value;
		}
	}

	[ProtoMember(4, Name = "max_floor")]
	public uint MaxFloor
	{
		get
		{
			return __pbn__MaxFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFloor = value;
		}
	}

	[ProtoMember(5, Name = "avatar_info_list")]
	public List<ScDLCTowerAvatarInfo> AvatarInfoLists { get; } = new List<ScDLCTowerAvatarInfo>();


	[ProtoMember(6, Name = "max_score")]
	public uint MaxScore
	{
		get
		{
			return __pbn__MaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScore = value;
		}
	}

	[ProtoMember(7, Name = "avatar_revive_times")]
	public uint AvatarReviveTimes
	{
		get
		{
			return __pbn__AvatarReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarReviveTimes = value;
		}
	}

	[ProtoMember(8, Name = "sc_dlc_fever_score")]
	public uint ScDlcFeverScore
	{
		get
		{
			return __pbn__ScDlcFeverScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcFeverScore = value;
		}
	}

	[ProtoMember(9, Name = "sc_dlc_climax_score")]
	public uint ScDlcClimaxScore
	{
		get
		{
			return __pbn__ScDlcClimaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcClimaxScore = value;
		}
	}

	[ProtoMember(10, Name = "max_can_sweep_floor")]
	public uint MaxCanSweepFloor
	{
		get
		{
			return __pbn__MaxCanSweepFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxCanSweepFloor = value;
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

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}

	public bool ShouldSerializeMaxFloor()
	{
		return __pbn__MaxFloor.HasValue;
	}

	public void ResetMaxFloor()
	{
		__pbn__MaxFloor = null;
	}

	public bool ShouldSerializeMaxScore()
	{
		return __pbn__MaxScore.HasValue;
	}

	public void ResetMaxScore()
	{
		__pbn__MaxScore = null;
	}

	public bool ShouldSerializeAvatarReviveTimes()
	{
		return __pbn__AvatarReviveTimes.HasValue;
	}

	public void ResetAvatarReviveTimes()
	{
		__pbn__AvatarReviveTimes = null;
	}

	public bool ShouldSerializeScDlcFeverScore()
	{
		return __pbn__ScDlcFeverScore.HasValue;
	}

	public void ResetScDlcFeverScore()
	{
		__pbn__ScDlcFeverScore = null;
	}

	public bool ShouldSerializeScDlcClimaxScore()
	{
		return __pbn__ScDlcClimaxScore.HasValue;
	}

	public void ResetScDlcClimaxScore()
	{
		__pbn__ScDlcClimaxScore = null;
	}

	public bool ShouldSerializeMaxCanSweepFloor()
	{
		return __pbn__MaxCanSweepFloor.HasValue;
	}

	public void ResetMaxCanSweepFloor()
	{
		__pbn__MaxCanSweepFloor = null;
	}
}
