using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpArmadaCabinRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2632
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ARMADA_LEVEL_LACK")]
		ArmadaLevelLack,
		[ProtoEnum(Name = "FUND_LACK")]
		FundLack,
		[ProtoEnum(Name = "POPL_LACK")]
		PoplLack,
		[ProtoEnum(Name = "BUSY")]
		Busy,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "CABIN_NOT_EXIST")]
		CabinNotExist,
		[ProtoEnum(Name = "CABIN_FULL")]
		CabinFull,
		[ProtoEnum(Name = "GRID_ERROR")]
		GridError,
		[ProtoEnum(Name = "NOT_GRANTED")]
		NotGranted,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "NO_OWNERSHIP")]
		NoOwnership
	}

	private IExtension __pbn__extensionData;

	private ArmadaCabinType? __pbn__Type;

	private uint? __pbn__CabinId;

	private uint? __pbn__GridId;

	private uint? __pbn__LevelUpEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(ArmadaCabinType.ArmadaCabinNone)]
	public ArmadaCabinType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "cabin_id")]
	public uint CabinId
	{
		get
		{
			return __pbn__CabinId.GetValueOrDefault();
		}
		set
		{
			__pbn__CabinId = value;
		}
	}

	[ProtoMember(4, Name = "grid_id")]
	public uint GridId
	{
		get
		{
			return __pbn__GridId.GetValueOrDefault();
		}
		set
		{
			__pbn__GridId = value;
		}
	}

	[ProtoMember(5, Name = "level_up_end_time")]
	public uint LevelUpEndTime
	{
		get
		{
			return __pbn__LevelUpEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpEndTime = value;
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

	public bool ShouldSerializeCabinId()
	{
		return __pbn__CabinId.HasValue;
	}

	public void ResetCabinId()
	{
		__pbn__CabinId = null;
	}

	public bool ShouldSerializeGridId()
	{
		return __pbn__GridId.HasValue;
	}

	public void ResetGridId()
	{
		__pbn__GridId = null;
	}

	public bool ShouldSerializeLevelUpEndTime()
	{
		return __pbn__LevelUpEndTime.HasValue;
	}

	public void ResetLevelUpEndTime()
	{
		__pbn__LevelUpEndTime = null;
	}
}
