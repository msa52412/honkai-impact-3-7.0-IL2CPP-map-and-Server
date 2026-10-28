using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BindHoyolabAccountReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1658
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HoyolabChannelId;

	private string __pbn__HoyolabAccountUid;

	private string __pbn__HoyolabToken;

	[ProtoMember(1, Name = "hoyolab_channel_id")]
	public uint HoyolabChannelId
	{
		get
		{
			return __pbn__HoyolabChannelId.GetValueOrDefault();
		}
		set
		{
			__pbn__HoyolabChannelId = value;
		}
	}

	[ProtoMember(2, Name = "hoyolab_account_uid")]
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

	[ProtoMember(3, Name = "hoyolab_token")]
	[DefaultValue("")]
	public string HoyolabToken
	{
		get
		{
			return __pbn__HoyolabToken ?? "";
		}
		set
		{
			__pbn__HoyolabToken = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHoyolabChannelId()
	{
		return __pbn__HoyolabChannelId.HasValue;
	}

	public void ResetHoyolabChannelId()
	{
		__pbn__HoyolabChannelId = null;
	}

	public bool ShouldSerializeHoyolabAccountUid()
	{
		return __pbn__HoyolabAccountUid != null;
	}

	public void ResetHoyolabAccountUid()
	{
		__pbn__HoyolabAccountUid = null;
	}

	public bool ShouldSerializeHoyolabToken()
	{
		return __pbn__HoyolabToken != null;
	}

	public void ResetHoyolabToken()
	{
		__pbn__HoyolabToken = null;
	}
}
