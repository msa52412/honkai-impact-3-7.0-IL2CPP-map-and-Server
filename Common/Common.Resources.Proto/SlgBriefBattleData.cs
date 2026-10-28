using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBriefBattleData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__BattleScheduleId;

	private uint? __pbn__CountryId;

	private bool? __pbn__IsSettled;

	[ProtoMember(1, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(2, Name = "battle_schedule_id")]
	public uint BattleScheduleId
	{
		get
		{
			return __pbn__BattleScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleScheduleId = value;
		}
	}

	[ProtoMember(3, Name = "country_id")]
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

	[ProtoMember(4, Name = "country_data_list")]
	public List<SlgBattleBriefCountryData> CountryDataLists { get; } = new List<SlgBattleBriefCountryData>();


	[ProtoMember(5, Name = "is_settled")]
	public bool IsSettled
	{
		get
		{
			return __pbn__IsSettled.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSettled = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeBattleScheduleId()
	{
		return __pbn__BattleScheduleId.HasValue;
	}

	public void ResetBattleScheduleId()
	{
		__pbn__BattleScheduleId = null;
	}

	public bool ShouldSerializeCountryId()
	{
		return __pbn__CountryId.HasValue;
	}

	public void ResetCountryId()
	{
		__pbn__CountryId = null;
	}

	public bool ShouldSerializeIsSettled()
	{
		return __pbn__IsSettled.HasValue;
	}

	public void ResetIsSettled()
	{
		__pbn__IsSettled = null;
	}
}
