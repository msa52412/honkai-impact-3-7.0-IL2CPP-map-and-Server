using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldBeast : IExtensible
{
	[ProtoContract]
	public enum Status
	{
		[ProtoEnum(Name = "CHALLENGE")]
		Challenge = 1,
		[ProtoEnum(Name = "TREASURE")]
		Treasure
	}

	private IExtension __pbn__extensionData;

	private ulong? __pbn__BeastUniqueIdU64;

	private uint? __pbn__BeastId;

	private uint? __pbn__StageId;

	private uint? __pbn__LeftHp;

	private Status? __pbn__status;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__ChallengerNum;

	private uint? __pbn__TreasureRankId;

	[ProtoMember(1, Name = "beast_unique_id_u64")]
	public ulong BeastUniqueIdU64
	{
		get
		{
			return __pbn__BeastUniqueIdU64.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastUniqueIdU64 = value;
		}
	}

	[ProtoMember(2, Name = "beast_id")]
	public uint BeastId
	{
		get
		{
			return __pbn__BeastId.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastId = value;
		}
	}

	[ProtoMember(4, Name = "stage_id")]
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

	[ProtoMember(5, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(6)]
	[DefaultValue(Status.Challenge)]
	public Status status
	{
		get
		{
			return __pbn__status ?? Status.Challenge;
		}
		set
		{
			__pbn__status = value;
		}
	}

	[ProtoMember(7, Name = "wait_take_treasure_uid_list")]
	public uint[] WaitTakeTreasureUidLists { get; set; }

	[ProtoMember(8, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(9, Name = "challenger_num")]
	public uint ChallengerNum
	{
		get
		{
			return __pbn__ChallengerNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengerNum = value;
		}
	}

	[ProtoMember(10, Name = "treasure_rank_id")]
	public uint TreasureRankId
	{
		get
		{
			return __pbn__TreasureRankId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureRankId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeastUniqueIdU64()
	{
		return __pbn__BeastUniqueIdU64.HasValue;
	}

	public void ResetBeastUniqueIdU64()
	{
		__pbn__BeastUniqueIdU64 = null;
	}

	public bool ShouldSerializeBeastId()
	{
		return __pbn__BeastId.HasValue;
	}

	public void ResetBeastId()
	{
		__pbn__BeastId = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializestatus()
	{
		return __pbn__status.HasValue;
	}

	public void Resetstatus()
	{
		__pbn__status = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeChallengerNum()
	{
		return __pbn__ChallengerNum.HasValue;
	}

	public void ResetChallengerNum()
	{
		__pbn__ChallengerNum = null;
	}

	public bool ShouldSerializeTreasureRankId()
	{
		return __pbn__TreasureRankId.HasValue;
	}

	public void ResetTreasureRankId()
	{
		__pbn__TreasureRankId = null;
	}
}
