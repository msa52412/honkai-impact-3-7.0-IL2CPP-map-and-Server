using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGalInteractTriggerEventRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 816
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_SUCH_AVATAR")]
		NoSuchAvatar,
		[ProtoEnum(Name = "EVENT_ID_ERROR")]
		EventIdError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__EventId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(3, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	[ProtoMember(4, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(5, Name = "drop_item")]
	public DropItem DropItem { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}
}
