using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldCook : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CookId;

	private uint? __pbn__EatNum;

	[ProtoMember(1, Name = "cook_id")]
	public uint CookId
	{
		get
		{
			return __pbn__CookId.GetValueOrDefault();
		}
		set
		{
			__pbn__CookId = value;
		}
	}

	[ProtoMember(2, Name = "eat_num")]
	public uint EatNum
	{
		get
		{
			return __pbn__EatNum.GetValueOrDefault();
		}
		set
		{
			__pbn__EatNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCookId()
	{
		return __pbn__CookId.HasValue;
	}

	public void ResetCookId()
	{
		__pbn__CookId = null;
	}

	public bool ShouldSerializeEatNum()
	{
		return __pbn__EatNum.HasValue;
	}

	public void ResetEatNum()
	{
		__pbn__EatNum = null;
	}
}
