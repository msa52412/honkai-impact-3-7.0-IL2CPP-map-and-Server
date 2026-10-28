using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TeamLeaderChangeNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 371
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private uint? __pbn__OldLeader;

	private uint? __pbn__NewLeader;

	private bool? __pbn__IsTimeout;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "old_leader")]
	public uint OldLeader
	{
		get
		{
			return __pbn__OldLeader.GetValueOrDefault();
		}
		set
		{
			__pbn__OldLeader = value;
		}
	}

	[ProtoMember(3, Name = "new_leader")]
	public uint NewLeader
	{
		get
		{
			return __pbn__NewLeader.GetValueOrDefault();
		}
		set
		{
			__pbn__NewLeader = value;
		}
	}

	[ProtoMember(4, Name = "is_timeout")]
	public bool IsTimeout
	{
		get
		{
			return __pbn__IsTimeout.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTimeout = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeOldLeader()
	{
		return __pbn__OldLeader.HasValue;
	}

	public void ResetOldLeader()
	{
		__pbn__OldLeader = null;
	}

	public bool ShouldSerializeNewLeader()
	{
		return __pbn__NewLeader.HasValue;
	}

	public void ResetNewLeader()
	{
		__pbn__NewLeader = null;
	}

	public bool ShouldSerializeIsTimeout()
	{
		return __pbn__IsTimeout.HasValue;
	}

	public void ResetIsTimeout()
	{
		__pbn__IsTimeout = null;
	}
}
