using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetSecurityPasswordReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5023
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Password;

	[ProtoMember(1, Name = "password")]
	[DefaultValue("")]
	public string Password
	{
		get
		{
			return __pbn__Password ?? "";
		}
		set
		{
			__pbn__Password = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePassword()
	{
		return __pbn__Password != null;
	}

	public void ResetPassword()
	{
		__pbn__Password = null;
	}
}
