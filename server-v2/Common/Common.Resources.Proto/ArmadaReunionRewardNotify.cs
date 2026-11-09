using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaReunionRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2836
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "mission")]
	public ArmadaReunionMission Mission { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
