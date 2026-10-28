using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class IslandDisjoinEquipmentReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 179
	}

	private IExtension __pbn__extensionData;

	private EquipmentType? __pbn__Type;

	private DisjoinType? __pbn__DisjoinType;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(EquipmentType.EquipmentMaterial)]
	public EquipmentType Type
	{
		get
		{
			return __pbn__Type ?? EquipmentType.EquipmentMaterial;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "unique_id_list")]
	public uint[] UniqueIdLists { get; set; }

	[ProtoMember(3, Name = "disjoin_type")]
	[DefaultValue(DisjoinType.DisjoinTotal)]
	public DisjoinType DisjoinType
	{
		get
		{
			return __pbn__DisjoinType ?? DisjoinType.DisjoinTotal;
		}
		set
		{
			__pbn__DisjoinType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeDisjoinType()
	{
		return __pbn__DisjoinType.HasValue;
	}

	public void ResetDisjoinType()
	{
		__pbn__DisjoinType = null;
	}
}
