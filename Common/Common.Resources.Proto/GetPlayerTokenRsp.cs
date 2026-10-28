using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetPlayerTokenRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REGISTER_CLOSED")]
		RegisterClosed,
		[ProtoEnum(Name = "ACCOUNT_VERIFY_ERROR")]
		AccountVerifyError,
		[ProtoEnum(Name = "TOKEN_REPEAT")]
		TokenRepeat,
		[ProtoEnum(Name = "ACCOUNT_TYPE_ERROR")]
		AccountTypeError,
		[ProtoEnum(Name = "SERVER_FULL")]
		ServerFull,
		[ProtoEnum(Name = "INVALID_APK")]
		InvalidApk,
		[ProtoEnum(Name = "TRANSFER_CODE_ERROR")]
		TransferCodeError,
		[ProtoEnum(Name = "IN_BLACK_LIST")]
		InBlackList,
		[ProtoEnum(Name = "SERVER_STOP")]
		ServerStop,
		[ProtoEnum(Name = "ACCOUNT_LOCKED")]
		AccountLocked,
		[ProtoEnum(Name = "SERVER_BUSY")]
		ServerBusy,
		[ProtoEnum(Name = "DEVICE_TYPE_ERROR")]
		DeviceTypeError,
		[ProtoEnum(Name = "FORBID_VISITOR")]
		ForbidVisitor,
		[ProtoEnum(Name = "FORBID_TAG_SMURF_VISITOR")]
		ForbidTagSmurfVisitor,
		[ProtoEnum(Name = "ANTI_ADDICT_LOGIN")]
		AntiAddictLogin,
		[ProtoEnum(Name = "GAME_NOT_AVAILABLE")]
		GameNotAvailable
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private string __pbn__Token;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	private int? __pbn__Tag;

	private string __pbn__TransferCode;

	private uint? __pbn__TransferPwdExpireTime;

	private bool? __pbn__IsBindAccess;

	private uint? __pbn__FightserverIp;

	private uint? __pbn__FightserverPort;

	private string __pbn__Msg;

	private uint? __pbn__BlackListEndTime;

	private uint? __pbn__UserType;

	private uint? __pbn__LimitLevel;

	private byte[] __pbn__SecurityCmdBuffer;

	private string __pbn__HoyolabAccountUid;

	private string __pbn__UaPc;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(3, Name = "token")]
	[DefaultValue("")]
	public string Token
	{
		get
		{
			return __pbn__Token ?? "";
		}
		set
		{
			__pbn__Token = value;
		}
	}

	[ProtoMember(4, Name = "account_type")]
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

	[ProtoMember(5, Name = "account_uid")]
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

	[ProtoMember(6, Name = "tag")]
	public int Tag
	{
		get
		{
			return __pbn__Tag.GetValueOrDefault();
		}
		set
		{
			__pbn__Tag = value;
		}
	}

	[ProtoMember(15, Name = "transfer_code")]
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

	[ProtoMember(16, Name = "transfer_pwd_expire_time")]
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

	[ProtoMember(17, Name = "is_bind_access")]
	public bool IsBindAccess
	{
		get
		{
			return __pbn__IsBindAccess.GetValueOrDefault();
		}
		set
		{
			__pbn__IsBindAccess = value;
		}
	}

	[ProtoMember(18, Name = "fightserver_ip")]
	public uint FightserverIp
	{
		get
		{
			return __pbn__FightserverIp.GetValueOrDefault();
		}
		set
		{
			__pbn__FightserverIp = value;
		}
	}

	[ProtoMember(19, Name = "fightserver_port")]
	public uint FightserverPort
	{
		get
		{
			return __pbn__FightserverPort.GetValueOrDefault();
		}
		set
		{
			__pbn__FightserverPort = value;
		}
	}

	[ProtoMember(20, Name = "msg")]
	[DefaultValue("")]
	public string Msg
	{
		get
		{
			return __pbn__Msg ?? "";
		}
		set
		{
			__pbn__Msg = value;
		}
	}

	[ProtoMember(21, Name = "black_list_end_time")]
	public uint BlackListEndTime
	{
		get
		{
			return __pbn__BlackListEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BlackListEndTime = value;
		}
	}

	[ProtoMember(22, Name = "user_type")]
	public uint UserType
	{
		get
		{
			return __pbn__UserType.GetValueOrDefault();
		}
		set
		{
			__pbn__UserType = value;
		}
	}

	[ProtoMember(23, Name = "limit_level")]
	public uint LimitLevel
	{
		get
		{
			return __pbn__LimitLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__LimitLevel = value;
		}
	}

	[ProtoMember(24, Name = "security_cmd_buffer")]
	public byte[] SecurityCmdBuffer
	{
		get
		{
			return __pbn__SecurityCmdBuffer;
		}
		set
		{
			__pbn__SecurityCmdBuffer = value;
		}
	}

	[ProtoMember(26, Name = "hoyolab_account_uid")]
	[DefaultValue("")]
	public string HoyolabAccountUid
	{
		get
		{
			return __pbn__HoyolabAccountUid ?? "";
		}
		set
		{
			__pbn__HoyolabAccountUid = value;
		}
	}

	[ProtoMember(27, Name = "ua_pc")]
	[DefaultValue("")]
	public string UaPc
	{
		get
		{
			return __pbn__UaPc ?? "";
		}
		set
		{
			__pbn__UaPc = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeToken()
	{
		return __pbn__Token != null;
	}

	public void ResetToken()
	{
		__pbn__Token = null;
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

	public bool ShouldSerializeTag()
	{
		return __pbn__Tag.HasValue;
	}

	public void ResetTag()
	{
		__pbn__Tag = null;
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

	public bool ShouldSerializeIsBindAccess()
	{
		return __pbn__IsBindAccess.HasValue;
	}

	public void ResetIsBindAccess()
	{
		__pbn__IsBindAccess = null;
	}

	public bool ShouldSerializeFightserverIp()
	{
		return __pbn__FightserverIp.HasValue;
	}

	public void ResetFightserverIp()
	{
		__pbn__FightserverIp = null;
	}

	public bool ShouldSerializeFightserverPort()
	{
		return __pbn__FightserverPort.HasValue;
	}

	public void ResetFightserverPort()
	{
		__pbn__FightserverPort = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeBlackListEndTime()
	{
		return __pbn__BlackListEndTime.HasValue;
	}

	public void ResetBlackListEndTime()
	{
		__pbn__BlackListEndTime = null;
	}

	public bool ShouldSerializeUserType()
	{
		return __pbn__UserType.HasValue;
	}

	public void ResetUserType()
	{
		__pbn__UserType = null;
	}

	public bool ShouldSerializeLimitLevel()
	{
		return __pbn__LimitLevel.HasValue;
	}

	public void ResetLimitLevel()
	{
		__pbn__LimitLevel = null;
	}

	public bool ShouldSerializeSecurityCmdBuffer()
	{
		return __pbn__SecurityCmdBuffer != null;
	}

	public void ResetSecurityCmdBuffer()
	{
		__pbn__SecurityCmdBuffer = null;
	}

	public bool ShouldSerializeHoyolabAccountUid()
	{
		return __pbn__HoyolabAccountUid != null;
	}

	public void ResetHoyolabAccountUid()
	{
		__pbn__HoyolabAccountUid = null;
	}

	public bool ShouldSerializeUaPc()
	{
		return __pbn__UaPc != null;
	}

	public void ResetUaPc()
	{
		__pbn__UaPc = null;
	}
}
