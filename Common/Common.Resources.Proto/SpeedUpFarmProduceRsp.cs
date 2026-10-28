using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SpeedUpFarmProduceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1394
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_PRODUCE")]
		NotInProduce,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SlotId;

	private uint? __pbn__SpeedUpMaterialId;

	private uint? __pbn__SpeedUpMaterialNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "slot_id")]
	public uint SlotId
	{
		get
		{
			return __pbn__SlotId.GetValueOrDefault();
		}
		set
		{
			__pbn__SlotId = value;
		}
	}

	[ProtoMember(3, Name = "speed_up_material_id")]
	public uint SpeedUpMaterialId
	{
		get
		{
			return __pbn__SpeedUpMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__SpeedUpMaterialId = value;
		}
	}

	[ProtoMember(4, Name = "speed_up_material_num")]
	public uint SpeedUpMaterialNum
	{
		get
		{
			return __pbn__SpeedUpMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__SpeedUpMaterialNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSlotId()
	{
		return __pbn__SlotId.HasValue;
	}

	public void ResetSlotId()
	{
		__pbn__SlotId = null;
	}

	public bool ShouldSerializeSpeedUpMaterialId()
	{
		return __pbn__SpeedUpMaterialId.HasValue;
	}

	public void ResetSpeedUpMaterialId()
	{
		__pbn__SpeedUpMaterialId = null;
	}

	public bool ShouldSerializeSpeedUpMaterialNum()
	{
		return __pbn__SpeedUpMaterialNum.HasValue;
	}

	public void ResetSpeedUpMaterialNum()
	{
		__pbn__SpeedUpMaterialNum = null;
	}
}
