using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetStageDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 42
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STAGE_NOT_EXIST")]
		StageNotExist
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsAll;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stage_list")]
	public List<Stage> StageLists { get; } = new List<Stage>();


	[ProtoMember(3, Name = "finished_chapter_list")]
	public uint[] FinishedChapterLists { get; set; }

	[ProtoMember(4, Name = "event_data_list")]
	public List<StageEventData> EventDataLists { get; } = new List<StageEventData>();


	[ProtoMember(5, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}
}
