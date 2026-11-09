using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattlePointCountryData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	private ulong? __pbn__PointScoreU64;

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

	[ProtoMember(2, Name = "point_score_u64")]
	public ulong PointScoreU64
	{
		get
		{
			return __pbn__PointScoreU64.GetValueOrDefault();
		}
		set
		{
			__pbn__PointScoreU64 = value;
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

	public bool ShouldSerializePointScoreU64()
	{
		return __pbn__PointScoreU64.HasValue;
	}

	public void ResetPointScoreU64()
	{
		__pbn__PointScoreU64 = null;
	}
}
