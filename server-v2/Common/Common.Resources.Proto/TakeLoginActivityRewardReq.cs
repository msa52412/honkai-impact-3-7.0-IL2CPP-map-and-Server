using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeLoginActivityRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4194
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__TakeDays;

	private LoginActivityRewardType? __pbn__RewardType;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "take_days")]
	public uint TakeDays
	{
		get
		{
			return __pbn__TakeDays.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeDays = value;
		}
	}

	[ProtoMember(3, Name = "reward_type")]
	[DefaultValue(LoginActivityRewardType.LoginActivityRewardNormal)]
	public LoginActivityRewardType RewardType
	{
		get
		{
			return __pbn__RewardType.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeTakeDays()
	{
		return __pbn__TakeDays.HasValue;
	}

	public void ResetTakeDays()
	{
		__pbn__TakeDays = null;
	}

	public bool ShouldSerializeRewardType()
	{
		return __pbn__RewardType.HasValue;
	}

	public void ResetRewardType()
	{
		__pbn__RewardType = null;
	}
}
