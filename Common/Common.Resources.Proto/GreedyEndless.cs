using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndless : IExtensible
{
	private IExtension __pbn__extensionData;

	private bool? __pbn__IsNeedEnroll;

	private uint? __pbn__FloorConfigId;

	private uint? __pbn__RewardConfigId;

	private uint? __pbn__CurFloor;

	private uint? __pbn__MaxFinishFloor;

	[ProtoMember(1, Name = "is_need_enroll")]
	public bool IsNeedEnroll
	{
		get
		{
			return __pbn__IsNeedEnroll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNeedEnroll = value;
		}
	}

	[ProtoMember(2, Name = "floor_config_id")]
	public uint FloorConfigId
	{
		get
		{
			return __pbn__FloorConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__FloorConfigId = value;
		}
	}

	[ProtoMember(3, Name = "reward_config_id")]
	public uint RewardConfigId
	{
		get
		{
			return __pbn__RewardConfigId.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardConfigId = value;
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

	[ProtoMember(5, Name = "max_finish_floor")]
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

	public bool ShouldSerializeIsNeedEnroll()
	{
		return __pbn__IsNeedEnroll.HasValue;
	}

	public void ResetIsNeedEnroll()
	{
		__pbn__IsNeedEnroll = null;
	}

	public bool ShouldSerializeFloorConfigId()
	{
		return __pbn__FloorConfigId.HasValue;
	}

	public void ResetFloorConfigId()
	{
		__pbn__FloorConfigId = null;
	}

	public bool ShouldSerializeRewardConfigId()
	{
		return __pbn__RewardConfigId.HasValue;
	}

	public void ResetRewardConfigId()
	{
		__pbn__RewardConfigId = null;
	}

	public bool ShouldSerializeCurFloor()
	{
		return __pbn__CurFloor.HasValue;
	}

	public void ResetCurFloor()
	{
		__pbn__CurFloor = null;
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
