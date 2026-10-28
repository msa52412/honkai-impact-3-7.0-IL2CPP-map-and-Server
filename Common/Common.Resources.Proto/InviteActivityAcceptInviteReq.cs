using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteActivityAcceptInviteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1696
	}

	private IExtension __pbn__extensionData;

	private string __pbn__InviteCode;

	[ProtoMember(1, Name = "invite_code")]
	[DefaultValue("")]
	public string InviteCode
	{
		get
		{
			return __pbn__InviteCode ?? "";
		}
		set
		{
			__pbn__InviteCode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInviteCode()
	{
		return __pbn__InviteCode != null;
	}

	public void ResetInviteCode()
	{
		__pbn__InviteCode = null;
	}
}
