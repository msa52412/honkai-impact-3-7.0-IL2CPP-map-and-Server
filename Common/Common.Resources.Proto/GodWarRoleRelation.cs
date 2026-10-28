using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarRoleRelation : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RoleId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__RewardHasTakeLevel;

	[ProtoMember(1, Name = "role_id")]
	public uint RoleId
	{
		get
		{
			return __pbn__RoleId.GetValueOrDefault();
		}
		set
		{
			__pbn__RoleId = value;
		}
	}

	[ProtoMember(2, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(3, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(4, Name = "reward_has_take_level")]
	public uint RewardHasTakeLevel
	{
		get
		{
			return __pbn__RewardHasTakeLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__RewardHasTakeLevel = value;
		}
	}

	[ProtoMember(5, Name = "reward_has_take_story_list")]
	public uint[] RewardHasTakeStoryLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRoleId()
	{
		return __pbn__RoleId.HasValue;
	}

	public void ResetRoleId()
	{
		__pbn__RoleId = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeRewardHasTakeLevel()
	{
		return __pbn__RewardHasTakeLevel.HasValue;
	}

	public void ResetRewardHasTakeLevel()
	{
		__pbn__RewardHasTakeLevel = null;
	}
}
