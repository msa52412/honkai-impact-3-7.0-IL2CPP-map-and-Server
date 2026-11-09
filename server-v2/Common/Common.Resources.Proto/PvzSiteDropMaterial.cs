using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzSiteDropMaterial : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MaterialId;

	private uint? __pbn__HasDropNum;

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

	[ProtoMember(2, Name = "has_drop_num")]
	public uint HasDropNum
	{
		get
		{
			return __pbn__HasDropNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasDropNum = value;
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

	public bool ShouldSerializeHasDropNum()
	{
		return __pbn__HasDropNum.HasValue;
	}

	public void ResetHasDropNum()
	{
		__pbn__HasDropNum = null;
	}
}
