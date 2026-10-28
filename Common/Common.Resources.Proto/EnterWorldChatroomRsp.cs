using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterWorldChatroomRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2232
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "CHATROOM_ID_ERROR")]
		ChatroomIdError,
		[ProtoEnum(Name = "CHATROOM_FULL")]
		ChatroomFull,
		[ProtoEnum(Name = "NO_KINGDOM")]
		NoKingdom,
		[ProtoEnum(Name = "TOO_OFTEN")]
		TooOften,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "NO_GLOBAL_EXPLORE_KINGDOM")]
		NoGlobalExploreKingdom
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatroomId;

	private ActivityWorldChatroomType? __pbn__ActivityType;

	private uint? __pbn__PlayerNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chatroom_id")]
	public uint ChatroomId
	{
		get
		{
			return __pbn__ChatroomId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatroomId = value;
		}
	}

	[ProtoMember(3, Name = "his_chat_msg_list")]
	public List<ChatMsg> HisChatMsgLists { get; } = new List<ChatMsg>();


	[ProtoMember(4, Name = "activity_type")]
	[DefaultValue(ActivityWorldChatroomType.ActivityWorldChatroomTypeNone)]
	public ActivityWorldChatroomType ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	[ProtoMember(5, Name = "player_num")]
	public uint PlayerNum
	{
		get
		{
			return __pbn__PlayerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatroomId()
	{
		return __pbn__ChatroomId.HasValue;
	}

	public void ResetChatroomId()
	{
		__pbn__ChatroomId = null;
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}

	public bool ShouldSerializePlayerNum()
	{
		return __pbn__PlayerNum.HasValue;
	}

	public void ResetPlayerNum()
	{
		__pbn__PlayerNum = null;
	}
}
