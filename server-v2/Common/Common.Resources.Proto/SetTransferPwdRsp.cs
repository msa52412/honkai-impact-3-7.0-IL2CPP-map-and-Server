using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetTransferPwdRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 246
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PWD_FORMAT_ERROR")]
		PwdFormatError
	}

	private IExtension __pbn__extensionData;

	private string __pbn__TransferCode;

	private uint? __pbn__TransferPwdExpireTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "transfer_code")]
	[DefaultValue("")]
	public string TransferCode
	{
		get
		{
			return __pbn__TransferCode ?? "";
		}
		set
		{
			__pbn__TransferCode = value;
		}
	}

	[ProtoMember(3, Name = "transfer_pwd_expire_time")]
	public uint TransferPwdExpireTime
	{
		get
		{
			return __pbn__TransferPwdExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TransferPwdExpireTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTransferCode()
	{
		return __pbn__TransferCode != null;
	}

	public void ResetTransferCode()
	{
		__pbn__TransferCode = null;
	}

	public bool ShouldSerializeTransferPwdExpireTime()
	{
		return __pbn__TransferPwdExpireTime.HasValue;
	}

	public void ResetTransferPwdExpireTime()
	{
		__pbn__TransferPwdExpireTime = null;
	}
}
