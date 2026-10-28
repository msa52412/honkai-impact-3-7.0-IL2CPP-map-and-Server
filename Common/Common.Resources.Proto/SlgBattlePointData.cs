using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgBattlePointData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__OccupyCountryId;

	[ProtoMember(1, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(2, Name = "occupy_country_id")]
	public uint OccupyCountryId
	{
		get
		{
			return __pbn__OccupyCountryId.GetValueOrDefault();
		}
		set
		{
			__pbn__OccupyCountryId = value;
		}
	}

	[ProtoMember(3, Name = "point_country_data_list")]
	public List<SlgBattlePointCountryData> PointCountryDataLists { get; } = new List<SlgBattlePointCountryData>();


	[ProtoMember(4, Name = "boss_data")]
	public SlgBattlePointBossData BossData { get; set; }

	[ProtoMember(5, Name = "occupied_country_list")]
	public uint[] OccupiedCountryLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializeOccupyCountryId()
	{
		return __pbn__OccupyCountryId.HasValue;
	}

	public void ResetOccupyCountryId()
	{
		__pbn__OccupyCountryId = null;
	}
}
