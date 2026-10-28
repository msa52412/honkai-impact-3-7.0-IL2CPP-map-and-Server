using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaDailyTimesLimit : IExtensible
{
	private IExtension __pbn__extensionData;

	private GachaType? __pbn__Type;

	private uint? __pbn__GroupDailyTimes;

	private uint? __pbn__GroupDailyLimitTimes;

	private string __pbn__GroupLimitDisplayExt;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType Type
	{
		get
		{
			return __pbn__Type ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "group_daily_times")]
	public uint GroupDailyTimes
	{
		get
		{
			return __pbn__GroupDailyTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupDailyTimes = value;
		}
	}

	[ProtoMember(3, Name = "group_daily_limit_times")]
	public uint GroupDailyLimitTimes
	{
		get
		{
			return __pbn__GroupDailyLimitTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupDailyLimitTimes = value;
		}
	}

	[ProtoMember(4, Name = "group_limit_display_ext")]
	[DefaultValue("")]
	public string GroupLimitDisplayExt
	{
		get
		{
			return __pbn__GroupLimitDisplayExt ?? "";
		}
		set
		{
			__pbn__GroupLimitDisplayExt = value;
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

	public bool ShouldSerializeGroupDailyTimes()
	{
		return __pbn__GroupDailyTimes.HasValue;
	}

	public void ResetGroupDailyTimes()
	{
		__pbn__GroupDailyTimes = null;
	}

	public bool ShouldSerializeGroupDailyLimitTimes()
	{
		return __pbn__GroupDailyLimitTimes.HasValue;
	}

	public void ResetGroupDailyLimitTimes()
	{
		__pbn__GroupDailyLimitTimes = null;
	}

	public bool ShouldSerializeGroupLimitDisplayExt()
	{
		return __pbn__GroupLimitDisplayExt != null;
	}

	public void ResetGroupLimitDisplayExt()
	{
		__pbn__GroupLimitDisplayExt = null;
	}
}
