using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityEndNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6464
	}

	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "WIN")]
		Win,
		[ProtoEnum(Name = "LOSE")]
		Lose,
		[ProtoEnum(Name = "ABANDON")]
		Abandon
	}

	private IExtension __pbn__extensionData;

	private Status? __pbn__status;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2)]
	[DefaultValue(Status.Win)]
	public Status status
	{
		get
		{
			return __pbn__status.GetValueOrDefault();
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(3, Name = "challenge_list")]
	public List<TownChallenge> ChallengeLists { get; } = new List<TownChallenge>();


	[ProtoMember(4, Name = "challenge_reward_data")]
	public RewardData ChallengeRewardData { get; set; }

	[ProtoMember(5, Name = "talent_point_item")]
	public GenericItemNum TalentPointItem { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}
}
