using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaBriefData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private string __pbn__Name;

	private uint? __pbn__Level;

	private string __pbn__Bulletin;

	private uint? __pbn__ApplyLevel;

	private bool? __pbn__AutoPass;

	private uint? __pbn__AutoPassLevel;

	private uint? __pbn__Fund;

	private uint? __pbn__FundCap;

	private uint? __pbn__Ap;

	private uint? __pbn__ApCap;

	private uint? __pbn__Popl;

	private uint? __pbn__PoplCap;

	private uint? __pbn__MemberNum;

	private uint? __pbn__MemberCap;

	private uint? __pbn__LeaderId;

	private string __pbn__LeaderName;

	private string __pbn__Contact;

	private uint? __pbn__Contribution;

	private bool? __pbn__IsLowPopl;

	private uint? __pbn__RecentContribution;

	private uint? __pbn__ActiveLevel;

	private uint? __pbn__ActiveMemberNum;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(3, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(4, Name = "bulletin")]
	[DefaultValue("")]
	public string Bulletin
	{
		get
		{
			return __pbn__Bulletin ?? "";
		}
		set
		{
			__pbn__Bulletin = value;
		}
	}

	[ProtoMember(5, Name = "apply_level")]
	public uint ApplyLevel
	{
		get
		{
			return __pbn__ApplyLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ApplyLevel = value;
		}
	}

	[ProtoMember(6, Name = "auto_pass")]
	public bool AutoPass
	{
		get
		{
			return __pbn__AutoPass.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPass = value;
		}
	}

	[ProtoMember(7, Name = "auto_pass_level")]
	public uint AutoPassLevel
	{
		get
		{
			return __pbn__AutoPassLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__AutoPassLevel = value;
		}
	}

	[ProtoMember(8, Name = "fund")]
	public uint Fund
	{
		get
		{
			return __pbn__Fund.GetValueOrDefault();
		}
		set
		{
			__pbn__Fund = value;
		}
	}

	[ProtoMember(9, Name = "fund_cap")]
	public uint FundCap
	{
		get
		{
			return __pbn__FundCap.GetValueOrDefault();
		}
		set
		{
			__pbn__FundCap = value;
		}
	}

	[ProtoMember(10, Name = "ap")]
	public uint Ap
	{
		get
		{
			return __pbn__Ap.GetValueOrDefault();
		}
		set
		{
			__pbn__Ap = value;
		}
	}

	[ProtoMember(11, Name = "ap_cap")]
	public uint ApCap
	{
		get
		{
			return __pbn__ApCap.GetValueOrDefault();
		}
		set
		{
			__pbn__ApCap = value;
		}
	}

	[ProtoMember(12, Name = "popl")]
	public uint Popl
	{
		get
		{
			return __pbn__Popl.GetValueOrDefault();
		}
		set
		{
			__pbn__Popl = value;
		}
	}

	[ProtoMember(13, Name = "popl_cap")]
	public uint PoplCap
	{
		get
		{
			return __pbn__PoplCap.GetValueOrDefault();
		}
		set
		{
			__pbn__PoplCap = value;
		}
	}

	[ProtoMember(14, Name = "member_num")]
	public uint MemberNum
	{
		get
		{
			return __pbn__MemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__MemberNum = value;
		}
	}

	[ProtoMember(15, Name = "member_cap")]
	public uint MemberCap
	{
		get
		{
			return __pbn__MemberCap.GetValueOrDefault();
		}
		set
		{
			__pbn__MemberCap = value;
		}
	}

	[ProtoMember(16, Name = "leader_id")]
	public uint LeaderId
	{
		get
		{
			return __pbn__LeaderId.GetValueOrDefault();
		}
		set
		{
			__pbn__LeaderId = value;
		}
	}

	[ProtoMember(17, Name = "leader_name")]
	[DefaultValue("")]
	public string LeaderName
	{
		get
		{
			return __pbn__LeaderName ?? "";
		}
		set
		{
			__pbn__LeaderName = value;
		}
	}

	[ProtoMember(18, Name = "contact")]
	[DefaultValue("")]
	public string Contact
	{
		get
		{
			return __pbn__Contact ?? "";
		}
		set
		{
			__pbn__Contact = value;
		}
	}

	[ProtoMember(19, Name = "contribution")]
	public uint Contribution
	{
		get
		{
			return __pbn__Contribution.GetValueOrDefault();
		}
		set
		{
			__pbn__Contribution = value;
		}
	}

	[ProtoMember(20, Name = "is_low_popl")]
	public bool IsLowPopl
	{
		get
		{
			return __pbn__IsLowPopl.GetValueOrDefault();
		}
		set
		{
			__pbn__IsLowPopl = value;
		}
	}

	[ProtoMember(21, Name = "recent_contribution")]
	public uint RecentContribution
	{
		get
		{
			return __pbn__RecentContribution.GetValueOrDefault();
		}
		set
		{
			__pbn__RecentContribution = value;
		}
	}

	[ProtoMember(23, Name = "active_level")]
	public uint ActiveLevel
	{
		get
		{
			return __pbn__ActiveLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveLevel = value;
		}
	}

	[ProtoMember(24, Name = "active_member_num")]
	public uint ActiveMemberNum
	{
		get
		{
			return __pbn__ActiveMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveMemberNum = value;
		}
	}

	[ProtoMember(25, Name = "friend_uid_list")]
	public uint[] FriendUidLists { get; set; }

	[ProtoMember(26, Name = "leader_brief_data")]
	public PlayerFriendBriefData LeaderBriefData { get; set; }

	[ProtoMember(27, Name = "label")]
	public ArmadaLabel Label { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeBulletin()
	{
		return __pbn__Bulletin != null;
	}

	public void ResetBulletin()
	{
		__pbn__Bulletin = null;
	}

	public bool ShouldSerializeApplyLevel()
	{
		return __pbn__ApplyLevel.HasValue;
	}

	public void ResetApplyLevel()
	{
		__pbn__ApplyLevel = null;
	}

	public bool ShouldSerializeAutoPass()
	{
		return __pbn__AutoPass.HasValue;
	}

	public void ResetAutoPass()
	{
		__pbn__AutoPass = null;
	}

	public bool ShouldSerializeAutoPassLevel()
	{
		return __pbn__AutoPassLevel.HasValue;
	}

	public void ResetAutoPassLevel()
	{
		__pbn__AutoPassLevel = null;
	}

	public bool ShouldSerializeFund()
	{
		return __pbn__Fund.HasValue;
	}

	public void ResetFund()
	{
		__pbn__Fund = null;
	}

	public bool ShouldSerializeFundCap()
	{
		return __pbn__FundCap.HasValue;
	}

	public void ResetFundCap()
	{
		__pbn__FundCap = null;
	}

	public bool ShouldSerializeAp()
	{
		return __pbn__Ap.HasValue;
	}

	public void ResetAp()
	{
		__pbn__Ap = null;
	}

	public bool ShouldSerializeApCap()
	{
		return __pbn__ApCap.HasValue;
	}

	public void ResetApCap()
	{
		__pbn__ApCap = null;
	}

	public bool ShouldSerializePopl()
	{
		return __pbn__Popl.HasValue;
	}

	public void ResetPopl()
	{
		__pbn__Popl = null;
	}

	public bool ShouldSerializePoplCap()
	{
		return __pbn__PoplCap.HasValue;
	}

	public void ResetPoplCap()
	{
		__pbn__PoplCap = null;
	}

	public bool ShouldSerializeMemberNum()
	{
		return __pbn__MemberNum.HasValue;
	}

	public void ResetMemberNum()
	{
		__pbn__MemberNum = null;
	}

	public bool ShouldSerializeMemberCap()
	{
		return __pbn__MemberCap.HasValue;
	}

	public void ResetMemberCap()
	{
		__pbn__MemberCap = null;
	}

	public bool ShouldSerializeLeaderId()
	{
		return __pbn__LeaderId.HasValue;
	}

	public void ResetLeaderId()
	{
		__pbn__LeaderId = null;
	}

	public bool ShouldSerializeLeaderName()
	{
		return __pbn__LeaderName != null;
	}

	public void ResetLeaderName()
	{
		__pbn__LeaderName = null;
	}

	public bool ShouldSerializeContact()
	{
		return __pbn__Contact != null;
	}

	public void ResetContact()
	{
		__pbn__Contact = null;
	}

	public bool ShouldSerializeContribution()
	{
		return __pbn__Contribution.HasValue;
	}

	public void ResetContribution()
	{
		__pbn__Contribution = null;
	}

	public bool ShouldSerializeIsLowPopl()
	{
		return __pbn__IsLowPopl.HasValue;
	}

	public void ResetIsLowPopl()
	{
		__pbn__IsLowPopl = null;
	}

	public bool ShouldSerializeRecentContribution()
	{
		return __pbn__RecentContribution.HasValue;
	}

	public void ResetRecentContribution()
	{
		__pbn__RecentContribution = null;
	}

	public bool ShouldSerializeActiveLevel()
	{
		return __pbn__ActiveLevel.HasValue;
	}

	public void ResetActiveLevel()
	{
		__pbn__ActiveLevel = null;
	}

	public bool ShouldSerializeActiveMemberNum()
	{
		return __pbn__ActiveMemberNum.HasValue;
	}

	public void ResetActiveMemberNum()
	{
		__pbn__ActiveMemberNum = null;
	}
}
