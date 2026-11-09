using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWeekDayActivityCompensationInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1666
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class GetWeekDayActivityCompensationInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1667
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__HasTakenCompensation;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_compensation_list")]
	public List<WeekDayActivityStageCompensation> StageCompensationLists { get; } = new List<WeekDayActivityStageCompensation>();


	[ProtoMember(3, Name = "has_taken_compensation")]
	public bool HasTakenCompensation
	{
		get
		{
			return __pbn__HasTakenCompensation.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakenCompensation = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHasTakenCompensation()
	{
		return __pbn__HasTakenCompensation.HasValue;
	}

	public void ResetHasTakenCompensation()
	{
		__pbn__HasTakenCompensation = null;
	}
}
