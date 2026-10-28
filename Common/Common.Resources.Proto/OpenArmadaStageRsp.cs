using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenArmadaStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2800
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
		[ProtoEnum(Name = "NOT_LEADER")]
		NotLeader,
		[ProtoEnum(Name = "TIME_LIMIT")]
		TimeLimit,
		[ProtoEnum(Name = "BOSS_NOT_FOUND")]
		BossNotFound,
		[ProtoEnum(Name = "LEVEL_LOCK")]
		LevelLock,
		[ProtoEnum(Name = "POPL_LACK")]
		PoplLack,
		[ProtoEnum(Name = "HAS_OPEN")]
		HasOpen,
		[ProtoEnum(Name = "NOT_FINISH")]
		NotFinish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__HardLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "hard_level")]
	public uint HardLevel
	{
		get
		{
			return __pbn__HardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HardLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHardLevel()
	{
		return __pbn__HardLevel.HasValue;
	}

	public void ResetHardLevel()
	{
		__pbn__HardLevel = null;
	}
}
