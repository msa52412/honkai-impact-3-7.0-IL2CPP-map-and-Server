using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldBossHuntRatingRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4528
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__RatingId;

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

	[ProtoMember(2, Name = "rating_id")]
	public uint RatingId
	{
		get
		{
			return __pbn__RatingId.GetValueOrDefault();
		}
		set
		{
			__pbn__RatingId = value;
		}
	}

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

	public bool ShouldSerializeRatingId()
	{
		return __pbn__RatingId.HasValue;
	}

	public void ResetRatingId()
	{
		__pbn__RatingId = null;
	}
}
[ProtoContract]
public class TakeOpenworldBossHuntRatingRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4529
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_KILL_ALL")]
		NotKillAll,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward,
		[ProtoEnum(Name = "NO_RATING")]
		NoRating,
		[ProtoEnum(Name = "RATING_DISMATCH")]
		RatingDismatch,
		[ProtoEnum(Name = "HAVE_TAKE")]
		HaveTake
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__RatingId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
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

	[ProtoMember(3, Name = "rating_id")]
	public uint RatingId
	{
		get
		{
			return __pbn__RatingId.GetValueOrDefault();
		}
		set
		{
			__pbn__RatingId = value;
		}
	}

	[ProtoMember(4, Name = "reward_data_list")]
	public List<RewardData> RewardDataLists { get; } = new List<RewardData>();


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

	public bool ShouldSerializeRatingId()
	{
		return __pbn__RatingId.HasValue;
	}

	public void ResetRatingId()
	{
		__pbn__RatingId = null;
	}
}
