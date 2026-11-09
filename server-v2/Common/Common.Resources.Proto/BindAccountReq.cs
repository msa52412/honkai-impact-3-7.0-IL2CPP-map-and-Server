using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BindAccountReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 119
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	private string __pbn__AccountToken;

	private string __pbn__AccountExt;

	[ProtoMember(1, Name = "account_type")]
	public uint AccountType
	{
		get
		{
			return __pbn__AccountType.GetValueOrDefault();
		}
		set
		{
			__pbn__AccountType = value;
		}
	}

	[ProtoMember(2, Name = "account_uid")]
	[DefaultValue("")]
	public string AccountUid
	{
		get
		{
			return __pbn__AccountUid ?? "";
		}
		set
		{
			__pbn__AccountUid = value;
		}
	}

	[ProtoMember(3, Name = "account_token")]
	[DefaultValue("")]
	public string AccountToken
	{
		get
		{
			return __pbn__AccountToken ?? "";
		}
		set
		{
			__pbn__AccountToken = value;
		}
	}

	[ProtoMember(4, Name = "account_ext")]
	[DefaultValue("")]
	public string AccountExt
	{
		get
		{
			return __pbn__AccountExt ?? "";
		}
		set
		{
			__pbn__AccountExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAccountType()
	{
		return __pbn__AccountType.HasValue;
	}

	public void ResetAccountType()
	{
		__pbn__AccountType = null;
	}

	public bool ShouldSerializeAccountUid()
	{
		return __pbn__AccountUid != null;
	}

	public void ResetAccountUid()
	{
		__pbn__AccountUid = null;
	}

	public bool ShouldSerializeAccountToken()
	{
		return __pbn__AccountToken != null;
	}

	public void ResetAccountToken()
	{
		__pbn__AccountToken = null;
	}

	public bool ShouldSerializeAccountExt()
	{
		return __pbn__AccountExt != null;
	}

	public void ResetAccountExt()
	{
		__pbn__AccountExt = null;
	}
}
