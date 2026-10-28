using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ActivityRewardStatisticItemData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ShowId;

	private uint? __pbn__Num;

	[ProtoMember(1, Name = "show_id")]
	public uint ShowId
	{
		get
		{
			return __pbn__ShowId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShowId = value;
		}
	}

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShowId()
	{
		return __pbn__ShowId.HasValue;
	}

	public void ResetShowId()
	{
		__pbn__ShowId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
