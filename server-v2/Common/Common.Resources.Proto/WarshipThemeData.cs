using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WarshipThemeData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__WarshipId;

	private uint? __pbn__BgmPlayMode;

	private bool? __pbn__IsWeatherFixed;

	private uint? __pbn__WeatherIdx;

	[ProtoMember(1, Name = "warship_id")]
	public uint WarshipId
	{
		get
		{
			return __pbn__WarshipId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipId = value;
		}
	}

	[ProtoMember(2, Name = "component_list")]
	public List<WarshipComponent> ComponentLists { get; } = new List<WarshipComponent>();


	[ProtoMember(3, Name = "bgm_play_mode")]
	public uint BgmPlayMode
	{
		get
		{
			return __pbn__BgmPlayMode.GetValueOrDefault();
		}
		set
		{
			__pbn__BgmPlayMode = value;
		}
	}

	[ProtoMember(4, Name = "is_weather_fixed")]
	public bool IsWeatherFixed
	{
		get
		{
			return __pbn__IsWeatherFixed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWeatherFixed = value;
		}
	}

	[ProtoMember(5, Name = "weather_idx")]
	public uint WeatherIdx
	{
		get
		{
			return __pbn__WeatherIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__WeatherIdx = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWarshipId()
	{
		return __pbn__WarshipId.HasValue;
	}

	public void ResetWarshipId()
	{
		__pbn__WarshipId = null;
	}

	public bool ShouldSerializeBgmPlayMode()
	{
		return __pbn__BgmPlayMode.HasValue;
	}

	public void ResetBgmPlayMode()
	{
		__pbn__BgmPlayMode = null;
	}

	public bool ShouldSerializeIsWeatherFixed()
	{
		return __pbn__IsWeatherFixed.HasValue;
	}

	public void ResetIsWeatherFixed()
	{
		__pbn__IsWeatherFixed = null;
	}

	public bool ShouldSerializeWeatherIdx()
	{
		return __pbn__WeatherIdx.HasValue;
	}

	public void ResetWeatherIdx()
	{
		__pbn__WeatherIdx = null;
	}
}
