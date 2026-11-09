using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaTotalProb : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private string __pbn__Prob;

	private uint? __pbn__OrderValue;

	[ProtoMember(1, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(2, Name = "prob")]
	[DefaultValue("")]
	public string Prob
	{
		get
		{
			return __pbn__Prob ?? "";
		}
		set
		{
			__pbn__Prob = value;
		}
	}

	[ProtoMember(3, Name = "order_value")]
	public uint OrderValue
	{
		get
		{
			return __pbn__OrderValue.GetValueOrDefault();
		}
		set
		{
			__pbn__OrderValue = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeProb()
	{
		return __pbn__Prob != null;
	}

	public void ResetProb()
	{
		__pbn__Prob = null;
	}

	public bool ShouldSerializeOrderValue()
	{
		return __pbn__OrderValue.HasValue;
	}

	public void ResetOrderValue()
	{
		__pbn__OrderValue = null;
	}
}
