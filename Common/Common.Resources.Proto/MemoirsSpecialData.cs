using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MemoirsSpecialData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Value;

	private string __pbn__StrValue;

	private MemoirsDataType? __pbn__Type;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "value")]
	public uint Value
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

	[ProtoMember(3, Name = "str_value")]
	[DefaultValue("")]
	public string StrValue
	{
		get
		{
			return __pbn__StrValue ?? "";
		}
		set
		{
			__pbn__StrValue = value;
		}
	}

	[ProtoMember(4, Name = "type")]
	[DefaultValue(MemoirsDataType.MemoirsDataTypeUint)]
	public MemoirsDataType Type
	{
		get
		{
			return __pbn__Type ?? MemoirsDataType.MemoirsDataTypeUint;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeValue()
	{
		return __pbn__Value.HasValue;
	}

	public void ResetValue()
	{
		__pbn__Value = null;
	}

	public bool ShouldSerializeStrValue()
	{
		return __pbn__StrValue != null;
	}

	public void ResetStrValue()
	{
		__pbn__StrValue = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}
}
