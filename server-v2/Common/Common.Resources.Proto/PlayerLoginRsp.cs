using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerLoginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REPEAT_LOGIN")]
		RepeatLogin,
		[ProtoEnum(Name = "IN_BLACK_LIST")]
		InBlackList,
		[ProtoEnum(Name = "WAIT_STOP_ANOTHER_LOGIN")]
		WaitStopAnotherLogin,
		[ProtoEnum(Name = "ANTI_ADDICT_LOGIN")]
		AntiAddictLogin,
		[ProtoEnum(Name = "SQUEEZE_CD")]
		SqueezeCd
	}

	private IExtension __pbn__extensionData;

	private string __pbn__Msg;

	private bool? __pbn__IsFirstLogin;

	private uint? __pbn__LastClientPacketId;

	private string __pbn__RegionName;

	private CGType? __pbn__CgType;

	private uint? __pbn__LastLogoutTime;

	private uint? __pbn__RegionId;

	private bool? __pbn__IsValidReconnect;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	private uint? __pbn__LoginSessionToken;

	private uint? __pbn__PsychoKey;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "msg")]
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

	[ProtoMember(4, Name = "is_first_login")]
	public bool IsFirstLogin
	{
		get
		{
			return __pbn__IsFirstLogin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFirstLogin = value;
		}
	}

	[ProtoMember(5, Name = "last_client_packet_id")]
	public uint LastClientPacketId
	{
		get
		{
			return __pbn__LastClientPacketId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastClientPacketId = value;
		}
	}

	[ProtoMember(6, Name = "region_name")]
	[DefaultValue("")]
	public string RegionName
	{
		get
		{
			return __pbn__RegionName ?? "";
		}
		set
		{
			__pbn__RegionName = value;
		}
	}

	[ProtoMember(9, Name = "cg_type")]
	[DefaultValue(CGType.CgNone)]
	public CGType CgType
	{
		get
		{
			return __pbn__CgType.GetValueOrDefault();
		}
		set
		{
			__pbn__CgType = value;
		}
	}

	[ProtoMember(10, Name = "last_logout_time")]
	public uint LastLogoutTime
	{
		get
		{
			return __pbn__LastLogoutTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLogoutTime = value;
		}
	}

	[ProtoMember(11, Name = "region_id")]
	public uint RegionId
	{
		get
		{
			return __pbn__RegionId.GetValueOrDefault();
		}
		set
		{
			__pbn__RegionId = value;
		}
	}

	[ProtoMember(12, Name = "is_valid_reconnect")]
	public bool IsValidReconnect
	{
		get
		{
			return __pbn__IsValidReconnect.GetValueOrDefault();
		}
		set
		{
			__pbn__IsValidReconnect = value;
		}
	}

	[ProtoMember(13, Name = "account_type")]
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

	[ProtoMember(14, Name = "account_uid")]
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

	[ProtoMember(15, Name = "login_session_token")]
	public uint LoginSessionToken
	{
		get
		{
			return __pbn__LoginSessionToken.GetValueOrDefault();
		}
		set
		{
			__pbn__LoginSessionToken = value;
		}
	}

	[ProtoMember(16, Name = "psycho_key")]
	public uint PsychoKey
	{
		get
		{
			return __pbn__PsychoKey.GetValueOrDefault();
		}
		set
		{
			__pbn__PsychoKey = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeIsFirstLogin()
	{
		return __pbn__IsFirstLogin.HasValue;
	}

	public void ResetIsFirstLogin()
	{
		__pbn__IsFirstLogin = null;
	}

	public bool ShouldSerializeLastClientPacketId()
	{
		return __pbn__LastClientPacketId.HasValue;
	}

	public void ResetLastClientPacketId()
	{
		__pbn__LastClientPacketId = null;
	}

	public bool ShouldSerializeRegionName()
	{
		return __pbn__RegionName != null;
	}

	public void ResetRegionName()
	{
		__pbn__RegionName = null;
	}

	public bool ShouldSerializeCgType()
	{
		return __pbn__CgType.HasValue;
	}

	public void ResetCgType()
	{
		__pbn__CgType = null;
	}

	public bool ShouldSerializeLastLogoutTime()
	{
		return __pbn__LastLogoutTime.HasValue;
	}

	public void ResetLastLogoutTime()
	{
		__pbn__LastLogoutTime = null;
	}

	public bool ShouldSerializeRegionId()
	{
		return __pbn__RegionId.HasValue;
	}

	public void ResetRegionId()
	{
		__pbn__RegionId = null;
	}

	public bool ShouldSerializeIsValidReconnect()
	{
		return __pbn__IsValidReconnect.HasValue;
	}

	public void ResetIsValidReconnect()
	{
		__pbn__IsValidReconnect = null;
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

	public bool ShouldSerializeLoginSessionToken()
	{
		return __pbn__LoginSessionToken.HasValue;
	}

	public void ResetLoginSessionToken()
	{
		__pbn__LoginSessionToken = null;
	}

	public bool ShouldSerializePsychoKey()
	{
		return __pbn__PsychoKey.HasValue;
	}

	public void ResetPsychoKey()
	{
		__pbn__PsychoKey = null;
	}
}
