using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatTakeRedEnvelope : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SenderUid;

	private string __pbn__SenderNickname;

	private uint? __pbn__ItemId;

	private uint? __pbn__TakeScoin;

	private string __pbn__Msg;

	[ProtoMember(1, Name = "sender_uid")]
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

	[ProtoMember(2, Name = "sender_nickname")]
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

	[ProtoMember(3, Name = "item_id")]
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

	[ProtoMember(4, Name = "take_scoin")]
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

	[ProtoMember(5, Name = "drop_item_list")]
	public List<DropItem> DropItemLists { get; } = new List<DropItem>();


	[ProtoMember(6, Name = "msg")]
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

	[ProtoMember(7, Name = "take_material")]
	public Material TakeMaterial { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
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
}
