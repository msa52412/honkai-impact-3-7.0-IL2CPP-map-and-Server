using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardDataChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 492
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__MsgConfig;

	[ProtoMember(1, Name = "uid")]
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

	[ProtoMember(2, Name = "msg_config")]
	public uint MsgConfig
	{
		get
		{
			return __pbn__MsgConfig.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgConfig = value;
		}
	}

	[ProtoMember(3, Name = "medal")]
	public PlayerCardMedalData Medal { get; set; }

	[ProtoMember(4, Name = "avatar")]
	public PlayerCardAvatarData Avatar { get; set; }

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

	public bool ShouldSerializeMsgConfig()
	{
		return __pbn__MsgConfig.HasValue;
	}

	public void ResetMsgConfig()
	{
		__pbn__MsgConfig = null;
	}
}
