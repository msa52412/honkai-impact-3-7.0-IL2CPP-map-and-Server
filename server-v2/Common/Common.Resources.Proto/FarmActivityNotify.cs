using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FarmActivityNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1395
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FarmLevel;

	private uint? __pbn__FarmExp;

	[ProtoMember(1, Name = "farm_level")]
	public uint FarmLevel
	{
		get
		{
			return __pbn__FarmLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__FarmLevel = value;
		}
	}

	[ProtoMember(2, Name = "farm_exp")]
	public uint FarmExp
	{
		get
		{
			return __pbn__FarmExp.GetValueOrDefault();
		}
		set
		{
			__pbn__FarmExp = value;
		}
	}

	[ProtoMember(3, Name = "slot_detail_list")]
	public List<FarmSlotDetail> SlotDetailLists { get; } = new List<FarmSlotDetail>();


	[ProtoMember(4, Name = "produce_material_list")]
	public List<FarmProduceMaterialDetail> ProduceMaterialLists { get; } = new List<FarmProduceMaterialDetail>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFarmLevel()
	{
		return __pbn__FarmLevel.HasValue;
	}

	public void ResetFarmLevel()
	{
		__pbn__FarmLevel = null;
	}

	public bool ShouldSerializeFarmExp()
	{
		return __pbn__FarmExp.HasValue;
	}

	public void ResetFarmExp()
	{
		__pbn__FarmExp = null;
	}
}
