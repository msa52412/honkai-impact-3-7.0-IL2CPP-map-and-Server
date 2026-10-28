using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpTeamMemberSetVoiceIdReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 396
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__VoiceId;

	[ProtoMember(1, Name = "voice_id")]
	public uint VoiceId
	{
		get
		{
			return __pbn__VoiceId.GetValueOrDefault();
		}
		set
		{
			__pbn__VoiceId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeVoiceId()
	{
		return __pbn__VoiceId.HasValue;
	}

	public void ResetVoiceId()
	{
		__pbn__VoiceId = null;
	}
}
