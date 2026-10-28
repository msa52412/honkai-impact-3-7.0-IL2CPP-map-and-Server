using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PostWareHouseDemandRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2650
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "STATUS_ERROR")]
		StatusError,
		[ProtoEnum(Name = "SCOIN_LACK")]
		ScoinLack,
		[ProtoEnum(Name = "ITEM_LACK")]
		ItemLack,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "NOT_VALID")]
		NotValid
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DemandId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "demand_id")]
	public uint DemandId
	{
		get
		{
			return __pbn__DemandId.GetValueOrDefault();
		}
		set
		{
			__pbn__DemandId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeDemandId()
	{
		return __pbn__DemandId.HasValue;
	}

	public void ResetDemandId()
	{
		__pbn__DemandId = null;
	}
}
