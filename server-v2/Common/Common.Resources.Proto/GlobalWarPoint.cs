using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalWarPoint : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PointId;

	private uint? __pbn__CurHp;

	private uint? __pbn__PointStatus;

	private uint? __pbn__LastRefreshMemberNum;

	private uint? __pbn__PointFinishTime;

	private uint? __pbn__LastTakeSweepRewardTime;

	[ProtoMember(1, Name = "point_id")]
	public uint PointId
	{
		get
		{
			return __pbn__PointId.GetValueOrDefault();
		}
		set
		{
			__pbn__PointId = value;
		}
	}

	[ProtoMember(2, Name = "cur_hp")]
	public uint CurHp
	{
		get
		{
			return __pbn__CurHp.GetValueOrDefault();
		}
		set
		{
			__pbn__CurHp = value;
		}
	}

	[ProtoMember(3, Name = "point_status")]
	public uint PointStatus
	{
		get
		{
			return __pbn__PointStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__PointStatus = value;
		}
	}

	[ProtoMember(4, Name = "last_refresh_member_num")]
	public uint LastRefreshMemberNum
	{
		get
		{
			return __pbn__LastRefreshMemberNum.GetValueOrDefault();
		}
		set
		{
			__pbn__LastRefreshMemberNum = value;
		}
	}

	[ProtoMember(5, Name = "point_finish_time")]
	public uint PointFinishTime
	{
		get
		{
			return __pbn__PointFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__PointFinishTime = value;
		}
	}

	[ProtoMember(6, Name = "last_take_sweep_reward_time")]
	public uint LastTakeSweepRewardTime
	{
		get
		{
			return __pbn__LastTakeSweepRewardTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastTakeSweepRewardTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePointId()
	{
		return __pbn__PointId.HasValue;
	}

	public void ResetPointId()
	{
		__pbn__PointId = null;
	}

	public bool ShouldSerializeCurHp()
	{
		return __pbn__CurHp.HasValue;
	}

	public void ResetCurHp()
	{
		__pbn__CurHp = null;
	}

	public bool ShouldSerializePointStatus()
	{
		return __pbn__PointStatus.HasValue;
	}

	public void ResetPointStatus()
	{
		__pbn__PointStatus = null;
	}

	public bool ShouldSerializeLastRefreshMemberNum()
	{
		return __pbn__LastRefreshMemberNum.HasValue;
	}

	public void ResetLastRefreshMemberNum()
	{
		__pbn__LastRefreshMemberNum = null;
	}

	public bool ShouldSerializePointFinishTime()
	{
		return __pbn__PointFinishTime.HasValue;
	}

	public void ResetPointFinishTime()
	{
		__pbn__PointFinishTime = null;
	}

	public bool ShouldSerializeLastTakeSweepRewardTime()
	{
		return __pbn__LastTakeSweepRewardTime.HasValue;
	}

	public void ResetLastTakeSweepRewardTime()
	{
		__pbn__LastTakeSweepRewardTime = null;
	}
}
