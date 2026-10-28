using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MonopolyRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3860
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BehaviorId;

	[ProtoMember(1, Name = "behavior_id")]
	public uint BehaviorId
	{
		get
		{
			return __pbn__BehaviorId.GetValueOrDefault();
		}
		set
		{
			__pbn__BehaviorId = value;
		}
	}

	[ProtoMember(2, Name = "reward")]
	public RewardData Reward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBehaviorId()
	{
		return __pbn__BehaviorId.HasValue;
	}

	public void ResetBehaviorId()
	{
		__pbn__BehaviorId = null;
	}
}
