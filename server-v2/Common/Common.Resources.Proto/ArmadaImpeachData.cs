using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ArmadaImpeachData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ImpeachStatus;

	private uint? __pbn__ElectionEndTime;

	[ProtoMember(1, Name = "impeach_status")]
	public uint ImpeachStatus
	{
		get
		{
			return __pbn__ImpeachStatus.GetValueOrDefault();
		}
		set
		{
			__pbn__ImpeachStatus = value;
		}
	}

	[ProtoMember(2, Name = "optional_leader_list")]
	public List<ArmadaOptionalLeader> OptionalLeaderLists { get; } = new List<ArmadaOptionalLeader>();


	[ProtoMember(3, Name = "election_end_time")]
	public uint ElectionEndTime
	{
		get
		{
			return __pbn__ElectionEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ElectionEndTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeImpeachStatus()
	{
		return __pbn__ImpeachStatus.HasValue;
	}

	public void ResetImpeachStatus()
	{
		__pbn__ImpeachStatus = null;
	}

	public bool ShouldSerializeElectionEndTime()
	{
		return __pbn__ElectionEndTime.HasValue;
	}

	public void ResetElectionEndTime()
	{
		__pbn__ElectionEndTime = null;
	}
}
