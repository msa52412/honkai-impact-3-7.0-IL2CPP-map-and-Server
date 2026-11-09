using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RoomMemberSetVoiceIdRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3180
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ROOM")]
		NotInRoom
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__VoiceId;

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

	[ProtoMember(3, Name = "voice_id")]
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

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
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
