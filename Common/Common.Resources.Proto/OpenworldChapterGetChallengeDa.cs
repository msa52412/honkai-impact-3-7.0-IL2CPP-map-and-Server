using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterGetChallengeDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4593
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
[ProtoContract]
public class OpenworldChapterGetChallengeDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4594
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__ChallengeScheduleId;

	private uint? __pbn__ScheduleEndTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(3, Name = "challenge_schedule_id")]
	public uint ChallengeScheduleId
	{
		get
		{
			return __pbn__ChallengeScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeScheduleId = value;
		}
	}

	[ProtoMember(4, Name = "open_group_list")]
	public uint[] OpenGroupLists { get; set; }

	[ProtoMember(5, Name = "group_list")]
	public List<OpenworldChapterChallengeGroup> GroupLists { get; } = new List<OpenworldChapterChallengeGroup>();


	[ProtoMember(6, Name = "schedule_end_time")]
	public uint ScheduleEndTime
	{
		get
		{
			return __pbn__ScheduleEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeChallengeScheduleId()
	{
		return __pbn__ChallengeScheduleId.HasValue;
	}

	public void ResetChallengeScheduleId()
	{
		__pbn__ChallengeScheduleId = null;
	}

	public bool ShouldSerializeScheduleEndTime()
	{
		return __pbn__ScheduleEndTime.HasValue;
	}

	public void ResetScheduleEndTime()
	{
		__pbn__ScheduleEndTime = null;
	}
}
