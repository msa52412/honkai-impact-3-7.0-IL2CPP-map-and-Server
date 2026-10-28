using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetPlayerTokenReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AccountType;

	private string __pbn__AccountUid;

	private string __pbn__AccountToken;

	private string __pbn__AccountExt;

	private string __pbn__Token;

	private string __pbn__Version;

	private string __pbn__Device;

	private int? __pbn__Tag;

	private uint? __pbn__Uid;

	private string __pbn__SystemInfo;

	private string __pbn__Ipv4Str;

	private string __pbn__Ipv6Str;

	private string __pbn__Cps;

	private string __pbn__DeviceUuid;

	private string __pbn__TransferCode;

	private string __pbn__TransferPwd;

	private string __pbn__IpInfoStr;

	private DeviceType? __pbn__DeviceType;

	private bool? __pbn__IsAllowDeviceRegister;

	private uint? __pbn__ComboChannelId;

	private string __pbn__ComboToken;

	private string __pbn__UserIpStr;

	private string __pbn__UaPc;

	[ProtoMember(1, Name = "account_type")]
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

	[ProtoMember(2, Name = "account_uid")]
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

	[ProtoMember(3, Name = "account_token")]
	[DefaultValue("")]
	public string AccountToken
	{
		get
		{
			return __pbn__AccountToken ?? "";
		}
		set
		{
			__pbn__AccountToken = value;
		}
	}

	[ProtoMember(4, Name = "account_ext")]
	[DefaultValue("")]
	public string AccountExt
	{
		get
		{
			return __pbn__AccountExt ?? "";
		}
		set
		{
			__pbn__AccountExt = value;
		}
	}

	[ProtoMember(5, Name = "token")]
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

	[ProtoMember(6, Name = "version")]
	[DefaultValue("")]
	public string Version
	{
		get
		{
			return __pbn__Version ?? "";
		}
		set
		{
			__pbn__Version = value;
		}
	}

	[ProtoMember(7, Name = "device")]
	[DefaultValue("")]
	public string Device
	{
		get
		{
			return __pbn__Device ?? "";
		}
		set
		{
			__pbn__Device = value;
		}
	}

	[ProtoMember(8, Name = "tag")]
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

	[ProtoMember(9, Name = "uid")]
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

	[ProtoMember(10, Name = "system_info")]
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

	[ProtoMember(11, Name = "ipv4_str")]
	[DefaultValue("")]
	public string Ipv4Str
	{
		get
		{
			return __pbn__Ipv4Str ?? "";
		}
		set
		{
			__pbn__Ipv4Str = value;
		}
	}

	[ProtoMember(12, Name = "ipv6_str")]
	[DefaultValue("")]
	public string Ipv6Str
	{
		get
		{
			return __pbn__Ipv6Str ?? "";
		}
		set
		{
			__pbn__Ipv6Str = value;
		}
	}

	[ProtoMember(13, Name = "cps")]
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

	[ProtoMember(14, Name = "device_uuid")]
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

	[ProtoMember(16, Name = "transfer_pwd")]
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

	[ProtoMember(17, Name = "ip_info_str")]
	[DefaultValue("")]
	public string IpInfoStr
	{
		get
		{
			return __pbn__IpInfoStr ?? "";
		}
		set
		{
			__pbn__IpInfoStr = value;
		}
	}

	[ProtoMember(20, Name = "device_type")]
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

	[ProtoMember(21, Name = "is_allow_device_register")]
	public bool IsAllowDeviceRegister
	{
		get
		{
			return __pbn__IsAllowDeviceRegister.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAllowDeviceRegister = value;
		}
	}

	[ProtoMember(22, Name = "combo_channel_id")]
	public uint ComboChannelId
	{
		get
		{
			return __pbn__ComboChannelId.GetValueOrDefault();
		}
		set
		{
			__pbn__ComboChannelId = value;
		}
	}

	[ProtoMember(23, Name = "combo_token")]
	[DefaultValue("")]
	public string ComboToken
	{
		get
		{
			return __pbn__ComboToken ?? "";
		}
		set
		{
			__pbn__ComboToken = value;
		}
	}

	[ProtoMember(24, Name = "user_ip_str")]
	[DefaultValue("")]
	public string UserIpStr
	{
		get
		{
			return __pbn__UserIpStr ?? "";
		}
		set
		{
			__pbn__UserIpStr = value;
		}
	}

	[ProtoMember(25, Name = "ua_pc")]
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

	public bool ShouldSerializeAccountToken()
	{
		return __pbn__AccountToken != null;
	}

	public void ResetAccountToken()
	{
		__pbn__AccountToken = null;
	}

	public bool ShouldSerializeAccountExt()
	{
		return __pbn__AccountExt != null;
	}

	public void ResetAccountExt()
	{
		__pbn__AccountExt = null;
	}

	public bool ShouldSerializeToken()
	{
		return __pbn__Token != null;
	}

	public void ResetToken()
	{
		__pbn__Token = null;
	}

	public bool ShouldSerializeVersion()
	{
		return __pbn__Version != null;
	}

	public void ResetVersion()
	{
		__pbn__Version = null;
	}

	public bool ShouldSerializeDevice()
	{
		return __pbn__Device != null;
	}

	public void ResetDevice()
	{
		__pbn__Device = null;
	}

	public bool ShouldSerializeTag()
	{
		return __pbn__Tag.HasValue;
	}

	public void ResetTag()
	{
		__pbn__Tag = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeSystemInfo()
	{
		return __pbn__SystemInfo != null;
	}

	public void ResetSystemInfo()
	{
		__pbn__SystemInfo = null;
	}

	public bool ShouldSerializeIpv4Str()
	{
		return __pbn__Ipv4Str != null;
	}

	public void ResetIpv4Str()
	{
		__pbn__Ipv4Str = null;
	}

	public bool ShouldSerializeIpv6Str()
	{
		return __pbn__Ipv6Str != null;
	}

	public void ResetIpv6Str()
	{
		__pbn__Ipv6Str = null;
	}

	public bool ShouldSerializeCps()
	{
		return __pbn__Cps != null;
	}

	public void ResetCps()
	{
		__pbn__Cps = null;
	}

	public bool ShouldSerializeDeviceUuid()
	{
		return __pbn__DeviceUuid != null;
	}

	public void ResetDeviceUuid()
	{
		__pbn__DeviceUuid = null;
	}

	public bool ShouldSerializeTransferCode()
	{
		return __pbn__TransferCode != null;
	}

	public void ResetTransferCode()
	{
		__pbn__TransferCode = null;
	}

	public bool ShouldSerializeTransferPwd()
	{
		return __pbn__TransferPwd != null;
	}

	public void ResetTransferPwd()
	{
		__pbn__TransferPwd = null;
	}

	public bool ShouldSerializeIpInfoStr()
	{
		return __pbn__IpInfoStr != null;
	}

	public void ResetIpInfoStr()
	{
		__pbn__IpInfoStr = null;
	}

	public bool ShouldSerializeDeviceType()
	{
		return __pbn__DeviceType.HasValue;
	}

	public void ResetDeviceType()
	{
		__pbn__DeviceType = null;
	}

	public bool ShouldSerializeIsAllowDeviceRegister()
	{
		return __pbn__IsAllowDeviceRegister.HasValue;
	}

	public void ResetIsAllowDeviceRegister()
	{
		__pbn__IsAllowDeviceRegister = null;
	}

	public bool ShouldSerializeComboChannelId()
	{
		return __pbn__ComboChannelId.HasValue;
	}

	public void ResetComboChannelId()
	{
		__pbn__ComboChannelId = null;
	}

	public bool ShouldSerializeComboToken()
	{
		return __pbn__ComboToken != null;
	}

	public void ResetComboToken()
	{
		__pbn__ComboToken = null;
	}

	public bool ShouldSerializeUserIpStr()
	{
		return __pbn__UserIpStr != null;
	}

	public void ResetUserIpStr()
	{
		__pbn__UserIpStr = null;
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
