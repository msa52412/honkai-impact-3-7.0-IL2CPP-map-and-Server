using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleSendEmojiRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6598
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "EMOJI_INVALID")]
		EmojiInvalid,
		[ProtoEnum(Name = "EMOJI_IN_CD")]
		EmojiInCd
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EmojiId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "emoji_id")]
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
