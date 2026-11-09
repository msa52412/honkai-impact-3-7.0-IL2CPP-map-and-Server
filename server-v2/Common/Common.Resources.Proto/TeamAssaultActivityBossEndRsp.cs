using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamAssaultActivityBossEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4027
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
		[ProtoEnum(Name = "ORDER_ERROR")]
		OrderError,
		[ProtoEnum(Name = "CHEAT_ERROR")]
		CheatError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BossIdx;

	private uint? __pbn__Score;

	private uint? __pbn__ActivityScore;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "boss_idx")]
	public uint BossIdx
	{
		get
		{
			return __pbn__BossIdx.GetValueOrDefault();
		}
		set
		{
			__pbn__BossIdx = value;
		}
	}

	[ProtoMember(3, Name = "score")]
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

	[ProtoMember(4, Name = "activity_score")]
	public uint ActivityScore
	{
		get
		{
			return __pbn__ActivityScore.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBossIdx()
	{
		return __pbn__BossIdx.HasValue;
	}

	public void ResetBossIdx()
	{
		__pbn__BossIdx = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeActivityScore()
	{
		return __pbn__ActivityScore.HasValue;
	}

	public void ResetActivityScore()
	{
		__pbn__ActivityScore = null;
	}
}
