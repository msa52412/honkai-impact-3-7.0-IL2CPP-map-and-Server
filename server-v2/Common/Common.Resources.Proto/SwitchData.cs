using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchData : IExtensible
{
	private IExtension __pbn__extensionData;

	private FeatureSwitchType? __pbn__Type;

	private byte[] __pbn__Data;

	private uint? __pbn__MinLevel;

	private uint? __pbn__MaxLevel;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(FeatureSwitchType.FeatureSwitchNone)]
	public FeatureSwitchType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "data")]
	public byte[] Data
	{
		get
		{
			return __pbn__Data;
		}
		set
		{
			__pbn__Data = value;
		}
	}

	[ProtoMember(3, Name = "min_level")]
	public uint MinLevel
	{
		get
		{
			return __pbn__MinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinLevel = value;
		}
	}

	[ProtoMember(4, Name = "max_level")]
	public uint MaxLevel
	{
		get
		{
			return __pbn__MaxLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxLevel = value;
		}
	}

	[ProtoMember(5, Name = "begin_time")]
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

	[ProtoMember(6, Name = "end_time")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeData()
	{
		return __pbn__Data != null;
	}

	public void ResetData()
	{
		__pbn__Data = null;
	}

	public bool ShouldSerializeMinLevel()
	{
		return __pbn__MinLevel.HasValue;
	}

	public void ResetMinLevel()
	{
		__pbn__MinLevel = null;
	}

	public bool ShouldSerializeMaxLevel()
	{
		return __pbn__MaxLevel.HasValue;
	}

	public void ResetMaxLevel()
	{
		__pbn__MaxLevel = null;
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
}
