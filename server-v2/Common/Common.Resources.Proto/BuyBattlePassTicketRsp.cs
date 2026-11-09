using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuyBattlePassTicketRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3753
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "TYPE_ERROR")]
		TypeError,
		[ProtoEnum(Name = "HAS_GOT")]
		HasGot,
		[ProtoEnum(Name = "LACK_MCOIN")]
		LackMcoin,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "advanced_reward_list")]
	public List<RewardData> AdvancedRewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "luxury_reward_list")]
	public List<RewardData> LuxuryRewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
