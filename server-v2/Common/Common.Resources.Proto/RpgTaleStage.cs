using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__MaxFloor;

	private uint? __pbn__MaxScore;

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

	[ProtoMember(2, Name = "max_floor")]
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

	[ProtoMember(3, Name = "max_score")]
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

	[ProtoMember(5, Name = "score_drop")]
	public RpgTaleStageScoreDrop ScoreDrop { get; set; }

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
}
