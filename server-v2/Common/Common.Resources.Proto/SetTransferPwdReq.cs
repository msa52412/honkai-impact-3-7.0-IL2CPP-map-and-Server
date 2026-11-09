using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetTransferPwdReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 245
	}

	private IExtension __pbn__extensionData;

	private string __pbn__TransferPwd;

	[ProtoMember(1, Name = "transfer_pwd")]
	[DefaultValue("")]
	public string TransferPwd
	{
		get
		{
			return __pbn__TransferPwd ?? "";
		}
		set
		{
			__pbn__TransferPwd = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTransferPwd()
	{
		return __pbn__TransferPwd != null;
	}

	public void ResetTransferPwd()
	{
		__pbn__TransferPwd = null;
	}
}
