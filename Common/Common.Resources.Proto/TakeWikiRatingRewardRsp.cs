using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeWikiRatingRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1196
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "HAS_TKAE")]
		HasTkae = 3,
		[ProtoEnum(Name = "SCORE_LACK")]
		ScoreLack = 4
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RatingId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "rating_id")]
	public uint RatingId
	{
		get
		{
			return __pbn__RatingId.GetValueOrDefault();
		}
		set
		{
			__pbn__RatingId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRatingId()
	{
		return __pbn__RatingId.HasValue;
	}

	public void ResetRatingId()
	{
		__pbn__RatingId = null;
	}
}
