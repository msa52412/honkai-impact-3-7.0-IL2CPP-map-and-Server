using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetInviteFriendRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 224
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

	private string __pbn__MyInviteCode;

	private uint? __pbn__HasInviteNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "my_invite_code")]
	[DefaultValue("")]
	public string MyInviteCode
	{
		get
		{
			return __pbn__MyInviteCode ?? "";
		}
		set
		{
			__pbn__MyInviteCode = value;
		}
	}

	[ProtoMember(3, Name = "has_invite_num")]
	public uint HasInviteNum
	{
		get
		{
			return __pbn__HasInviteNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasInviteNum = value;
		}
	}

	[ProtoMember(4, Name = "my_invite_reward_list")]
	public List<InviteFriendRewardData> MyInviteRewardLists { get; } = new List<InviteFriendRewardData>();


	[ProtoMember(5, Name = "invite_activity")]
	public InviteActivityData InviteActivity { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMyInviteCode()
	{
		return __pbn__MyInviteCode != null;
	}

	public void ResetMyInviteCode()
	{
		__pbn__MyInviteCode = null;
	}

	public bool ShouldSerializeHasInviteNum()
	{
		return __pbn__HasInviteNum.HasValue;
	}

	public void ResetHasInviteNum()
	{
		__pbn__HasInviteNum = null;
	}
}
