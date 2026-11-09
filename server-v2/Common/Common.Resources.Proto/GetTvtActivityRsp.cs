using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetTvtActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3301
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
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "PLAYER_LEVEL_LACK")]
		PlayerLevelLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurSeasonId;

	private uint? __pbn__SeasonBeginTime;

	private uint? __pbn__SeasonEndTime;

	private uint? __pbn__PunishEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "tvt_activity_list")]
	public List<TvtActivity> TvtActivityLists { get; } = new List<TvtActivity>();


	[ProtoMember(3, Name = "cur_season_id")]
	public uint CurSeasonId
	{
		get
		{
			return __pbn__CurSeasonId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurSeasonId = value;
		}
	}

	[ProtoMember(4, Name = "season_begin_time")]
	public uint SeasonBeginTime
	{
		get
		{
			return __pbn__SeasonBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SeasonBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "season_end_time")]
	public uint SeasonEndTime
	{
		get
		{
			return __pbn__SeasonEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__SeasonEndTime = value;
		}
	}

	[ProtoMember(6, Name = "punish_end_time")]
	public uint PunishEndTime
	{
		get
		{
			return __pbn__PunishEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PunishEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurSeasonId()
	{
		return __pbn__CurSeasonId.HasValue;
	}

	public void ResetCurSeasonId()
	{
		__pbn__CurSeasonId = null;
	}

	public bool ShouldSerializeSeasonBeginTime()
	{
		return __pbn__SeasonBeginTime.HasValue;
	}

	public void ResetSeasonBeginTime()
	{
		__pbn__SeasonBeginTime = null;
	}

	public bool ShouldSerializeSeasonEndTime()
	{
		return __pbn__SeasonEndTime.HasValue;
	}

	public void ResetSeasonEndTime()
	{
		__pbn__SeasonEndTime = null;
	}

	public bool ShouldSerializePunishEndTime()
	{
		return __pbn__PunishEndTime.HasValue;
	}

	public void ResetPunishEndTime()
	{
		__pbn__PunishEndTime = null;
	}
}
