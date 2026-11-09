using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SanctuaryTakeLevelRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4308
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HasTakeRewardLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "has_take_reward_level")]
	public uint HasTakeRewardLevel
	{
		get
		{
			return __pbn__HasTakeRewardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardLevel = value;
		}
	}

	[ProtoMember(3, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHasTakeRewardLevel()
	{
		return __pbn__HasTakeRewardLevel.HasValue;
	}

	public void ResetHasTakeRewardLevel()
	{
		__pbn__HasTakeRewardLevel = null;
	}
}
