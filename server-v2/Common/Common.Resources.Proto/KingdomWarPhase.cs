using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarPhase : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PhaseId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__TodayBeginTime;

	private uint? __pbn__TodayEndTime;

	private uint? __pbn__AssistPoint;

	private uint? __pbn__LastRefreshTime;

	[ProtoMember(1, Name = "phase_id")]
	public uint PhaseId
	{
		get
		{
			return __pbn__PhaseId.GetValueOrDefault();
		}
		set
		{
			__pbn__PhaseId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "today_begin_time")]
	public uint TodayBeginTime
	{
		get
		{
			return __pbn__TodayBeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayBeginTime = value;
		}
	}

	[ProtoMember(5, Name = "today_end_time")]
	public uint TodayEndTime
	{
		get
		{
			return __pbn__TodayEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayEndTime = value;
		}
	}

	[ProtoMember(6, Name = "point_list")]
	public List<KingdomWarPoint> PointLists { get; } = new List<KingdomWarPoint>();


	[ProtoMember(7, Name = "kingdom_list")]
	public List<KingdomWarPhaseKingdomData> KingdomLists { get; } = new List<KingdomWarPhaseKingdomData>();


	[ProtoMember(8, Name = "player_data")]
	public KingdomWarPhasePlayerData PlayerData { get; set; }

	[ProtoMember(9, Name = "assist_point")]
	public uint AssistPoint
	{
		get
		{
			return __pbn__AssistPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistPoint = value;
		}
	}

	[ProtoMember(10, Name = "last_refresh_time")]
	public uint LastRefreshTime
	{
		get
		{
			return __pbn__LastRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRefreshTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePhaseId()
	{
		return __pbn__PhaseId.HasValue;
	}

	public void ResetPhaseId()
	{
		__pbn__PhaseId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeTodayBeginTime()
	{
		return __pbn__TodayBeginTime.HasValue;
	}

	public void ResetTodayBeginTime()
	{
		__pbn__TodayBeginTime = null;
	}

	public bool ShouldSerializeTodayEndTime()
	{
		return __pbn__TodayEndTime.HasValue;
	}

	public void ResetTodayEndTime()
	{
		__pbn__TodayEndTime = null;
	}

	public bool ShouldSerializeAssistPoint()
	{
		return __pbn__AssistPoint.HasValue;
	}

	public void ResetAssistPoint()
	{
		__pbn__AssistPoint = null;
	}

	public bool ShouldSerializeLastRefreshTime()
	{
		return __pbn__LastRefreshTime.HasValue;
	}

	public void ResetLastRefreshTime()
	{
		__pbn__LastRefreshTime = null;
	}
}
