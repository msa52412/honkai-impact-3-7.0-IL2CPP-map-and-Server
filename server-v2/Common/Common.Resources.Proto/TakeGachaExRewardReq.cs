using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGachaExRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4714
	}

	private IExtension __pbn__extensionData;

	private GachaType? __pbn__GachaType;

	private uint? __pbn__GachaId;

	private uint? __pbn__ExRewardGachaTimes;

	[ProtoMember(1, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(2, Name = "gacha_id")]
	public uint GachaId
	{
		get
		{
			return __pbn__GachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaId = value;
		}
	}

	[ProtoMember(3, Name = "ex_reward_gacha_times")]
	public uint ExRewardGachaTimes
	{
		get
		{
			return __pbn__ExRewardGachaTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ExRewardGachaTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}

	public bool ShouldSerializeExRewardGachaTimes()
	{
		return __pbn__ExRewardGachaTimes.HasValue;
	}

	public void ResetExRewardGachaTimes()
	{
		__pbn__ExRewardGachaTimes = null;
	}
}
