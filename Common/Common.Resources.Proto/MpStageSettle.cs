using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageSettle : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__Score;

	private uint? __pbn__Damage;

	private uint? __pbn__MvpScore;

	[ProtoMember(1, Name = "uid")]
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

	[ProtoMember(2, Name = "score")]
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

	[ProtoMember(3, Name = "damage")]
	public uint Damage
	{
		get
		{
			return __pbn__Damage.GetValueOrDefault();
		}
		set
		{
			__pbn__Damage = value;
		}
	}

	[ProtoMember(4, Name = "mvp_score")]
	public uint MvpScore
	{
		get
		{
			return __pbn__MvpScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MvpScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeDamage()
	{
		return __pbn__Damage.HasValue;
	}

	public void ResetDamage()
	{
		__pbn__Damage = null;
	}

	public bool ShouldSerializeMvpScore()
	{
		return __pbn__MvpScore.HasValue;
	}

	public void ResetMvpScore()
	{
		__pbn__MvpScore = null;
	}
}
