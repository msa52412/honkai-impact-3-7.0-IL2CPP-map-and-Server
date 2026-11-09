using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetWeekDayActivityDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 126
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

	private bool? __pbn__IsWholeData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_list")]
	public List<WeekDayActivity> ActivityLists { get; } = new List<WeekDayActivity>();


	[ProtoMember(3, Name = "is_whole_data")]
	public bool IsWholeData
	{
		get
		{
			return __pbn__IsWholeData.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWholeData = value;
		}
	}

	[ProtoMember(5, Name = "series_list")]
	public List<ActivitySeries> SeriesLists { get; } = new List<ActivitySeries>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsWholeData()
	{
		return __pbn__IsWholeData.HasValue;
	}

	public void ResetIsWholeData()
	{
		__pbn__IsWholeData = null;
	}
}
