using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BbqActivityTakeLevelUpRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4255
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MaxRewardTakenLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data_list")]
	public List<RewardData> RewardDataLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "max_reward_taken_level")]
	public uint MaxRewardTakenLevel
	{
		get
		{
			return __pbn__MaxRewardTakenLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxRewardTakenLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMaxRewardTakenLevel()
	{
		return __pbn__MaxRewardTakenLevel.HasValue;
	}

	public void ResetMaxRewardTakenLevel()
	{
		__pbn__MaxRewardTakenLevel = null;
	}
}
