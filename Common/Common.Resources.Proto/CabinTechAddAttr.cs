using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CabinTechAddAttr : IExtensible
{
	private IExtension __pbn__extensionData;

	private AvatarAttrType? __pbn__AttrType;

	private float? __pbn__AddRatio;

	[ProtoMember(1, Name = "attr_type")]
	[DefaultValue(AvatarAttrType.AvatarHp)]
	public AvatarAttrType AttrType
	{
		get
		{
			return __pbn__AttrType ?? AvatarAttrType.AvatarHp;
		}
		set
		{
			__pbn__AttrType = value;
		}
	}

	[ProtoMember(2, Name = "add_ratio")]
	public float AddRatio
	{
		get
		{
			return __pbn__AddRatio.GetValueOrDefault();
		}
		set
		{
			__pbn__AddRatio = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAttrType()
	{
		return __pbn__AttrType.HasValue;
	}

	public void ResetAttrType()
	{
		__pbn__AttrType = null;
	}

	public bool ShouldSerializeAddRatio()
	{
		return __pbn__AddRatio.HasValue;
	}

	public void ResetAddRatio()
	{
		__pbn__AddRatio = null;
	}
}
