using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RpgTaleStageScoreDrop : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalNum;

	private uint? __pbn__DailyNum;

	[ProtoMember(1, Name = "total_num")]
	public uint TotalNum
	{
		get
		{
			return __pbn__TotalNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalNum = value;
		}
	}

	[ProtoMember(2, Name = "daily_num")]
	public uint DailyNum
	{
		get
		{
			return __pbn__DailyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalNum()
	{
		return __pbn__TotalNum.HasValue;
	}

	public void ResetTotalNum()
	{
		__pbn__TotalNum = null;
	}

	public bool ShouldSerializeDailyNum()
	{
		return __pbn__DailyNum.HasValue;
	}

	public void ResetDailyNum()
	{
		__pbn__DailyNum = null;
	}
}
