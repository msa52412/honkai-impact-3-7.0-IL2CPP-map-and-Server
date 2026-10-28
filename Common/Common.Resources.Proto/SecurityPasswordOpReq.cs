using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SecurityPasswordOpReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5029
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Password;

	private SecurityPasswordOpType? __pbn__OpType;

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

	[ProtoMember(3, Name = "para_list")]
	public uint[] ParaLists { get; set; }

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

	public bool ShouldSerializeOpType()
	{
		return __pbn__OpType.HasValue;
	}

	public void ResetOpType()
	{
		__pbn__OpType = null;
	}
}
