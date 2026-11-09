using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DropLimitItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LimitId;

	private uint? __pbn__GotNum;

	[ProtoMember(1, Name = "limit_id")]
	public uint LimitId
	{
		get
		{
			return __pbn__LimitId.GetValueOrDefault();
		}
		set
		{
			__pbn__LimitId = value;
		}
	}

	[ProtoMember(2, Name = "got_num")]
	public uint GotNum
	{
		get
		{
			return __pbn__GotNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GotNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLimitId()
	{
		return __pbn__LimitId.HasValue;
	}

	public void ResetLimitId()
	{
		__pbn__LimitId = null;
	}

	public bool ShouldSerializeGotNum()
	{
		return __pbn__GotNum.HasValue;
	}

	public void ResetGotNum()
	{
		__pbn__GotNum = null;
	}
}
