using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleSendEmojiNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6599
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EmojiId;

	private uint? __pbn__Uid;

	private uint? __pbn__EntityId;

	[ProtoMember(1, Name = "emoji_id")]
	public uint EmojiId
	{
		get
		{
			return __pbn__EmojiId.GetValueOrDefault();
		}
		set
		{
			__pbn__EmojiId = value;
		}
	}

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

	[ProtoMember(3, Name = "entity_id")]
	public uint EntityId
	{
		get
		{
			return __pbn__EntityId.GetValueOrDefault();
		}
		set
		{
			__pbn__EntityId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEmojiId()
	{
		return __pbn__EmojiId.HasValue;
	}

	public void ResetEmojiId()
	{
		__pbn__EmojiId = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeEntityId()
	{
		return __pbn__EntityId.HasValue;
	}

	public void ResetEntityId()
	{
		__pbn__EntityId = null;
	}
}
