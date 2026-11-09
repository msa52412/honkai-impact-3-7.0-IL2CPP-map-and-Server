using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossIdInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__BossId;

	private uint? __pbn__Score;

	private uint? __pbn__BossTimesScore;

	[ProtoMember(1, Name = "boss_id")]
	public uint BossId
	{
		get
		{
			return __pbn__BossId.GetValueOrDefault();
		}
		set
		{
			__pbn__BossId = value;
		}
	}

	[ProtoMember(3, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(4, Name = "weapon_id_list")]
	public uint[] WeaponIdLists { get; set; }

	[ProtoMember(5, Name = "stigmata_id_list")]
	public uint[] StigmataIdLists { get; set; }

	[ProtoMember(6, Name = "score")]
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

	[ProtoMember(7, Name = "last_avatar_id_list")]
	public uint[] LastAvatarIdLists { get; set; }

	[ProtoMember(8, Name = "boss_times_score")]
	public uint BossTimesScore
	{
		get
		{
			return __pbn__BossTimesScore.GetValueOrDefault();
		}
		set
		{
			__pbn__BossTimesScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossId()
	{
		return __pbn__BossId.HasValue;
	}

	public void ResetBossId()
	{
		__pbn__BossId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeBossTimesScore()
	{
		return __pbn__BossTimesScore.HasValue;
	}

	public void ResetBossTimesScore()
	{
		__pbn__BossTimesScore = null;
	}
}
