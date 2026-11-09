using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AcceptGratuityStageRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4103
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TIMES_LACK")]
		TimesLack,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private GratuityAcceptType? __pbn__AcceptType;

	private uint? __pbn__TodayLeftFreeStageNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_list")]
	public List<GratuityStage> StageLists { get; } = new List<GratuityStage>();


	[ProtoMember(3, Name = "accept_type")]
	[DefaultValue(GratuityAcceptType.GratuityAcceptNone)]
	public GratuityAcceptType AcceptType
	{
		get
		{
			return __pbn__AcceptType.GetValueOrDefault();
		}
		set
		{
			__pbn__AcceptType = value;
		}
	}

	[ProtoMember(4, Name = "today_left_free_stage_num")]
	public uint TodayLeftFreeStageNum
	{
		get
		{
			return __pbn__TodayLeftFreeStageNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TodayLeftFreeStageNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAcceptType()
	{
		return __pbn__AcceptType.HasValue;
	}

	public void ResetAcceptType()
	{
		__pbn__AcceptType = null;
	}

	public bool ShouldSerializeTodayLeftFreeStageNum()
	{
		return __pbn__TodayLeftFreeStageNum.HasValue;
	}

	public void ResetTodayLeftFreeStageNum()
	{
		__pbn__TodayLeftFreeStageNum = null;
	}
}
