using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HeroSpiritsEventNotice : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HeroId;

	private bool? __pbn__IsAlreadyNotice;

	[ProtoMember(1, Name = "hero_id")]
	public uint HeroId
	{
		get
		{
			return __pbn__HeroId.GetValueOrDefault();
		}
		set
		{
			__pbn__HeroId = value;
		}
	}

	[ProtoMember(2, Name = "is_already_notice")]
	public bool IsAlreadyNotice
	{
		get
		{
			return __pbn__IsAlreadyNotice.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAlreadyNotice = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHeroId()
	{
		return __pbn__HeroId.HasValue;
	}

	public void ResetHeroId()
	{
		__pbn__HeroId = null;
	}

	public bool ShouldSerializeIsAlreadyNotice()
	{
		return __pbn__IsAlreadyNotice.HasValue;
	}

	public void ResetIsAlreadyNotice()
	{
		__pbn__IsAlreadyNotice = null;
	}
}
