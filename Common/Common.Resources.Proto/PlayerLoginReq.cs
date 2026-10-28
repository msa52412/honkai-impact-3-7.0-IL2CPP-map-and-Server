using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerLoginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__LastServerPacketId;

	private string __pbn__Cps;

	private string __pbn__CheckSum;

	private string __pbn__DeviceUuid;

	private string __pbn__AndroidSignatures;

	private string __pbn__Language;

	private string __pbn__AdvertiserId;

	private byte[] __pbn__SecurityCmdReply;

	private uint? __pbn__LastLoginVersion;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	private DeviceType? __pbn__DeviceType;

	private uint? __pbn__UserType;

	private string __pbn__UserIp;

	private uint? __pbn__Tag;

	private string __pbn__ClientVersion;

	private string __pbn__ClientDevice;

	private string __pbn__SystemInfo;

	private string __pbn__AndroidSign;

	private uint? __pbn__LimitLevel;

	private uint? __pbn__BlackListEndTime;

	private bool? __pbn__IsBindAccess;

	private uint? __pbn__LoginSessionToken;

	private string __pbn__HoyolabAccountUid;

	private uint? __pbn__UserPort;

	private string __pbn__UaPc;

	[ProtoMember(2, Name = "last_server_packet_id")]
	public uint LastServerPacketId
	{
		get
		{
			return __pbn__LastServerPacketId.GetValueOrDefault();
		}
		set
		{
			__pbn__LastServerPacketId = value;
		}
	}

	[ProtoMember(3, Name = "cps")]
	[DefaultValue("")]
	public string Cps
	{
		get
		{
			return __pbn__Cps ?? "";
		}
		set
		{
			__pbn__Cps = value;
		}
	}

	[ProtoMember(4, Name = "check_sum")]
	[DefaultValue("")]
	public string CheckSum
	{
		get
		{
			return __pbn__CheckSum ?? "";
		}
		set
		{
			__pbn__CheckSum = value;
		}
	}

	[ProtoMember(5, Name = "device_uuid")]
	[DefaultValue("")]
	public string DeviceUuid
	{
		get
		{
			return __pbn__DeviceUuid ?? "";
		}
		set
		{
			__pbn__DeviceUuid = value;
		}
	}

	[ProtoMember(6, Name = "android_signatures")]
	[DefaultValue("")]
	public string AndroidSignatures
	{
		get
		{
			return __pbn__AndroidSignatures ?? "";
		}
		set
		{
			__pbn__AndroidSignatures = value;
		}
	}

	[ProtoMember(7, Name = "language")]
	[DefaultValue("")]
	public string Language
	{
		get
		{
			return __pbn__Language ?? "";
		}
		set
		{
			__pbn__Language = value;
		}
	}

	[ProtoMember(8, Name = "advertiser_id")]
	[DefaultValue("")]
	public string AdvertiserId
	{
		get
		{
			return __pbn__AdvertiserId ?? "";
		}
		set
		{
			__pbn__AdvertiserId = value;
		}
	}

	[ProtoMember(9, Name = "security_cmd_reply")]
	public byte[] SecurityCmdReply
	{
		get
		{
			return __pbn__SecurityCmdReply;
		}
		set
		{
			__pbn__SecurityCmdReply = value;
		}
	}

	[ProtoMember(10, Name = "last_login_version")]
	public uint LastLoginVersion
	{
		get
		{
			return __pbn__LastLoginVersion.GetValueOrDefault();
		}
		set
		{
			__pbn__LastLoginVersion = value;
		}
	}

	[ProtoMember(11, Name = "account_type")]
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

	[ProtoMember(12, Name = "account_uid")]
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

	[ProtoMember(13, Name = "device_type")]
	[DefaultValue(DeviceType.DeviceTypeNone)]
	public DeviceType DeviceType
	{
		get
		{
			return __pbn__DeviceType.GetValueOrDefault();
		}
		set
		{
			__pbn__DeviceType = value;
		}
	}

	[ProtoMember(14, Name = "user_type")]
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

	[ProtoMember(15, Name = "user_ip")]
	[DefaultValue("")]
	public string UserIp
	{
		get
		{
			return __pbn__UserIp ?? "";
		}
		set
		{
			__pbn__UserIp = value;
		}
	}

	[ProtoMember(16, Name = "tag")]
	public uint Tag
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

	[ProtoMember(17, Name = "client_version")]
	[DefaultValue("")]
	public string ClientVersion
	{
		get
		{
			return __pbn__ClientVersion ?? "";
		}
		set
		{
			__pbn__ClientVersion = value;
		}
	}

	[ProtoMember(18, Name = "client_device")]
	[DefaultValue("")]
	public string ClientDevice
	{
		get
		{
			return __pbn__ClientDevice ?? "";
		}
		set
		{
			__pbn__ClientDevice = value;
		}
	}

	[ProtoMember(19, Name = "system_info")]
	[DefaultValue("")]
	public string SystemInfo
	{
		get
		{
			return __pbn__SystemInfo ?? "";
		}
		set
		{
			__pbn__SystemInfo = value;
		}
	}

	[ProtoMember(20, Name = "android_sign")]
	[DefaultValue("")]
	public string AndroidSign
	{
		get
		{
			return __pbn__AndroidSign ?? "";
		}
		set
		{
			__pbn__AndroidSign = value;
		}
	}

	[ProtoMember(21, Name = "limit_level")]
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

	[ProtoMember(22, Name = "black_list_end_time")]
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

	[ProtoMember(23, Name = "is_bind_access")]
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

	[ProtoMember(25, Name = "login_session_token")]
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

	[ProtoMember(27, Name = "user_port")]
	public uint UserPort
	{
		get
		{
			return __pbn__UserPort.GetValueOrDefault();
		}
		set
		{
			__pbn__UserPort = value;
		}
	}

	[ProtoMember(28, Name = "ua_pc")]
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

	public bool ShouldSerializeLastServerPacketId()
	{
		return __pbn__LastServerPacketId.HasValue;
	}

	public void ResetLastServerPacketId()
	{
		__pbn__LastServerPacketId = null;
	}

	public bool ShouldSerializeCps()
	{
		return __pbn__Cps != null;
	}

	public void ResetCps()
	{
		__pbn__Cps = null;
	}

	public bool ShouldSerializeCheckSum()
	{
		return __pbn__CheckSum != null;
	}

	public void ResetCheckSum()
	{
		__pbn__CheckSum = null;
	}

	public bool ShouldSerializeDeviceUuid()
	{
		return __pbn__DeviceUuid != null;
	}

	public void ResetDeviceUuid()
	{
		__pbn__DeviceUuid = null;
	}

	public bool ShouldSerializeAndroidSignatures()
	{
		return __pbn__AndroidSignatures != null;
	}

	public void ResetAndroidSignatures()
	{
		__pbn__AndroidSignatures = null;
	}

	public bool ShouldSerializeLanguage()
	{
		return __pbn__Language != null;
	}

	public void ResetLanguage()
	{
		__pbn__Language = null;
	}

	public bool ShouldSerializeAdvertiserId()
	{
		return __pbn__AdvertiserId != null;
	}

	public void ResetAdvertiserId()
	{
		__pbn__AdvertiserId = null;
	}

	public bool ShouldSerializeSecurityCmdReply()
	{
		return __pbn__SecurityCmdReply != null;
	}

	public void ResetSecurityCmdReply()
	{
		__pbn__SecurityCmdReply = null;
	}

	public bool ShouldSerializeLastLoginVersion()
	{
		return __pbn__LastLoginVersion.HasValue;
	}

	public void ResetLastLoginVersion()
	{
		__pbn__LastLoginVersion = null;
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

	public bool ShouldSerializeDeviceType()
	{
		return __pbn__DeviceType.HasValue;
	}

	public void ResetDeviceType()
	{
		__pbn__DeviceType = null;
	}

	public bool ShouldSerializeUserType()
	{
		return __pbn__UserType.HasValue;
	}

	public void ResetUserType()
	{
		__pbn__UserType = null;
	}

	public bool ShouldSerializeUserIp()
	{
		return __pbn__UserIp != null;
	}

	public void ResetUserIp()
	{
		__pbn__UserIp = null;
	}

	public bool ShouldSerializeTag()
	{
		return __pbn__Tag.HasValue;
	}

	public void ResetTag()
	{
		__pbn__Tag = null;
	}

	public bool ShouldSerializeClientVersion()
	{
		return __pbn__ClientVersion != null;
	}

	public void ResetClientVersion()
	{
		__pbn__ClientVersion = null;
	}

	public bool ShouldSerializeClientDevice()
	{
		return __pbn__ClientDevice != null;
	}

	public void ResetClientDevice()
	{
		__pbn__ClientDevice = null;
	}

	public bool ShouldSerializeSystemInfo()
	{
		return __pbn__SystemInfo != null;
	}

	public void ResetSystemInfo()
	{
		__pbn__SystemInfo = null;
	}

	public bool ShouldSerializeAndroidSign()
	{
		return __pbn__AndroidSign != null;
	}

	public void ResetAndroidSign()
	{
		__pbn__AndroidSign = null;
	}

	public bool ShouldSerializeLimitLevel()
	{
		return __pbn__LimitLevel.HasValue;
	}

	public void ResetLimitLevel()
	{
		__pbn__LimitLevel = null;
	}

	public bool ShouldSerializeBlackListEndTime()
	{
		return __pbn__BlackListEndTime.HasValue;
	}

	public void ResetBlackListEndTime()
	{
		__pbn__BlackListEndTime = null;
	}

	public bool ShouldSerializeIsBindAccess()
	{
		return __pbn__IsBindAccess.HasValue;
	}

	public void ResetIsBindAccess()
	{
		__pbn__IsBindAccess = null;
	}

	public bool ShouldSerializeLoginSessionToken()
	{
		return __pbn__LoginSessionToken.HasValue;
	}

	public void ResetLoginSessionToken()
	{
		__pbn__LoginSessionToken = null;
	}

	public bool ShouldSerializeHoyolabAccountUid()
	{
		return __pbn__HoyolabAccountUid != null;
	}

	public void ResetHoyolabAccountUid()
	{
		__pbn__HoyolabAccountUid = null;
	}

	public bool ShouldSerializeUserPort()
	{
		return __pbn__UserPort.HasValue;
	}

	public void ResetUserPort()
	{
		__pbn__UserPort = null;
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
