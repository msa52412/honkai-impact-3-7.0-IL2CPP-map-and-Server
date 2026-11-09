using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyBuildingConstructRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3855
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LACK_MATERIAL")]
		LackMaterial,
		[ProtoEnum(Name = "ANOTHER_TYPE_EXIST")]
		AnotherTypeExist,
		[ProtoEnum(Name = "NUM_LIMIT")]
		NumLimit,
		[ProtoEnum(Name = "LEVEL_MAX")]
		LevelMax,
		[ProtoEnum(Name = "NO_LOCATION")]
		NoLocation,
		[ProtoEnum(Name = "WAIT_SELECT")]
		WaitSelect,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AreaId;

	private uint? __pbn__BuildingType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "area_id")]
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

	[ProtoMember(3, Name = "building_type")]
	public uint BuildingType
	{
		get
		{
			return __pbn__BuildingType.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingType = value;
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

	public bool ShouldSerializeBuildingType()
	{
		return __pbn__BuildingType.HasValue;
	}

	public void ResetBuildingType()
	{
		__pbn__BuildingType = null;
	}
}
