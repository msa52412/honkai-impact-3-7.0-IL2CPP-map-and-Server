using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LevelUpDormRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 614
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "BUSY")]
		Busy,
		[ProtoEnum(Name = "HOUSE_NOT_EXIST")]
		HouseNotExist,
		[ProtoEnum(Name = "LEVEL_ERROR")]
		LevelError,
		[ProtoEnum(Name = "COMFORT_LIMIT")]
		ComfortLimit,
		[ProtoEnum(Name = "MATERIAL_LIMIT")]
		MaterialLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LevelUpHouseId;

	private uint? __pbn__LevelUpEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "level_up_house_id")]
	public uint LevelUpHouseId
	{
		get
		{
			return __pbn__LevelUpHouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__LevelUpHouseId = value;
		}
	}

	[ProtoMember(3, Name = "level_up_end_time")]
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

	public bool ShouldSerializeLevelUpHouseId()
	{
		return __pbn__LevelUpHouseId.HasValue;
	}

	public void ResetLevelUpHouseId()
	{
		__pbn__LevelUpHouseId = null;
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
