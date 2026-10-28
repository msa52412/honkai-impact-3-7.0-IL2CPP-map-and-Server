using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AccountBuffReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoinReward;

	private uint? __pbn__AvatarExpReward;

	private uint? __pbn__PlayerExpReward;

	[ProtoMember(1, Name = "scoin_reward")]
	public uint ScoinReward
	{
		get
		{
			return __pbn__ScoinReward.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoinReward = value;
		}
	}

	[ProtoMember(2, Name = "avatar_exp_reward")]
	public uint AvatarExpReward
	{
		get
		{
			return __pbn__AvatarExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarExpReward = value;
		}
	}

	[ProtoMember(3, Name = "player_exp_reward")]
	public uint PlayerExpReward
	{
		get
		{
			return __pbn__PlayerExpReward.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerExpReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoinReward()
	{
		return __pbn__ScoinReward.HasValue;
	}

	public void ResetScoinReward()
	{
		__pbn__ScoinReward = null;
	}

	public bool ShouldSerializeAvatarExpReward()
	{
		return __pbn__AvatarExpReward.HasValue;
	}

	public void ResetAvatarExpReward()
	{
		__pbn__AvatarExpReward = null;
	}

	public bool ShouldSerializePlayerExpReward()
	{
		return __pbn__PlayerExpReward.HasValue;
	}

	public void ResetPlayerExpReward()
	{
		__pbn__PlayerExpReward = null;
	}
}
