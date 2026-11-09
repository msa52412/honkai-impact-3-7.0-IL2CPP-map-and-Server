using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BurdenAlleviationActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	[ProtoMember(1, Name = "cur_schedule_id")]
	public uint CurScheduleId
	{
		get
		{
			return __pbn__CurScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "alleviation_list")]
	public List<BurdenAlleviation> AlleviationLists { get; } = new List<BurdenAlleviation>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurScheduleId()
	{
		return __pbn__CurScheduleId.HasValue;
	}

	public void ResetCurScheduleId()
	{
		__pbn__CurScheduleId = null;
	}
}
