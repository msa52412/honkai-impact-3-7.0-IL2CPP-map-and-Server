using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ExBossStageBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 530
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ = 0,
		[ProtoEnum(Name = "FAIL")]
		Fail = 1,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError = 3,
		[ProtoEnum(Name = "DUP_AVATAR")]
		DupAvatar = 4,
		[ProtoEnum(Name = "ENTER_TIMES_LACK")]
		EnterTimesLack = 5,
		[ProtoEnum(Name = "EX_BOSS_LACK_SCORE")]
		ExBossLackScore = 6,
		[ProtoEnum(Name = "ELF_ERROR")]
		ElfError = 7,
		[ProtoEnum(Name = "ELF_CARRY_LEVEL_LIMIT")]
		ElfCarryLevelLimit = 8,
		[ProtoEnum(Name = "NEED_FINISH_NORMAL")]
		NeedFinishNormal = 9,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit = 10,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed = 11,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen = 12,
		[ProtoEnum(Name = "NOT_ALLOW_TURBO")]
		NotAllowTurbo = 13,
		[ProtoEnum(Name = "NO_AVAILABLE_BOSS")]
		NoAvailableBoss = 14,
		[ProtoEnum(Name = "LACK_LOCK_SLOT")]
		LackLockSlot = 15,
		[ProtoEnum(Name = "PRE_BOSS_NOT_FINISH")]
		PreBossNotFinish = 16
	}

	private IExtension __pbn__extensionData;

	private string __pbn__StageTransactionStr;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_transaction_str")]
	[DefaultValue("")]
	public string StageTransactionStr
	{
		get
		{
			return __pbn__StageTransactionStr ?? "";
		}
		set
		{
			__pbn__StageTransactionStr = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageTransactionStr()
	{
		return __pbn__StageTransactionStr != null;
	}

	public void ResetStageTransactionStr()
	{
		__pbn__StageTransactionStr = null;
	}
}
