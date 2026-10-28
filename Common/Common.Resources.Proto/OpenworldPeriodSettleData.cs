using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldPeriodSettleData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__SettleLevel;

	private uint? __pbn__OldLevel;

	private uint? __pbn__OldStar;

	private uint? __pbn__CurLevel;

	private uint? __pbn__CurStar;

	private uint? __pbn__SubStar;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "settle_level")]
	public uint SettleLevel
	{
		get
		{
			return __pbn__SettleLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__SettleLevel = value;
		}
	}

	[ProtoMember(3, Name = "old_level")]
	public uint OldLevel
	{
		get
		{
			return __pbn__OldLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__OldLevel = value;
		}
	}

	[ProtoMember(4, Name = "old_star")]
	public uint OldStar
	{
		get
		{
			return __pbn__OldStar.GetValueOrDefault();
		}
		set
		{
			__pbn__OldStar = value;
		}
	}

	[ProtoMember(5, Name = "cur_level")]
	public uint CurLevel
	{
		get
		{
			return __pbn__CurLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__CurLevel = value;
		}
	}

	[ProtoMember(6, Name = "cur_star")]
	public uint CurStar
	{
		get
		{
			return __pbn__CurStar.GetValueOrDefault();
		}
		set
		{
			__pbn__CurStar = value;
		}
	}

	[ProtoMember(7, Name = "sub_star")]
	public uint SubStar
	{
		get
		{
			return __pbn__SubStar.GetValueOrDefault();
		}
		set
		{
			__pbn__SubStar = value;
		}
	}

	[ProtoMember(8, Name = "normal_reward")]
	public RewardData NormalReward { get; set; }

	[ProtoMember(9, Name = "up_reward")]
	public RewardData UpReward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeSettleLevel()
	{
		return __pbn__SettleLevel.HasValue;
	}

	public void ResetSettleLevel()
	{
		__pbn__SettleLevel = null;
	}

	public bool ShouldSerializeOldLevel()
	{
		return __pbn__OldLevel.HasValue;
	}

	public void ResetOldLevel()
	{
		__pbn__OldLevel = null;
	}

	public bool ShouldSerializeOldStar()
	{
		return __pbn__OldStar.HasValue;
	}

	public void ResetOldStar()
	{
		__pbn__OldStar = null;
	}

	public bool ShouldSerializeCurLevel()
	{
		return __pbn__CurLevel.HasValue;
	}

	public void ResetCurLevel()
	{
		__pbn__CurLevel = null;
	}

	public bool ShouldSerializeCurStar()
	{
		return __pbn__CurStar.HasValue;
	}

	public void ResetCurStar()
	{
		__pbn__CurStar = null;
	}

	public bool ShouldSerializeSubStar()
	{
		return __pbn__SubStar.HasValue;
	}

	public void ResetSubStar()
	{
		__pbn__SubStar = null;
	}
}
