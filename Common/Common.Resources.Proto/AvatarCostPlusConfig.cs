using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarCostPlusConfig : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Star;

	private uint? __pbn__CostPlus;

	[ProtoMember(1, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(2, Name = "cost_plus")]
	public uint CostPlus
	{
		get
		{
			return __pbn__CostPlus.GetValueOrDefault();
		}
		set
		{
			__pbn__CostPlus = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeCostPlus()
	{
		return __pbn__CostPlus.HasValue;
	}

	public void ResetCostPlus()
	{
		__pbn__CostPlus = null;
	}
}
