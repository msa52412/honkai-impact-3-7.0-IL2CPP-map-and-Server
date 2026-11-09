using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeRollRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 645
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__RewardGroupId;

	[ProtoMember(1, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(2, Name = "reward_group_id")]
	public uint RewardGroupId
	{
		get
		{
			return __pbn__RewardGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeRewardGroupId()
	{
		return __pbn__RewardGroupId.HasValue;
	}

	public void ResetRewardGroupId()
	{
		__pbn__RewardGroupId = null;
	}
}
