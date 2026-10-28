using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AiCyberArea : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private bool? __pbn__IsPuzzleRewardTaken;

	private uint? __pbn__CurDailyStageId;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(2, Name = "is_puzzle_reward_taken")]
	public bool IsPuzzleRewardTaken
	{
		get
		{
			return __pbn__IsPuzzleRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPuzzleRewardTaken = value;
		}
	}

	[ProtoMember(3, Name = "cur_daily_stage_id")]
	public uint CurDailyStageId
	{
		get
		{
			return __pbn__CurDailyStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurDailyStageId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeIsPuzzleRewardTaken()
	{
		return __pbn__IsPuzzleRewardTaken.HasValue;
	}

	public void ResetIsPuzzleRewardTaken()
	{
		__pbn__IsPuzzleRewardTaken = null;
	}

	public bool ShouldSerializeCurDailyStageId()
	{
		return __pbn__CurDailyStageId.HasValue;
	}

	public void ResetCurDailyStageId()
	{
		__pbn__CurDailyStageId = null;
	}
}
