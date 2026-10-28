using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaStageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2670
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
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "BOSS_DEAD")]
		BossDead,
		[ProtoEnum(Name = "FIGHT_INTERVAL")]
		FightInterval,
		[ProtoEnum(Name = "NOT_BEGIN")]
		NotBegin,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "FIGHT_TIMES_LACK")]
		FightTimesLack,
		[ProtoEnum(Name = "BOSS_NO_DEAD")]
		BossNoDead,
		[ProtoEnum(Name = "HAS_FIGHT")]
		HasFight,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsFightBody;

	private bool? __pbn__IsMpMode;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_fight_body")]
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

	[ProtoMember(3, Name = "is_mp_mode")]
	public bool IsMpMode
	{
		get
		{
			return __pbn__IsMpMode.GetValueOrDefault();
		}
		set
		{
			__pbn__IsMpMode = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsFightBody()
	{
		return __pbn__IsFightBody.HasValue;
	}

	public void ResetIsFightBody()
	{
		__pbn__IsFightBody = null;
	}

	public bool ShouldSerializeIsMpMode()
	{
		return __pbn__IsMpMode.HasValue;
	}

	public void ResetIsMpMode()
	{
		__pbn__IsMpMode = null;
	}
}
