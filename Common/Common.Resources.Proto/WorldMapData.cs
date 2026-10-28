using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WorldMapData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WorldMapId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__HighLightMinLevel;

	private uint? __pbn__HighLightMaxLevel;

	private uint? __pbn__Weight;

	private uint? __pbn__AdvanceTime;

	private uint? __pbn__Id;

	[ProtoMember(1, Name = "world_map_id")]
	public uint WorldMapId
	{
		get
		{
			return __pbn__WorldMapId.GetValueOrDefault();
		}
		set
		{
			__pbn__WorldMapId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "high_light_min_level")]
	public uint HighLightMinLevel
	{
		get
		{
			return __pbn__HighLightMinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HighLightMinLevel = value;
		}
	}

	[ProtoMember(5, Name = "high_light_max_level")]
	public uint HighLightMaxLevel
	{
		get
		{
			return __pbn__HighLightMaxLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HighLightMaxLevel = value;
		}
	}

	[ProtoMember(6, Name = "weight")]
	public uint Weight
	{
		get
		{
			return __pbn__Weight.GetValueOrDefault();
		}
		set
		{
			__pbn__Weight = value;
		}
	}

	[ProtoMember(7, Name = "advance_time")]
	public uint AdvanceTime
	{
		get
		{
			return __pbn__AdvanceTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AdvanceTime = value;
		}
	}

	[ProtoMember(8, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWorldMapId()
	{
		return __pbn__WorldMapId.HasValue;
	}

	public void ResetWorldMapId()
	{
		__pbn__WorldMapId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeHighLightMinLevel()
	{
		return __pbn__HighLightMinLevel.HasValue;
	}

	public void ResetHighLightMinLevel()
	{
		__pbn__HighLightMinLevel = null;
	}

	public bool ShouldSerializeHighLightMaxLevel()
	{
		return __pbn__HighLightMaxLevel.HasValue;
	}

	public void ResetHighLightMaxLevel()
	{
		__pbn__HighLightMaxLevel = null;
	}

	public bool ShouldSerializeWeight()
	{
		return __pbn__Weight.HasValue;
	}

	public void ResetWeight()
	{
		__pbn__Weight = null;
	}

	public bool ShouldSerializeAdvanceTime()
	{
		return __pbn__AdvanceTime.HasValue;
	}

	public void ResetAdvanceTime()
	{
		__pbn__AdvanceTime = null;
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}
}
