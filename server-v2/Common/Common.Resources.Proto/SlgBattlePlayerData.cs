using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattlePlayerData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	private uint? __pbn__Score;

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

	[ProtoMember(2, Name = "buff_list")]
	public uint[] BuffLists { get; set; }

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
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

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
