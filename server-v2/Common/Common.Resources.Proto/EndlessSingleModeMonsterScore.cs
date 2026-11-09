using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EndlessSingleModeMonsterScore : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MonsterGroupId;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "monster_group_id")]
	public uint MonsterGroupId
	{
		get
		{
			return __pbn__MonsterGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__MonsterGroupId = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMonsterGroupId()
	{
		return __pbn__MonsterGroupId.HasValue;
	}

	public void ResetMonsterGroupId()
	{
		__pbn__MonsterGroupId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
