using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__BeginTime;

	private uint? __pbn__Duration;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "duration")]
	public uint Duration
	{
		get
		{
			return __pbn__Duration.GetValueOrDefault();
		}
		set
		{
			__pbn__Duration = value;
		}
	}

	[ProtoMember(4, Name = "member_list")]
	public List<MpRecordMember> MemberLists { get; } = new List<MpRecordMember>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeDuration()
	{
		return __pbn__Duration.HasValue;
	}

	public void ResetDuration()
	{
		__pbn__Duration = null;
	}
}
