using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PostWareHouseDemandReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2649
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__DemandId;

	[ProtoMember(1, Name = "demand_id")]
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
