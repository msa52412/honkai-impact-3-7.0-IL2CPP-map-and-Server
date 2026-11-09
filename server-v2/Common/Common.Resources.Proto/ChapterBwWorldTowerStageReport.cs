using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChapterBwWorldTowerStageReportFloorReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7663
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(2, Name = "stage_id")]
	public uint StageId { get; set; }

	[ProtoMember(3, Name = "floor")]
	public uint Floor { get; set; }

	[ProtoMember(4, Name = "score")]
	public uint Score { get; set; }

	[ProtoMember(5, Name = "cost_time")]
	public uint CostTime { get; set; }

	[ProtoMember(6, Name = "end_status")]
	public StageEndStatus EndStatus { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class ChapterBwWorldTowerStageReportFloorRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7664
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "SCORE_ERROR")]
		ScoreError,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "FLOOR_ERROR")]
		FloorError,
		[ProtoEnum(Name = "STAGE_ERROR")]
		StageError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "chapter_id")]
	public uint ChapterId { get; set; }

	[ProtoMember(3, Name = "stage_id")]
	public uint StageId { get; set; }

	[ProtoMember(4, Name = "floor")]
	public uint Floor { get; set; }

	[ProtoMember(5, Name = "score")]
	public uint Score { get; set; }

	[ProtoMember(6, Name = "cost_time")]
	public uint CostTime { get; set; }

	[ProtoMember(7, Name = "end_status")]
	public StageEndStatus EndStatus { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
