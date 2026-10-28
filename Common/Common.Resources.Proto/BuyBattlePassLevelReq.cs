using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyBattlePassLevelReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3756
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetLevel;

	private uint? __pbn__HcoinCost;

	[ProtoMember(1, Name = "target_level")]
	public uint TargetLevel
	{
		get
		{
			return __pbn__TargetLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLevel = value;
		}
	}

	[ProtoMember(2, Name = "hcoin_cost")]
	public uint HcoinCost
	{
		get
		{
			return __pbn__HcoinCost.GetValueOrDefault();
		}
		set
		{
			__pbn__HcoinCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetLevel()
	{
		return __pbn__TargetLevel.HasValue;
	}

	public void ResetTargetLevel()
	{
		__pbn__TargetLevel = null;
	}

	public bool ShouldSerializeHcoinCost()
	{
		return __pbn__HcoinCost.HasValue;
	}

	public void ResetHcoinCost()
	{
		__pbn__HcoinCost = null;
	}
}
