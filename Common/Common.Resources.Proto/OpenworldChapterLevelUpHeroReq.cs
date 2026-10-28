using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterLevelUpHeroReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4583
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HeroId;

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
}
