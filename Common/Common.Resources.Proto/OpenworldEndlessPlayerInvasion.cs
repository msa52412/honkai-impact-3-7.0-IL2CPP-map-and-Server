using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldEndlessPlayerInvasionData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__InvaderId;

	private uint? __pbn__ActiveTime;

	private uint? __pbn__SubScore;

	private uint? __pbn__BattleId;

	private uint? __pbn__InvasionId;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "invader_id")]
	public uint InvaderId
	{
		get
		{
			return __pbn__InvaderId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvaderId = value;
		}
	}

	[ProtoMember(3, Name = "active_time")]
	public uint ActiveTime
	{
		get
		{
			return __pbn__ActiveTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveTime = value;
		}
	}

	[ProtoMember(4, Name = "buff_list")]
	public uint[] BuffLists { get; set; }

	[ProtoMember(5, Name = "sub_score")]
	public uint SubScore
	{
		get
		{
			return __pbn__SubScore.GetValueOrDefault();
		}
		set
		{
			__pbn__SubScore = value;
		}
	}

	[ProtoMember(6, Name = "battle_id")]
	public uint BattleId
	{
		get
		{
			return __pbn__BattleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BattleId = value;
		}
	}

	[ProtoMember(7, Name = "invasion_id")]
	public uint InvasionId
	{
		get
		{
			return __pbn__InvasionId.GetValueOrDefault();
		}
		set
		{
			__pbn__InvasionId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeInvaderId()
	{
		return __pbn__InvaderId.HasValue;
	}

	public void ResetInvaderId()
	{
		__pbn__InvaderId = null;
	}

	public bool ShouldSerializeActiveTime()
	{
		return __pbn__ActiveTime.HasValue;
	}

	public void ResetActiveTime()
	{
		__pbn__ActiveTime = null;
	}

	public bool ShouldSerializeSubScore()
	{
		return __pbn__SubScore.HasValue;
	}

	public void ResetSubScore()
	{
		__pbn__SubScore = null;
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeInvasionId()
	{
		return __pbn__InvasionId.HasValue;
	}

	public void ResetInvasionId()
	{
		__pbn__InvasionId = null;
	}
}
