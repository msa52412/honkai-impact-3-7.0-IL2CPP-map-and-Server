using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CancelWareHouseDemandRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2652
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

	private bool? __pbn__IsAutoCancel;

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

	[ProtoMember(3, Name = "is_auto_cancel")]
	public bool IsAutoCancel
	{
		get
		{
			return __pbn__IsAutoCancel.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAutoCancel = value;
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

	public bool ShouldSerializeIsAutoCancel()
	{
		return __pbn__IsAutoCancel.HasValue;
	}

	public void ResetIsAutoCancel()
	{
		__pbn__IsAutoCancel = null;
	}
}
