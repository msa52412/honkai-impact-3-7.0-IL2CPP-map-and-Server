using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TimeLimitAddUpRewardGrantNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6737
	}

	private IExtension __pbn__extensionData;

	private TimeLimitAddUpType? __pbn__Type;

	private uint? __pbn__AddUpValueNeed;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(TimeLimitAddUpType.AddUpVipPointAll)]
	public TimeLimitAddUpType Type
	{
		get
		{
			return __pbn__Type ?? TimeLimitAddUpType.AddUpVipPointAll;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "add_up_value_need")]
	public uint AddUpValueNeed
	{
		get
		{
			return __pbn__AddUpValueNeed.GetValueOrDefault();
		}
		set
		{
			__pbn__AddUpValueNeed = value;
		}
	}

	[ProtoMember(3, Name = "reward")]
	public RewardData Reward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeAddUpValueNeed()
	{
		return __pbn__AddUpValueNeed.HasValue;
	}

	public void ResetAddUpValueNeed()
	{
		__pbn__AddUpValueNeed = null;
	}
}
