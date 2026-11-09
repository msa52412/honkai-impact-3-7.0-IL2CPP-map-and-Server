using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOfflineResourceDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4212
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_SCHEDULE")]
		NotInSchedule
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CurScheduleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "cur_schedule_id")]
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

	[ProtoMember(3, Name = "offline_resource_list")]
	public List<OfflineResource> OfflineResourceLists { get; } = new List<OfflineResource>();


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
