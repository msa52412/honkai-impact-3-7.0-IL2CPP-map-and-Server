using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeArmadaReunionRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2837
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RewardId;

	[ProtoMember(1, Name = "reward_id")]
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

	public bool ShouldSerializeRewardId()
	{
		return __pbn__RewardId.HasValue;
	}

	public void ResetRewardId()
	{
		__pbn__RewardId = null;
	}
}
