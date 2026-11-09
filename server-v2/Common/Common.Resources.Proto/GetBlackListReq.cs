using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBlackListReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1594
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsOnlyUid;

	[ProtoMember(1, Name = "is_only_uid")]
	public bool IsOnlyUid
	{
		get
		{
			return __pbn__IsOnlyUid.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOnlyUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsOnlyUid()
	{
		return __pbn__IsOnlyUid.HasValue;
	}

	public void ResetIsOnlyUid()
	{
		__pbn__IsOnlyUid = null;
	}
}
