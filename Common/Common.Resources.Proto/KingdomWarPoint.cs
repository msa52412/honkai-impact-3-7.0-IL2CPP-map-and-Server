using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarPoint : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__CurKingdomId;

	private uint? __pbn__Score;

	private uint? __pbn__LastKingdomId;

	private uint? __pbn__FinishedNum;

	private uint? __pbn__StageId;

	private uint? __pbn__TodayScore;

	private bool? __pbn__IsOpen;

	[ProtoMember(1, Name = "point_id")]
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

	[ProtoMember(2, Name = "cur_kingdom_id")]
	public uint CurKingdomId
	{
		get
		{
			return __pbn__CurKingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurKingdomId = value;
		}
	}

	[ProtoMember(3, Name = "kingdom_list")]
	public List<KingdomWarPointKingdomData> KingdomLists { get; } = new List<KingdomWarPointKingdomData>();


	[ProtoMember(4, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(5, Name = "last_kingdom_id")]
	public uint LastKingdomId
	{
		get
		{
			return __pbn__LastKingdomId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastKingdomId = value;
		}
	}

	[ProtoMember(6, Name = "finished_num")]
	public uint FinishedNum
	{
		get
		{
			return __pbn__FinishedNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FinishedNum = value;
		}
	}

	[ProtoMember(7, Name = "stage_id")]
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

	[ProtoMember(8, Name = "today_score")]
	public uint TodayScore
	{
		get
		{
			return __pbn__TodayScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayScore = value;
		}
	}

	[ProtoMember(9, Name = "is_open")]
	public bool IsOpen
	{
		get
		{
			return __pbn__IsOpen.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpen = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializeCurKingdomId()
	{
		return __pbn__CurKingdomId.HasValue;
	}

	public void ResetCurKingdomId()
	{
		__pbn__CurKingdomId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeLastKingdomId()
	{
		return __pbn__LastKingdomId.HasValue;
	}

	public void ResetLastKingdomId()
	{
		__pbn__LastKingdomId = null;
	}

	public bool ShouldSerializeFinishedNum()
	{
		return __pbn__FinishedNum.HasValue;
	}

	public void ResetFinishedNum()
	{
		__pbn__FinishedNum = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeTodayScore()
	{
		return __pbn__TodayScore.HasValue;
	}

	public void ResetTodayScore()
	{
		__pbn__TodayScore = null;
	}

	public bool ShouldSerializeIsOpen()
	{
		return __pbn__IsOpen.HasValue;
	}

	public void ResetIsOpen()
	{
		__pbn__IsOpen = null;
	}
}
