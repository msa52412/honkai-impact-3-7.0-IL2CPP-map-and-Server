using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockDormHouseReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 609
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HouseId;

	[ProtoMember(1, Name = "house_id")]
	public uint HouseId
	{
		get
		{
			return __pbn__HouseId.GetValueOrDefault();
		}
		set
		{
			__pbn__HouseId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHouseId()
	{
		return __pbn__HouseId.HasValue;
	}

	public void ResetHouseId()
	{
		__pbn__HouseId = null;
	}
}
