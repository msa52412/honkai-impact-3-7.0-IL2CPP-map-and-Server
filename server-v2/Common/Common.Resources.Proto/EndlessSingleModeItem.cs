using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSingleModeItem : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ItemId;

	private uint? __pbn__ItemNum;

	[ProtoMember(1, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(2, Name = "item_num")]
	public uint ItemNum
	{
		get
		{
			return __pbn__ItemNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeItemNum()
	{
		return __pbn__ItemNum.HasValue;
	}

	public void ResetItemNum()
	{
		__pbn__ItemNum = null;
	}
}
