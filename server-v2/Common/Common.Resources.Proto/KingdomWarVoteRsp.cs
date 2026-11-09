using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarVoteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3055
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "PHASE_ERROR")]
		PhaseError,
		[ProtoEnum(Name = "TIME_ERROR")]
		TimeError,
		[ProtoEnum(Name = "POINT_ERROR")]
		PointError,
		[ProtoEnum(Name = "HAS_VOTE")]
		HasVote,
		[ProtoEnum(Name = "SCORE_LACK")]
		ScoreLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetPoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_point")]
	public uint TargetPoint
	{
		get
		{
			return __pbn__TargetPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetPoint()
	{
		return __pbn__TargetPoint.HasValue;
	}

	public void ResetTargetPoint()
	{
		__pbn__TargetPoint = null;
	}
}
