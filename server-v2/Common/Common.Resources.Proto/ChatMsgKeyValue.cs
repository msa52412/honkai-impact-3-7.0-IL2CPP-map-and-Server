using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgKeyValue : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Key;

	private string __pbn__Value;

	[ProtoMember(1, Name = "key")]
	[DefaultValue("")]
	public string Key
	{
		get
		{
			return __pbn__Key ?? "";
		}
		set
		{
			__pbn__Key = value;
		}
	}

	[ProtoMember(2, Name = "value")]
	[DefaultValue("")]
	public string Value
	{
		get
		{
			return __pbn__Value ?? "";
		}
		set
		{
			__pbn__Value = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKey()
	{
		return __pbn__Key != null;
	}

	public void ResetKey()
	{
		__pbn__Key = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value != null;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}
}
