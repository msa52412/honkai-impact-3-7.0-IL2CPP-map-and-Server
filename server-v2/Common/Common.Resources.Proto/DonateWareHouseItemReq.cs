using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DonateWareHouseItemReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2653
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__DemandId;

	[ProtoMember(1, Name = "item")]
	public EquipmentItem Item { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "demand_id")]
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

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
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
