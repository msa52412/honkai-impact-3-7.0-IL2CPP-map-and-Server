using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BindAccountRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 120
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACCOUNT_TYPE_ERROR")]
		AccountTypeError,
		[ProtoEnum(Name = "ACCOUNT_VERIFY_FAILED")]
		AccountVerifyFailed,
		[ProtoEnum(Name = "ACCOUNT_BIND_TO_OTHER_UID")]
		AccountBindToOtherUid,
		[ProtoEnum(Name = "REPEAT_BIND")]
		RepeatBind
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "account_type")]
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

	[ProtoMember(3, Name = "account_uid")]
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
}
