using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattleBriefCountryData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	private uint? __pbn__CountryScore;

	[ProtoMember(1, Name = "country_id")]
	public uint CountryId
	{
		get
		{
			return __pbn__CountryId.GetValueOrDefault();
		}
		set
		{
			__pbn__CountryId = value;
		}
	}

	[ProtoMember(2, Name = "country_score")]
	public uint CountryScore
	{
		get
		{
			return __pbn__CountryScore.GetValueOrDefault();
		}
		set
		{
			__pbn__CountryScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCountryId()
	{
		return __pbn__CountryId.HasValue;
	}

	public void ResetCountryId()
	{
		__pbn__CountryId = null;
	}

	public bool ShouldSerializeCountryScore()
	{
		return __pbn__CountryScore.HasValue;
	}

	public void ResetCountryScore()
	{
		__pbn__CountryScore = null;
	}
}
