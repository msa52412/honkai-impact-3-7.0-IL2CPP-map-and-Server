using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldFishKickPlayerNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2444
	}

	[ProtoContract]
	public enum KickReason
	{
		[ProtoEnum(Name = "KICK_NONE")]
		KickNone,
		[ProtoEnum(Name = "KICK_CURRENCY_FULL")]
		KickCurrencyFull,
		[ProtoEnum(Name = "KICK_NO_ACTION")]
		KickNoAction
	}

	private IExtension __pbn__extensionData;

	private KickReason? __pbn__kick_reason;

	[ProtoMember(1)]
	[DefaultValue(KickReason.KickNone)]
	public KickReason kick_reason
	{
		get
		{
			return __pbn__kick_reason.GetValueOrDefault();
		}
		set
		{
			__pbn__kick_reason = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializekick_reason()
	{
		return __pbn__kick_reason.HasValue;
	}

	public void Resetkick_reason()
	{
		__pbn__kick_reason = null;
	}
}
