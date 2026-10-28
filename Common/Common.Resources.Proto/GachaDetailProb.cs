using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaDetailProb : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private string __pbn__Star;

	private string __pbn__Content;

	private string __pbn__Prob;

	private bool? __pbn__IsUp;

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

	[ProtoMember(2, Name = "star")]
	[DefaultValue("")]
	public string Star
	{
		get
		{
			return __pbn__Star ?? "";
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(3, Name = "content")]
	[DefaultValue("")]
	public string Content
	{
		get
		{
			return __pbn__Content ?? "";
		}
		set
		{
			__pbn__Content = value;
		}
	}

	[ProtoMember(4, Name = "prob")]
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

	[ProtoMember(5, Name = "is_up")]
	public bool IsUp
	{
		get
		{
			return __pbn__IsUp.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUp = value;
		}
	}

	[ProtoMember(6, Name = "order_value")]
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

	public bool ShouldSerializeStar()
	{
		return __pbn__Star != null;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeContent()
	{
		return __pbn__Content != null;
	}

	public void ResetContent()
	{
		__pbn__Content = null;
	}

	public bool ShouldSerializeProb()
	{
		return __pbn__Prob != null;
	}

	public void ResetProb()
	{
		__pbn__Prob = null;
	}

	public bool ShouldSerializeIsUp()
	{
		return __pbn__IsUp.HasValue;
	}

	public void ResetIsUp()
	{
		__pbn__IsUp = null;
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
