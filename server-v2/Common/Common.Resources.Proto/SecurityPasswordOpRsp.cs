using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SecurityPasswordOpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5030
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

	private SecurityPasswordOpType? __pbn__OpType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "op_type")]
	[DefaultValue(SecurityPasswordOpType.SecurityPasswordNoneOp)]
	public SecurityPasswordOpType OpType
	{
		get
		{
			return __pbn__OpType.GetValueOrDefault();
		}
		set
		{
			__pbn__OpType = value;
		}
	}

	[ProtoMember(3, Name = "security_password_info")]
	public SecurityPasswordInfo SecurityPasswordInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOpType()
	{
		return __pbn__OpType.HasValue;
	}

	public void ResetOpType()
	{
		__pbn__OpType = null;
	}
}
