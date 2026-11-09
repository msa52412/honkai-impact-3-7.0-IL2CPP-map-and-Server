using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChangeSecurityPasswordReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5025
	}

	private IExtension __pbn__extensionData;

	private string __pbn__OldPassword;

	private string __pbn__NewPassword;

	[ProtoMember(1, Name = "old_password")]
	[DefaultValue("")]
	public string OldPassword
	{
		get
		{
			return __pbn__OldPassword ?? "";
		}
		set
		{
			__pbn__OldPassword = value;
		}
	}

	[ProtoMember(2, Name = "new_password")]
	[DefaultValue("")]
	public string NewPassword
	{
		get
		{
			return __pbn__NewPassword ?? "";
		}
		set
		{
			__pbn__NewPassword = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOldPassword()
	{
		return __pbn__OldPassword != null;
	}

	public void ResetOldPassword()
	{
		__pbn__OldPassword = null;
	}

	public bool ShouldSerializeNewPassword()
	{
		return __pbn__NewPassword != null;
	}

	public void ResetNewPassword()
	{
		__pbn__NewPassword = null;
	}
}
