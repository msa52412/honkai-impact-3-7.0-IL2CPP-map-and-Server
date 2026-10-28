using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaStageFightInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private uint? __pbn__TotalDamage;

	private bool? __pbn__IsFightBody;

	private uint? __pbn__KillRewardMaterialNum;

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

	[ProtoMember(2, Name = "total_damage")]
	public uint TotalDamage
	{
		get
		{
			return __pbn__TotalDamage.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDamage = value;
		}
	}

	[ProtoMember(3, Name = "is_fight_body")]
	public bool IsFightBody
	{
		get
		{
			return __pbn__IsFightBody.GetValueOrDefault();
		}
		set
		{
			__pbn__IsFightBody = value;
		}
	}

	[ProtoMember(4, Name = "kill_reward_material_num")]
	public uint KillRewardMaterialNum
	{
		get
		{
			return __pbn__KillRewardMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__KillRewardMaterialNum = value;
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

	public bool ShouldSerializeTotalDamage()
	{
		return __pbn__TotalDamage.HasValue;
	}

	public void ResetTotalDamage()
	{
		__pbn__TotalDamage = null;
	}

	public bool ShouldSerializeIsFightBody()
	{
		return __pbn__IsFightBody.HasValue;
	}

	public void ResetIsFightBody()
	{
		__pbn__IsFightBody = null;
	}

	public bool ShouldSerializeKillRewardMaterialNum()
	{
		return __pbn__KillRewardMaterialNum.HasValue;
	}

	public void ResetKillRewardMaterialNum()
	{
		__pbn__KillRewardMaterialNum = null;
	}
}
