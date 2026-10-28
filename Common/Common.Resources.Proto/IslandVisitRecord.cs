using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class IslandVisitRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private IslandVisitRecordType? __pbn__RecordType;

	private uint? __pbn__VisitTime;

	private uint? __pbn__VisitUid;

	private uint? __pbn__VisitEventId;

	[ProtoMember(1, Name = "record_type")]
	[DefaultValue(IslandVisitRecordType.IslandRecordHelpFriend)]
	public IslandVisitRecordType RecordType
	{
		get
		{
			return __pbn__RecordType ?? IslandVisitRecordType.IslandRecordHelpFriend;
		}
		set
		{
			__pbn__RecordType = value;
		}
	}

	[ProtoMember(2, Name = "visit_time")]
	public uint VisitTime
	{
		get
		{
			return __pbn__VisitTime.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitTime = value;
		}
	}

	[ProtoMember(3, Name = "visit_uid")]
	public uint VisitUid
	{
		get
		{
			return __pbn__VisitUid.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitUid = value;
		}
	}

	[ProtoMember(4, Name = "visit_event_id")]
	public uint VisitEventId
	{
		get
		{
			return __pbn__VisitEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__VisitEventId = value;
		}
	}

	[ProtoMember(5, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRecordType()
	{
		return __pbn__RecordType.HasValue;
	}

	public void ResetRecordType()
	{
		__pbn__RecordType = null;
	}

	public bool ShouldSerializeVisitTime()
	{
		return __pbn__VisitTime.HasValue;
	}

	public void ResetVisitTime()
	{
		__pbn__VisitTime = null;
	}

	public bool ShouldSerializeVisitUid()
	{
		return __pbn__VisitUid.HasValue;
	}

	public void ResetVisitUid()
	{
		__pbn__VisitUid = null;
	}

	public bool ShouldSerializeVisitEventId()
	{
		return __pbn__VisitEventId.HasValue;
	}

	public void ResetVisitEventId()
	{
		__pbn__VisitEventId = null;
	}
}
