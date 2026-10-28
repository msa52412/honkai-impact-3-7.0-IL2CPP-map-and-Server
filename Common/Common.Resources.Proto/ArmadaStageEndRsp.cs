using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaStageEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2672
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
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError,
		[ProtoEnum(Name = "STAGE_FAIL")]
		StageFail,
		[ProtoEnum(Name = "FIGHT_TIMES_LACK")]
		FightTimesLack,
		[ProtoEnum(Name = "BOSS_NO_DEAD")]
		BossNoDead,
		[ProtoEnum(Name = "HAS_FIGHT")]
		HasFight
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AddScoin;

	private StageEndStatus? __pbn__EndStatus;

	private bool? __pbn__IsFightBody;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "add_scoin")]
	public uint AddScoin
	{
		get
		{
			return __pbn__AddScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__AddScoin = value;
		}
	}

	[ProtoMember(3, Name = "end_status")]
	[DefaultValue(StageEndStatus.StageWin)]
	public StageEndStatus EndStatus
	{
		get
		{
			return __pbn__EndStatus ?? StageEndStatus.StageWin;
		}
		set
		{
			__pbn__EndStatus = value;
		}
	}

	[ProtoMember(5, Name = "is_fight_body")]
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

	[ProtoMember(6, Name = "body")]
	public StageEndReqBody Body { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAddScoin()
	{
		return __pbn__AddScoin.HasValue;
	}

	public void ResetAddScoin()
	{
		__pbn__AddScoin = null;
	}

	public bool ShouldSerializeEndStatus()
	{
		return __pbn__EndStatus.HasValue;
	}

	public void ResetEndStatus()
	{
		__pbn__EndStatus = null;
	}

	public bool ShouldSerializeIsFightBody()
	{
		return __pbn__IsFightBody.HasValue;
	}

	public void ResetIsFightBody()
	{
		__pbn__IsFightBody = null;
	}
}
