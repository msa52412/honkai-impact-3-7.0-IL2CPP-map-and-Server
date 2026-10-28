using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterHeroCard : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CardId;

	private uint? __pbn__CardLevel;

	private uint? __pbn__CardExp;

	[ProtoMember(1, Name = "card_id")]
	public uint CardId
	{
		get
		{
			return __pbn__CardId.GetValueOrDefault();
		}
		set
		{
			__pbn__CardId = value;
		}
	}

	[ProtoMember(2, Name = "card_level")]
	public uint CardLevel
	{
		get
		{
			return __pbn__CardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CardLevel = value;
		}
	}

	[ProtoMember(3, Name = "card_exp")]
	public uint CardExp
	{
		get
		{
			return __pbn__CardExp.GetValueOrDefault();
		}
		set
		{
			__pbn__CardExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCardId()
	{
		return __pbn__CardId.HasValue;
	}

	public void ResetCardId()
	{
		__pbn__CardId = null;
	}

	public bool ShouldSerializeCardLevel()
	{
		return __pbn__CardLevel.HasValue;
	}

	public void ResetCardLevel()
	{
		__pbn__CardLevel = null;
	}

	public bool ShouldSerializeCardExp()
	{
		return __pbn__CardExp.HasValue;
	}

	public void ResetCardExp()
	{
		__pbn__CardExp = null;
	}
}
