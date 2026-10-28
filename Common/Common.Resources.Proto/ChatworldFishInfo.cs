using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__DailyGetCurrencyNum;

	[ProtoMember(1, Name = "daily_get_currency_num")]
	public uint DailyGetCurrencyNum
	{
		get
		{
			return __pbn__DailyGetCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyGetCurrencyNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDailyGetCurrencyNum()
	{
		return __pbn__DailyGetCurrencyNum.HasValue;
	}

	public void ResetDailyGetCurrencyNum()
	{
		__pbn__DailyGetCurrencyNum = null;
	}
}
