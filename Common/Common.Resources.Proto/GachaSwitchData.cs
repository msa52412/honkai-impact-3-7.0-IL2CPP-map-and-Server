using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaSwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaId;

	private uint? __pbn__GachaType;

	[ProtoMember(1, Name = "gacha_id")]
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

	[ProtoMember(2, Name = "gacha_type")]
	public uint GachaType
	{
		get
		{
			return __pbn__GachaType.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaId()
	{
		return __pbn__GachaId.HasValue;
	}

	public void ResetGachaId()
	{
		__pbn__GachaId = null;
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}
}
