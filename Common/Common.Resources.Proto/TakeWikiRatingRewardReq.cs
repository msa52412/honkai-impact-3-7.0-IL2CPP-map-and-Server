using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeWikiRatingRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1195
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RatingId;

	private uint? __pbn__RatingScore;

	[ProtoMember(1, Name = "rating_id")]
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

	[ProtoMember(2, Name = "rating_score")]
	public uint RatingScore
	{
		get
		{
			return __pbn__RatingScore.GetValueOrDefault();
		}
		set
		{
			__pbn__RatingScore = value;
		}
	}

	[ProtoMember(3, Name = "cg_list")]
	public uint[] CgLists { get; set; }

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

	public bool ShouldSerializeRatingScore()
	{
		return __pbn__RatingScore.HasValue;
	}

	public void ResetRatingScore()
	{
		__pbn__RatingScore = null;
	}
}
