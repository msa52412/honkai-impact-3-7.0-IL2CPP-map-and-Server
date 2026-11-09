using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__IslandLikeNum;

	private uint? __pbn__OnPhonePendantId;

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

	[ProtoMember(2, Name = "avatar_list")]
	public List<PlayerCardAvatarData> AvatarLists { get; } = new List<PlayerCardAvatarData>();


	[ProtoMember(3, Name = "medal_list")]
	public List<PlayerCardMedalData> MedalLists { get; } = new List<PlayerCardMedalData>();


	[ProtoMember(4, Name = "msg_data")]
	public PlayerCardMsgData MsgData { get; set; }

	[ProtoMember(5, Name = "island_like_num")]
	public uint IslandLikeNum
	{
		get
		{
			return __pbn__IslandLikeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__IslandLikeNum = value;
		}
	}

	[ProtoMember(6, Name = "on_phone_pendant_id")]
	public uint OnPhonePendantId
	{
		get
		{
			return __pbn__OnPhonePendantId.GetValueOrDefault();
		}
		set
		{
			__pbn__OnPhonePendantId = value;
		}
	}

	[ProtoMember(7, Name = "elf_list")]
	public List<PlayerCardElfData> ElfLists { get; } = new List<PlayerCardElfData>();


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

	public bool ShouldSerializeIslandLikeNum()
	{
		return __pbn__IslandLikeNum.HasValue;
	}

	public void ResetIslandLikeNum()
	{
		__pbn__IslandLikeNum = null;
	}

	public bool ShouldSerializeOnPhonePendantId()
	{
		return __pbn__OnPhonePendantId.HasValue;
	}

	public void ResetOnPhonePendantId()
	{
		__pbn__OnPhonePendantId = null;
	}
}
