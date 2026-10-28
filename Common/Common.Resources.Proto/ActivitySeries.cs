using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivitySeries : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SeriesId;

	private uint? __pbn__AvailableSweepNum;

	[ProtoMember(1, Name = "series_id")]
	public uint SeriesId
	{
		get
		{
			return __pbn__SeriesId.GetValueOrDefault();
		}
		set
		{
			__pbn__SeriesId = value;
		}
	}

	[ProtoMember(2, Name = "available_sweep_num")]
	public uint AvailableSweepNum
	{
		get
		{
			return __pbn__AvailableSweepNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AvailableSweepNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSeriesId()
	{
		return __pbn__SeriesId.HasValue;
	}

	public void ResetSeriesId()
	{
		__pbn__SeriesId = null;
	}

	public bool ShouldSerializeAvailableSweepNum()
	{
		return __pbn__AvailableSweepNum.HasValue;
	}

	public void ResetAvailableSweepNum()
	{
		__pbn__AvailableSweepNum = null;
	}
}
