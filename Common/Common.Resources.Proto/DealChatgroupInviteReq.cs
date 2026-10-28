using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DealChatgroupInviteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2214
	}

	[ProtoContract]
	public enum InviteAction
	{
		[ProtoEnum(Name = "INVITE_NONE")]
		InviteNone,
		[ProtoEnum(Name = "INVITE_AGREE")]
		InviteAgree,
		[ProtoEnum(Name = "INVITE_REJECT")]
		InviteReject
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	private InviteAction? __pbn__Action;

	[ProtoMember(1, Name = "chatgroup_id")]
	public uint ChatgroupId
	{
		get
		{
			return __pbn__ChatgroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupId = value;
		}
	}

	[ProtoMember(2, Name = "action")]
	[DefaultValue(InviteAction.InviteNone)]
	public InviteAction Action
	{
		get
		{
			return __pbn__Action.GetValueOrDefault();
		}
		set
		{
			__pbn__Action = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}

	public bool ShouldSerializeAction()
	{
		return __pbn__Action.HasValue;
	}

	public void ResetAction()
	{
		__pbn__Action = null;
	}
}
