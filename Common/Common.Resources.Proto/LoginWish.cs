using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LoginWish : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LoginDay;

	private uint? __pbn__WishId;

	private uint? __pbn__ChooseRewardId;

	private bool? __pbn__IsRewardTaken;

	[ProtoMember(1, Name = "login_day")]
	public uint LoginDay
	{
		get
		{
			return __pbn__LoginDay.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginDay = value;
		}
	}

	[ProtoMember(2, Name = "wish_id")]
	public uint WishId
	{
		get
		{
			return __pbn__WishId.GetValueOrDefault();
		}
		set
		{
			__pbn__WishId = value;
		}
	}

	[ProtoMember(3, Name = "choose_reward_id")]
	public uint ChooseRewardId
	{
		get
		{
			return __pbn__ChooseRewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChooseRewardId = value;
		}
	}

	[ProtoMember(4, Name = "is_reward_taken")]
	public bool IsRewardTaken
	{
		get
		{
			return __pbn__IsRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsRewardTaken = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLoginDay()
	{
		return __pbn__LoginDay.HasValue;
	}

	public void ResetLoginDay()
	{
		__pbn__LoginDay = null;
	}

	public bool ShouldSerializeWishId()
	{
		return __pbn__WishId.HasValue;
	}

	public void ResetWishId()
	{
		__pbn__WishId = null;
	}

	public bool ShouldSerializeChooseRewardId()
	{
		return __pbn__ChooseRewardId.HasValue;
	}

	public void ResetChooseRewardId()
	{
		__pbn__ChooseRewardId = null;
	}

	public bool ShouldSerializeIsRewardTaken()
	{
		return __pbn__IsRewardTaken.HasValue;
	}

	public void ResetIsRewardTaken()
	{
		__pbn__IsRewardTaken = null;
	}
}
