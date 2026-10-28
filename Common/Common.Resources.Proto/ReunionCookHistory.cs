using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReunionCookHistory : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BookId;

	private uint? __pbn__DailyNum;

	private uint? __pbn__TotalNum;

	[ProtoMember(1, Name = "book_id")]
	public uint BookId
	{
		get
		{
			return __pbn__BookId.GetValueOrDefault();
		}
		set
		{
			__pbn__BookId = value;
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

	[ProtoMember(3, Name = "total_num")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBookId()
	{
		return __pbn__BookId.HasValue;
	}

	public void ResetBookId()
	{
		__pbn__BookId = null;
	}

	public bool ShouldSerializeDailyNum()
	{
		return __pbn__DailyNum.HasValue;
	}

	public void ResetDailyNum()
	{
		__pbn__DailyNum = null;
	}

	public bool ShouldSerializeTotalNum()
	{
		return __pbn__TotalNum.HasValue;
	}

	public void ResetTotalNum()
	{
		__pbn__TotalNum = null;
	}
}
