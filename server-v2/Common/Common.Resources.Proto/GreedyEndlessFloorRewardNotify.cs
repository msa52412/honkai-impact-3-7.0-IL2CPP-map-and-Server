using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessFloorRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3457
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaxFinishFloor;

	[ProtoMember(1, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	[ProtoMember(2, Name = "max_finish_floor")]
	public uint MaxFinishFloor
	{
		get
		{
			return __pbn__MaxFinishFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxFinishFloor = value;
		}
	}

	[ProtoMember(6, Name = "finish_challenge_id_list")]
	public uint[] FinishChallengeIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaxFinishFloor()
	{
		return __pbn__MaxFinishFloor.HasValue;
	}

	public void ResetMaxFinishFloor()
	{
		__pbn__MaxFinishFloor = null;
	}
}
