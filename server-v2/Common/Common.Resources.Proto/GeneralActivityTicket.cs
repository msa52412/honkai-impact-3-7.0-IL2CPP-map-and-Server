using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityTicket : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__AddNum;

	[ProtoMember(1, Name = "material_id")]
	public uint MaterialId
	{
		get
		{
			return __pbn__MaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__MaterialId = value;
		}
	}

	[ProtoMember(2, Name = "add_num")]
	public uint AddNum
	{
		get
		{
			return __pbn__AddNum.GetValueOrDefault();
		}
		set
		{
			__pbn__AddNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaterialId()
	{
		return __pbn__MaterialId.HasValue;
	}

	public void ResetMaterialId()
	{
		__pbn__MaterialId = null;
	}

	public bool ShouldSerializeAddNum()
	{
		return __pbn__AddNum.HasValue;
	}

	public void ResetAddNum()
	{
		__pbn__AddNum = null;
	}
}
