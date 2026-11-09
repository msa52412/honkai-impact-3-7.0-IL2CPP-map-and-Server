using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWareHouseItemRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2656
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
		StatusError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DemandId;

	private WareHouseStatus? __pbn__Status;

	private uint? __pbn__CdFinishTime;

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

	[ProtoMember(3, Name = "status")]
	[DefaultValue(WareHouseStatus.WarehouseStatusNone)]
	public WareHouseStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(4, Name = "cd_finish_time")]
	public uint CdFinishTime
	{
		get
		{
			return __pbn__CdFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CdFinishTime = value;
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

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeCdFinishTime()
	{
		return __pbn__CdFinishTime.HasValue;
	}

	public void ResetCdFinishTime()
	{
		__pbn__CdFinishTime = null;
	}
}
