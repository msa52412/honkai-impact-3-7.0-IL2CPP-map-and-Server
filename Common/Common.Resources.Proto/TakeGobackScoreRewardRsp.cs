using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGobackScoreRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1449
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SCORE_LACK")]
		ScoreLack,
		[ProtoEnum(Name = "NOT_GOBACK")]
		NotGoback,
		[ProtoEnum(Name = "HAS_TAKE")]
		HasTake
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoreId;

	private uint? __pbn__Score;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "score_id")]
	public uint ScoreId
	{
		get
		{
			return __pbn__ScoreId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreId = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "score")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoreId()
	{
		return __pbn__ScoreId.HasValue;
	}

	public void ResetScoreId()
	{
		__pbn__ScoreId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
