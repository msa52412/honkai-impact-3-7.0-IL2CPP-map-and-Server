using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessFirstJoinRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5220
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "medal_reward")]
	public RewardData MedalReward { get; set; }

	[ProtoMember(2, Name = "promote_reward")]
	public RewardData PromoteReward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
