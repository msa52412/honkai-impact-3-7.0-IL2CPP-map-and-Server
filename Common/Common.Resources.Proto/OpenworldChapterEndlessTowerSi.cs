using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterEndlessTowerSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__HasTakeRewardScore;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(2, Name = "has_take_reward_score")]
	public uint HasTakeRewardScore
	{
		get
		{
			return __pbn__HasTakeRewardScore.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeHasTakeRewardScore()
	{
		return __pbn__HasTakeRewardScore.HasValue;
	}

	public void ResetHasTakeRewardScore()
	{
		__pbn__HasTakeRewardScore = null;
	}
}
