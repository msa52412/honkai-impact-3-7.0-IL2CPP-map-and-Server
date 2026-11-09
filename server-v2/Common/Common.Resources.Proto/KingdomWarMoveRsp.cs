using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarMoveRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3053
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
		[ProtoEnum(Name = "NOT_FINISH_CUR_POINT")]
		NotFinishCurPoint,
		[ProtoEnum(Name = "NO_TIMES")]
		NoTimes,
		[ProtoEnum(Name = "IN_MP_LOBBY")]
		InMpLobby,
		[ProtoEnum(Name = "IN_MP_MATCH")]
		InMpMatch
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurPointId;

	private uint? __pbn__TodayStep;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_point_id")]
	public uint CurPointId
	{
		get
		{
			return __pbn__CurPointId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurPointId = value;
		}
	}

	[ProtoMember(3, Name = "today_step")]
	public uint TodayStep
	{
		get
		{
			return __pbn__TodayStep.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayStep = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurPointId()
	{
		return __pbn__CurPointId.HasValue;
	}

	public void ResetCurPointId()
	{
		__pbn__CurPointId = null;
	}

	public bool ShouldSerializeTodayStep()
	{
		return __pbn__TodayStep.HasValue;
	}

	public void ResetTodayStep()
	{
		__pbn__TodayStep = null;
	}
}
