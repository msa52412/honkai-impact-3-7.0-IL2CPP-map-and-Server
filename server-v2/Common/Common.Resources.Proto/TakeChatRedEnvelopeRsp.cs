using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeChatRedEnvelopeRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1166
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken,
		[ProtoEnum(Name = "OVER")]
		Over,
		[ProtoEnum(Name = "LACK_TIMES")]
		LackTimes,
		[ProtoEnum(Name = "LACK_LEVEL")]
		LackLevel,
		[ProtoEnum(Name = "EXPIRE_TIME")]
		ExpireTime,
		[ProtoEnum(Name = "TAKE_BY_OTHERS")]
		TakeByOthers,
		[ProtoEnum(Name = "NO_CHATROOM")]
		NoChatroom,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "NOT_IN_WORLD_CHATROOM")]
		NotInWorldChatroom
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Channel;

	private uint? __pbn__SenderUid;

	private string __pbn__SenderNickname;

	private uint? __pbn__ItemId;

	private uint? __pbn__TakeScoin;

	private string __pbn__Msg;

	private uint? __pbn__RedEnvelopeId;

	private uint? __pbn__DropId;

	private uint? __pbn__TakeHcoin;

	private bool? __pbn__IsSenderNpc;

	private bool? __pbn__IsTakeOver;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "channel")]
	public uint Channel
	{
		get
		{
			return __pbn__Channel.GetValueOrDefault();
		}
		set
		{
			__pbn__Channel = value;
		}
	}

	[ProtoMember(3, Name = "sender_uid")]
	public uint SenderUid
	{
		get
		{
			return __pbn__SenderUid.GetValueOrDefault();
		}
		set
		{
			__pbn__SenderUid = value;
		}
	}

	[ProtoMember(4, Name = "sender_nickname")]
	[DefaultValue("")]
	public string SenderNickname
	{
		get
		{
			return __pbn__SenderNickname ?? "";
		}
		set
		{
			__pbn__SenderNickname = value;
		}
	}

	[ProtoMember(5, Name = "item_id")]
	public uint ItemId
	{
		get
		{
			return __pbn__ItemId.GetValueOrDefault();
		}
		set
		{
			__pbn__ItemId = value;
		}
	}

	[ProtoMember(6, Name = "take_scoin")]
	public uint TakeScoin
	{
		get
		{
			return __pbn__TakeScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeScoin = value;
		}
	}

	[ProtoMember(7, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(8, Name = "msg")]
	[DefaultValue("")]
	public string Msg
	{
		get
		{
			return __pbn__Msg ?? "";
		}
		set
		{
			__pbn__Msg = value;
		}
	}

	[ProtoMember(9, Name = "red_envelope_id")]
	public uint RedEnvelopeId
	{
		get
		{
			return __pbn__RedEnvelopeId.GetValueOrDefault();
		}
		set
		{
			__pbn__RedEnvelopeId = value;
		}
	}

	[ProtoMember(10, Name = "drop_id")]
	public uint DropId
	{
		get
		{
			return __pbn__DropId.GetValueOrDefault();
		}
		set
		{
			__pbn__DropId = value;
		}
	}

	[ProtoMember(11, Name = "take_hcoin")]
	public uint TakeHcoin
	{
		get
		{
			return __pbn__TakeHcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__TakeHcoin = value;
		}
	}

	[ProtoMember(12, Name = "is_sender_npc")]
	public bool IsSenderNpc
	{
		get
		{
			return __pbn__IsSenderNpc.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSenderNpc = value;
		}
	}

	[ProtoMember(13, Name = "take_material")]
	public Material TakeMaterial { get; set; }

	[ProtoMember(14, Name = "is_take_over")]
	public bool IsTakeOver
	{
		get
		{
			return __pbn__IsTakeOver.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeOver = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChannel()
	{
		return __pbn__Channel.HasValue;
	}

	public void ResetChannel()
	{
		__pbn__Channel = null;
	}

	public bool ShouldSerializeSenderUid()
	{
		return __pbn__SenderUid.HasValue;
	}

	public void ResetSenderUid()
	{
		__pbn__SenderUid = null;
	}

	public bool ShouldSerializeSenderNickname()
	{
		return __pbn__SenderNickname != null;
	}

	public void ResetSenderNickname()
	{
		__pbn__SenderNickname = null;
	}

	public bool ShouldSerializeItemId()
	{
		return __pbn__ItemId.HasValue;
	}

	public void ResetItemId()
	{
		__pbn__ItemId = null;
	}

	public bool ShouldSerializeTakeScoin()
	{
		return __pbn__TakeScoin.HasValue;
	}

	public void ResetTakeScoin()
	{
		__pbn__TakeScoin = null;
	}

	public bool ShouldSerializeMsg()
	{
		return __pbn__Msg != null;
	}

	public void ResetMsg()
	{
		__pbn__Msg = null;
	}

	public bool ShouldSerializeRedEnvelopeId()
	{
		return __pbn__RedEnvelopeId.HasValue;
	}

	public void ResetRedEnvelopeId()
	{
		__pbn__RedEnvelopeId = null;
	}

	public bool ShouldSerializeDropId()
	{
		return __pbn__DropId.HasValue;
	}

	public void ResetDropId()
	{
		__pbn__DropId = null;
	}

	public bool ShouldSerializeTakeHcoin()
	{
		return __pbn__TakeHcoin.HasValue;
	}

	public void ResetTakeHcoin()
	{
		__pbn__TakeHcoin = null;
	}

	public bool ShouldSerializeIsSenderNpc()
	{
		return __pbn__IsSenderNpc.HasValue;
	}

	public void ResetIsSenderNpc()
	{
		__pbn__IsSenderNpc = null;
	}

	public bool ShouldSerializeIsTakeOver()
	{
		return __pbn__IsTakeOver.HasValue;
	}

	public void ResetIsTakeOver()
	{
		__pbn__IsTakeOver = null;
	}
}
