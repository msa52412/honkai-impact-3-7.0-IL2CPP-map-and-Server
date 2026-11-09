using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessLastSettleRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5205
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "settle_info")]
	public UltraEndlessSettleInfo SettleInfo { get; set; }

	[ProtoMember(2, Name = "settle_reward_data")]
	public RewardData SettleRewardData { get; set; }

	[ProtoMember(3, Name = "group_level_reward_data")]
	public RewardData GroupLevelRewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
