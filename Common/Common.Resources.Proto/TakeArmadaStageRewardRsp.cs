using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeArmadaStageRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2676
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada,
		[ProtoEnum(Name = "TIME_INVALID")]
		TimeInvalid,
		[ProtoEnum(Name = "NO_BOSS")]
		NoBoss,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake,
		[ProtoEnum(Name = "NOT_FIGHT")]
		NotFight,
		[ProtoEnum(Name = "BOSS_NOT_FINISH")]
		BossNotFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__KillRewardMaterialId;

	private uint? __pbn__KillRewardMaterialNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_id_list")]
	public uint[] RewardIdLists { get; set; }

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "kill_reward_material_id")]
	public uint KillRewardMaterialId
	{
		get
		{
			return __pbn__KillRewardMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__KillRewardMaterialId = value;
		}
	}

	[ProtoMember(5, Name = "kill_reward_material_num")]
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

	public bool ShouldSerializeKillRewardMaterialId()
	{
		return __pbn__KillRewardMaterialId.HasValue;
	}

	public void ResetKillRewardMaterialId()
	{
		__pbn__KillRewardMaterialId = null;
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
