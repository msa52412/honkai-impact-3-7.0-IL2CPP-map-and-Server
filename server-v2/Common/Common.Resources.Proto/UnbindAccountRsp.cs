using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnbindAccountRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 962
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACCOUNT_VERIFY_ERROR")]
		AccountVerifyError,
		[ProtoEnum(Name = "ACCOUNT_TYPE_ERROR")]
		AccountTypeError,
		[ProtoEnum(Name = "NOT_BIND")]
		NotBind,
		[ProtoEnum(Name = "IS_LOGIN")]
		IsLogin
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
