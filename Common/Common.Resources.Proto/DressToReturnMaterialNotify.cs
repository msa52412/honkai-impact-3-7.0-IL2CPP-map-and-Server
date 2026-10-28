using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DressToReturnMaterialNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 250
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DressId;

	private uint? __pbn__Num;

	private uint? __pbn__Scoin;

	private uint? __pbn__ReturnMaterialId;

	private uint? __pbn__ReturnMaterialNum;

	[ProtoMember(1, Name = "dress_id")]
	public uint DressId
	{
		get
		{
			return __pbn__DressId.GetValueOrDefault();
		}
		set
		{
			__pbn__DressId = value;
		}
	}

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
		}
	}

	[ProtoMember(3, Name = "scoin")]
	public uint Scoin
	{
		get
		{
			return __pbn__Scoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Scoin = value;
		}
	}

	[ProtoMember(4, Name = "return_material_id")]
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

	[ProtoMember(5, Name = "return_material_num")]
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

	public bool ShouldSerializeDressId()
	{
		return __pbn__DressId.HasValue;
	}

	public void ResetDressId()
	{
		__pbn__DressId = null;
	}

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}

	public bool ShouldSerializeScoin()
	{
		return __pbn__Scoin.HasValue;
	}

	public void ResetScoin()
	{
		__pbn__Scoin = null;
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
