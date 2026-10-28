using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterHero : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HeroId;

	private uint? __pbn__HeroLevel;

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

	[ProtoMember(2, Name = "hero_level")]
	public uint HeroLevel
	{
		get
		{
			return __pbn__HeroLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HeroLevel = value;
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

	public bool ShouldSerializeHeroLevel()
	{
		return __pbn__HeroLevel.HasValue;
	}

	public void ResetHeroLevel()
	{
		__pbn__HeroLevel = null;
	}
}
