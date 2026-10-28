using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BossChallengeStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__UnlockTime;

	private uint? __pbn__MaxRewardIdx;

	private uint? __pbn__CurFloor;

	private uint? __pbn__CurStageCostTime;

	private uint? __pbn__MinStageCostTime;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "unlock_time")]
	public uint UnlockTime
	{
		get
		{
			return __pbn__UnlockTime.GetValueOrDefault();
		}
		set
		{
			__pbn__UnlockTime = value;
		}
	}

	[ProtoMember(3, Name = "max_reward_idx")]
	public uint MaxRewardIdx
	{
		get
		{
			return __pbn__MaxRewardIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRewardIdx = value;
		}
	}

	[ProtoMember(4, Name = "cur_floor")]
	public uint CurFloor
	{
		get
		{
			return __pbn__CurFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__CurFloor = value;
		}
	}

	[ProtoMember(5, Name = "cur_stage_cost_time")]
	public uint CurStageCostTime
	{
		get
		{
			return __pbn__CurStageCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CurStageCostTime = value;
		}
	}

	[ProtoMember(6, Name = "min_stage_cost_time")]
	public uint MinStageCostTime
	{
		get
		{
			return __pbn__MinStageCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MinStageCostTime = value;
		}
	}

	[ProtoMember(7, Name = "avatar_list")]
	public List<BossChallengeAvatar> AvatarLists { get; } = new List<BossChallengeAvatar>();


	[ProtoMember(8, Name = "sc_dlc_status")]
	public BossChallengeScDLCStatus ScDlcStatus { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeUnlockTime()
	{
		return __pbn__UnlockTime.HasValue;
	}

	public void ResetUnlockTime()
	{
		__pbn__UnlockTime = null;
	}

	public bool ShouldSerializeMaxRewardIdx()
	{
		return __pbn__MaxRewardIdx.HasValue;
	}

	public void ResetMaxRewardIdx()
	{
		__pbn__MaxRewardIdx = null;
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
	}

	public bool ShouldSerializeCurStageCostTime()
	{
		return __pbn__CurStageCostTime.HasValue;
	}

	public void ResetCurStageCostTime()
	{
		__pbn__CurStageCostTime = null;
	}

	public bool ShouldSerializeMinStageCostTime()
	{
		return __pbn__MinStageCostTime.HasValue;
	}

	public void ResetMinStageCostTime()
	{
		__pbn__MinStageCostTime = null;
	}
}
