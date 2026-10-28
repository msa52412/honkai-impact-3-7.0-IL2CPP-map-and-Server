using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RanchArea : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__CurProductNum;

	private uint? __pbn__ProductionEfficiency;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId
	{
		get
		{
			return __pbn__AreaId.GetValueOrDefault();
		}
		set
		{
			__pbn__AreaId = value;
		}
	}

	[ProtoMember(2, Name = "cur_product_num")]
	public uint CurProductNum
	{
		get
		{
			return __pbn__CurProductNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CurProductNum = value;
		}
	}

	[ProtoMember(3, Name = "monster_unique_id_list")]
	public uint[] MonsterUniqueIdLists { get; set; }

	[ProtoMember(4, Name = "production_efficiency")]
	public uint ProductionEfficiency
	{
		get
		{
			return __pbn__ProductionEfficiency.GetValueOrDefault();
		}
		set
		{
			__pbn__ProductionEfficiency = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAreaId()
	{
		return __pbn__AreaId.HasValue;
	}

	public void ResetAreaId()
	{
		__pbn__AreaId = null;
	}

	public bool ShouldSerializeCurProductNum()
	{
		return __pbn__CurProductNum.HasValue;
	}

	public void ResetCurProductNum()
	{
		__pbn__CurProductNum = null;
	}

	public bool ShouldSerializeProductionEfficiency()
	{
		return __pbn__ProductionEfficiency.HasValue;
	}

	public void ResetProductionEfficiency()
	{
		__pbn__ProductionEfficiency = null;
	}
}
