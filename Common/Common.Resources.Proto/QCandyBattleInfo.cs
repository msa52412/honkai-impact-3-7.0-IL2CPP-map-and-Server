using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BattleId;

	private uint? __pbn__RemainLifeTimes;

	private uint? __pbn__Score;

	private uint? __pbn__Uid;

	[ProtoMember(1, Name = "battle_id")]
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

	[ProtoMember(2, Name = "remain_life_times")]
	public uint RemainLifeTimes
	{
		get
		{
			return __pbn__RemainLifeTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RemainLifeTimes = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(4, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBattleId()
	{
		return __pbn__BattleId.HasValue;
	}

	public void ResetBattleId()
	{
		__pbn__BattleId = null;
	}

	public bool ShouldSerializeRemainLifeTimes()
	{
		return __pbn__RemainLifeTimes.HasValue;
	}

	public void ResetRemainLifeTimes()
	{
		__pbn__RemainLifeTimes = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}
}
