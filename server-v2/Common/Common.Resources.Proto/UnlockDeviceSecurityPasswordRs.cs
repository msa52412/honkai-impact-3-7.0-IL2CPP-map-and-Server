using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UnlockDeviceSecurityPasswordRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5028
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TOO_MANY_FAILS")]
		TooManyFails,
		[ProtoEnum(Name = "WRONG_PASSWORD")]
		WrongPassword
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "security_password_info")]
	public SecurityPasswordInfo SecurityPasswordInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
