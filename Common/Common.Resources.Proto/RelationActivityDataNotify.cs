using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RelationActivityDataNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1373
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LimitType;

	private uint? __pbn__LimitNum;

	private uint? __pbn__GotMaterialNum;

	private uint? __pbn__RelationType;

	private uint? __pbn__DropMaterialId;

	[ProtoMember(1, Name = "limit_type")]
	public uint LimitType
	{
		get
		{
			return __pbn__LimitType.GetValueOrDefault();
		}
		set
		{
			__pbn__LimitType = value;
		}
	}

	[ProtoMember(2, Name = "limit_num")]
	public uint LimitNum
	{
		get
		{
			return __pbn__LimitNum.GetValueOrDefault();
		}
		set
		{
			__pbn__LimitNum = value;
		}
	}

	[ProtoMember(3, Name = "got_material_num")]
	public uint GotMaterialNum
	{
		get
		{
			return __pbn__GotMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__GotMaterialNum = value;
		}
	}

	[ProtoMember(4, Name = "relation_type")]
	public uint RelationType
	{
		get
		{
			return __pbn__RelationType.GetValueOrDefault();
		}
		set
		{
			__pbn__RelationType = value;
		}
	}

	[ProtoMember(5, Name = "drop_material_id")]
	public uint DropMaterialId
	{
		get
		{
			return __pbn__DropMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__DropMaterialId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLimitType()
	{
		return __pbn__LimitType.HasValue;
	}

	public void ResetLimitType()
	{
		__pbn__LimitType = null;
	}

	public bool ShouldSerializeLimitNum()
	{
		return __pbn__LimitNum.HasValue;
	}

	public void ResetLimitNum()
	{
		__pbn__LimitNum = null;
	}

	public bool ShouldSerializeGotMaterialNum()
	{
		return __pbn__GotMaterialNum.HasValue;
	}

	public void ResetGotMaterialNum()
	{
		__pbn__GotMaterialNum = null;
	}

	public bool ShouldSerializeRelationType()
	{
		return __pbn__RelationType.HasValue;
	}

	public void ResetRelationType()
	{
		__pbn__RelationType = null;
	}

	public bool ShouldSerializeDropMaterialId()
	{
		return __pbn__DropMaterialId.HasValue;
	}

	public void ResetDropMaterialId()
	{
		__pbn__DropMaterialId = null;
	}
}
