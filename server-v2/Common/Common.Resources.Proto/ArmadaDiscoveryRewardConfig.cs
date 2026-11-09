using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaDiscoveryRewardConfig : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Discovery;

	private uint? __pbn__RewardId;

	[ProtoMember(1, Name = "discovery")]
	public uint Discovery
	{
		get
		{
			return __pbn__Discovery.GetValueOrDefault();
		}
		set
		{
			__pbn__Discovery = value;
		}
	}

	[ProtoMember(2, Name = "reward_id")]
	public uint RewardId
	{
		get
		{
			return __pbn__RewardId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDiscovery()
	{
		return __pbn__Discovery.HasValue;
	}

	public void ResetDiscovery()
	{
		__pbn__Discovery = null;
	}

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}
}
