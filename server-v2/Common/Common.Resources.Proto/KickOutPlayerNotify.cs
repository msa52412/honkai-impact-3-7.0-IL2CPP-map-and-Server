using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KickOutPlayerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5007
	}

	[ProtoContract]
	public enum KickOutReason
	{
		[ProtoEnum(Name = "REASON_NONE")]
		ReasonNone,
		[ProtoEnum(Name = "REASON_MTP_ERROR")]
		ReasonMtpError,
		[ProtoEnum(Name = "REASON_PLATFORM_ERROR")]
		ReasonPlatformError,
		[ProtoEnum(Name = "REASON_SQUEEZE")]
		ReasonSqueeze,
		[ProtoEnum(Name = "REASON_LOGIN_UNFINISH")]
		ReasonLoginUnfinish
	}

	private IExtension __pbn__extensionData;

	private KickOutReason? __pbn__Reason;

	[ProtoMember(1, Name = "reason")]
	[DefaultValue(KickOutReason.ReasonNone)]
	public KickOutReason Reason
	{
		get
		{
			return __pbn__Reason.GetValueOrDefault();
		}
		set
		{
			__pbn__Reason = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReason()
	{
		return __pbn__Reason.HasValue;
	}

	public void ResetReason()
	{
		__pbn__Reason = null;
	}
}
