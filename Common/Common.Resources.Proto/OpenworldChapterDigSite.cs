using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterDigSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MainId;

	private uint? __pbn__ProgramId;

	private uint? __pbn__ProgramFinishTime;

	[ProtoMember(1, Name = "main_id")]
	public uint MainId
	{
		get
		{
			return __pbn__MainId.GetValueOrDefault();
		}
		set
		{
			__pbn__MainId = value;
		}
	}

	[ProtoMember(2, Name = "program_id")]
	public uint ProgramId
	{
		get
		{
			return __pbn__ProgramId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgramId = value;
		}
	}

	[ProtoMember(3, Name = "program_finish_time")]
	public uint ProgramFinishTime
	{
		get
		{
			return __pbn__ProgramFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgramFinishTime = value;
		}
	}

	[ProtoMember(4, Name = "program_times_list")]
	public List<OpenworldChapterDigSiteProgramTimes> ProgramTimesLists { get; } = new List<OpenworldChapterDigSiteProgramTimes>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMainId()
	{
		return __pbn__MainId.HasValue;
	}

	public void ResetMainId()
	{
		__pbn__MainId = null;
	}

	public bool ShouldSerializeProgramId()
	{
		return __pbn__ProgramId.HasValue;
	}

	public void ResetProgramId()
	{
		__pbn__ProgramId = null;
	}

	public bool ShouldSerializeProgramFinishTime()
	{
		return __pbn__ProgramFinishTime.HasValue;
	}

	public void ResetProgramFinishTime()
	{
		__pbn__ProgramFinishTime = null;
	}
}
