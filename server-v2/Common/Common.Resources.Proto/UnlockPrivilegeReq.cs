using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockPrivilegeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1642
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__PrivilegeId;

	[ProtoMember(1, Name = "privilege_id")]
	public uint PrivilegeId
	{
		get
		{
			return __pbn__PrivilegeId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrivilegeId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrivilegeId()
	{
		return __pbn__PrivilegeId.HasValue;
	}

	public void ResetPrivilegeId()
	{
		__pbn__PrivilegeId = null;
	}
}
