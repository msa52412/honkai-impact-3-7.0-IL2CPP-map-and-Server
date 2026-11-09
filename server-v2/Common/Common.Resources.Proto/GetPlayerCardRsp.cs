using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetPlayerCardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 481
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private PlayerCardType? __pbn__Type;

	private uint? __pbn__MsgConfig;

	private uint? __pbn__LastReadMsgIndex;

	private uint? __pbn__MpLikeNum;

	private uint? __pbn__IslandLikeNum;

	private uint? __pbn__CampaignLikeNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "type")]
	[DefaultValue(PlayerCardType.CardAll)]
	public PlayerCardType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(3, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(4, Name = "medal_id_list")]
	public uint[] MedalIdLists { get; set; }

	[ProtoMember(5, Name = "msg_config")]
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

	[ProtoMember(6, Name = "msg_data")]
	public PlayerCardMsgData MsgData { get; set; }

	[ProtoMember(7, Name = "last_read_msg_index")]
	public uint LastReadMsgIndex
	{
		get
		{
			return __pbn__LastReadMsgIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__LastReadMsgIndex = value;
		}
	}

	[ProtoMember(8, Name = "mp_like_num")]
	public uint MpLikeNum
	{
		get
		{
			return __pbn__MpLikeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MpLikeNum = value;
		}
	}

	[ProtoMember(9, Name = "island_like_num")]
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

	[ProtoMember(10, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	[ProtoMember(11, Name = "campaign_like_num")]
	public uint CampaignLikeNum
	{
		get
		{
			return __pbn__CampaignLikeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__CampaignLikeNum = value;
		}
	}

	[ProtoMember(12, Name = "medal_list")]
	public List<Medal> MedalLists { get; } = new List<Medal>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeMsgConfig()
	{
		return __pbn__MsgConfig.HasValue;
	}

	public void ResetMsgConfig()
	{
		__pbn__MsgConfig = null;
	}

	public bool ShouldSerializeLastReadMsgIndex()
	{
		return __pbn__LastReadMsgIndex.HasValue;
	}

	public void ResetLastReadMsgIndex()
	{
		__pbn__LastReadMsgIndex = null;
	}

	public bool ShouldSerializeMpLikeNum()
	{
		return __pbn__MpLikeNum.HasValue;
	}

	public void ResetMpLikeNum()
	{
		__pbn__MpLikeNum = null;
	}

	public bool ShouldSerializeIslandLikeNum()
	{
		return __pbn__IslandLikeNum.HasValue;
	}

	public void ResetIslandLikeNum()
	{
		__pbn__IslandLikeNum = null;
	}

	public bool ShouldSerializeCampaignLikeNum()
	{
		return __pbn__CampaignLikeNum.HasValue;
	}

	public void ResetCampaignLikeNum()
	{
		__pbn__CampaignLikeNum = null;
	}
}
