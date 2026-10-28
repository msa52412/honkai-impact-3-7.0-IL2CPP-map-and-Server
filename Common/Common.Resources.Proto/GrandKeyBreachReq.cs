using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyBreachReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 757
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KeyId;

	[ProtoMember(1, Name = "key_id")]
	public uint KeyId
	{
		get
		{
			return __pbn__KeyId.GetValueOrDefault();
		}
		set
		{
			__pbn__KeyId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeKeyId()
	{
		return __pbn__KeyId.HasValue;
	}

	public void ResetKeyId()
	{
		__pbn__KeyId = null;
	}
}
