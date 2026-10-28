using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleSendEmojiReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6597
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EmojiId;

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
}
