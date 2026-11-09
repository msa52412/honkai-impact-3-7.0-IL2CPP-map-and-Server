using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeConsignedRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 523
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReliableId;

	[ProtoMember(1, Name = "reliable_id")]
	public uint ReliableId
	{
		get
		{
			return __pbn__ReliableId.GetValueOrDefault();
		}
		set
		{
			__pbn__ReliableId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReliableId()
	{
		return __pbn__ReliableId.HasValue;
	}

	public void ResetReliableId()
	{
		__pbn__ReliableId = null;
	}
}
