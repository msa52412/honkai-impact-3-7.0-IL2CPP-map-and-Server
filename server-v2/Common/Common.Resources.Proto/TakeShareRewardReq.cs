using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeShareRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4237
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ShareId;

	[ProtoMember(1, Name = "share_id")]
	public uint ShareId
	{
		get
		{
			return __pbn__ShareId.GetValueOrDefault();
		}
		set
		{
			__pbn__ShareId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeShareId()
	{
		return __pbn__ShareId.HasValue;
	}

	public void ResetShareId()
	{
		__pbn__ShareId = null;
	}
}
