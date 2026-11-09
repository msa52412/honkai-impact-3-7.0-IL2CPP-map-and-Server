using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelEntityProperty : IExtensible
{
	private IExtension __pbn__extensionData;

	private string __pbn__Name;

	private float? __pbn__Value;

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

	[ProtoMember(2, Name = "value")]
	public float Value
	{
		get
		{
			return __pbn__Value.GetValueOrDefault();
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

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}
}
