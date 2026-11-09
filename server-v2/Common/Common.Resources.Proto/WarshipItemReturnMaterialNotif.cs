using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WarshipItemReturnMaterialNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5462
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WarshipItemId;

	private uint? __pbn__ConvertNum;

	private uint? __pbn__ReturnMaterialId;

	private uint? __pbn__ReturnMaterialNum;

	[ProtoMember(1, Name = "warship_item_id")]
	public uint WarshipItemId
	{
		get
		{
			return __pbn__WarshipItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipItemId = value;
		}
	}

	[ProtoMember(2, Name = "convert_num")]
	public uint ConvertNum
	{
		get
		{
			return __pbn__ConvertNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ConvertNum = value;
		}
	}

	[ProtoMember(3, Name = "return_material_id")]
	public uint ReturnMaterialId
	{
		get
		{
			return __pbn__ReturnMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReturnMaterialId = value;
		}
	}

	[ProtoMember(4, Name = "return_material_num")]
	public uint ReturnMaterialNum
	{
		get
		{
			return __pbn__ReturnMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ReturnMaterialNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWarshipItemId()
	{
		return __pbn__WarshipItemId.HasValue;
	}

	public void ResetWarshipItemId()
	{
		__pbn__WarshipItemId = null;
	}

	public bool ShouldSerializeConvertNum()
	{
		return __pbn__ConvertNum.HasValue;
	}

	public void ResetConvertNum()
	{
		__pbn__ConvertNum = null;
	}

	public bool ShouldSerializeReturnMaterialId()
	{
		return __pbn__ReturnMaterialId.HasValue;
	}

	public void ResetReturnMaterialId()
	{
		__pbn__ReturnMaterialId = null;
	}

	public bool ShouldSerializeReturnMaterialNum()
	{
		return __pbn__ReturnMaterialNum.HasValue;
	}

	public void ResetReturnMaterialNum()
	{
		__pbn__ReturnMaterialNum = null;
	}
}
