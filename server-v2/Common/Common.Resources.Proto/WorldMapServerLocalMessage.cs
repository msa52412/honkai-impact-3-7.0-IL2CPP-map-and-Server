using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WorldMapServerLocalMessage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LastReportTime;

	private uint? __pbn__LastSinEx;

	private uint? __pbn__LastEndless;

	[ProtoMember(1, Name = "last_report_time")]
	public uint LastReportTime
	{
		get
		{
			return __pbn__LastReportTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastReportTime = value;
		}
	}

	[ProtoMember(2, Name = "main_line_Story_list")]
	public int[] mainlineStorylists { get; set; }

	[ProtoMember(3, Name = "main_chapter_activity_section_list")]
	public int[] MainChapterActivitySectionLists { get; set; }

	[ProtoMember(4, Name = "last_sin_ex")]
	public uint LastSinEx
	{
		get
		{
			return __pbn__LastSinEx.GetValueOrDefault();
		}
		set
		{
			__pbn__LastSinEx = value;
		}
	}

	[ProtoMember(5, Name = "entered_open_world_list")]
	public int[] EnteredOpenWorldLists { get; set; }

	[ProtoMember(6, Name = "last_endless")]
	public uint LastEndless
	{
		get
		{
			return __pbn__LastEndless.GetValueOrDefault();
		}
		set
		{
			__pbn__LastEndless = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLastReportTime()
	{
		return __pbn__LastReportTime.HasValue;
	}

	public void ResetLastReportTime()
	{
		__pbn__LastReportTime = null;
	}

	public bool ShouldSerializeLastSinEx()
	{
		return __pbn__LastSinEx.HasValue;
	}

	public void ResetLastSinEx()
	{
		__pbn__LastSinEx = null;
	}

	public bool ShouldSerializeLastEndless()
	{
		return __pbn__LastEndless.HasValue;
	}

	public void ResetLastEndless()
	{
		__pbn__LastEndless = null;
	}
}
