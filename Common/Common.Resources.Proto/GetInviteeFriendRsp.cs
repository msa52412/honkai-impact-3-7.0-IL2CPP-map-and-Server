using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetInviteeFriendRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 226
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack
	}

	private IExtension __pbn__extensionData;

	private string __pbn__InviteeCode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "invitee_code")]
	[DefaultValue("")]
	public string InviteeCode
	{
		get
		{
			return __pbn__InviteeCode ?? "";
		}
		set
		{
			__pbn__InviteeCode = value;
		}
	}

	[ProtoMember(3, Name = "invitee_reward_list")]
	public List<InviteeFriendRewardData> InviteeRewardLists { get; } = new List<InviteeFriendRewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInviteeCode()
	{
		return __pbn__InviteeCode != null;
	}

	public void ResetInviteeCode()
	{
		__pbn__InviteeCode = null;
	}
}
