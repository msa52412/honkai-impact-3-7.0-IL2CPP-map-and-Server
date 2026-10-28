using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetWarshipReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5456
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WarshipId;

	[ProtoMember(1, Name = "warship_id")]
	public uint WarshipId
	{
		get
		{
			return __pbn__WarshipId.GetValueOrDefault();
		}
		set
		{
			__pbn__WarshipId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWarshipId()
	{
		return __pbn__WarshipId.HasValue;
	}

	public void ResetWarshipId()
	{
		__pbn__WarshipId = null;
	}
}
