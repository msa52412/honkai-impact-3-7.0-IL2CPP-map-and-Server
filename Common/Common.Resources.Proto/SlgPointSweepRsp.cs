using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgPointSweepRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6053
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 3,
		[ProtoEnum(Name = "POINT_INVALID")]
		PointInvalid = 4,
		[ProtoEnum(Name = "COST_MATERIAL_LACK")]
		CostMaterialLack = 5,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull = 6
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}
}
