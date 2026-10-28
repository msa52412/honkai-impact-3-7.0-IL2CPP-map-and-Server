using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SettingDict : IExtensible
{
	[ProtoContract]
	public class DictItem : IExtensible
	{
		private IExtension __pbn__extensionData;

		private string __pbn__Key;

		private ValueType? __pbn__Type;

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

		[ProtoMember(2, Name = "type")]
		[DefaultValue(ValueType.Int)]
		public ValueType Type
		{
			get
			{
				return __pbn__Type ?? ValueType.Int;
			}
			set
			{
				__pbn__Type = value;
			}
		}

		[ProtoMember(3, Name = "value")]
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

		public bool ShouldSerializeType()
		{
			return __pbn__Type.HasValue;
		}

		public void ResetType()
		{
			__pbn__Type = null;
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

	[ProtoContract]
	public enum ValueType
	{
		[ProtoEnum(Name = "INT")]
		Int = 1,
		[ProtoEnum(Name = "FLOAT")]
		Float,
		[ProtoEnum(Name = "STRING")]
		String
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "dict_item_list")]
	public List<DictItem> DictItemLists { get; } = new List<DictItem>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
