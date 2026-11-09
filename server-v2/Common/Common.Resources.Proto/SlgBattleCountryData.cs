using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattleCountryData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CountryId;

	private uint? __pbn__TakeDownBossNum;

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

	[ProtoMember(2, Name = "take_down_boss_num")]
	public uint TakeDownBossNum
	{
		get
		{
			return __pbn__TakeDownBossNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeDownBossNum = value;
		}
	}

	[ProtoMember(3, Name = "buff_list")]
	public List<SlgBattleBuffData> BuffLists { get; } = new List<SlgBattleBuffData>();


	[ProtoMember(4, Name = "country_score")]
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

	public bool ShouldSerializeTakeDownBossNum()
	{
		return __pbn__TakeDownBossNum.HasValue;
	}

	public void ResetTakeDownBossNum()
	{
		__pbn__TakeDownBossNum = null;
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
