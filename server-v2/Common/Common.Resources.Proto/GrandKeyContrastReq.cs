using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyContrastReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 761
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}
}
