using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaDiscoveryRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2664
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_ARMADA")]
		NotInArmada
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ArmadaWeekDiscovery;

	private uint? __pbn__SelfWeekDiscovery;

	private uint? __pbn__NextRefreshTime;

	private uint? __pbn__CurTime;

	private uint? __pbn__JoinTime;

	private uint? __pbn__JoinArmadaWeekDiscovery;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "armada_week_discovery")]
	public uint ArmadaWeekDiscovery
	{
		get
		{
			return __pbn__ArmadaWeekDiscovery.GetValueOrDefault();
		}
		set
		{
			__pbn__ArmadaWeekDiscovery = value;
		}
	}

	[ProtoMember(3, Name = "self_week_discovery")]
	public uint SelfWeekDiscovery
	{
		get
		{
			return __pbn__SelfWeekDiscovery.GetValueOrDefault();
		}
		set
		{
			__pbn__SelfWeekDiscovery = value;
		}
	}

	[ProtoMember(4, Name = "next_refresh_time")]
	public uint NextRefreshTime
	{
		get
		{
			return __pbn__NextRefreshTime.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshTime = value;
		}
	}

	[ProtoMember(5, Name = "cur_time")]
	public uint CurTime
	{
		get
		{
			return __pbn__CurTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CurTime = value;
		}
	}

	[ProtoMember(6, Name = "join_time")]
	public uint JoinTime
	{
		get
		{
			return __pbn__JoinTime.GetValueOrDefault();
		}
		set
		{
			__pbn__JoinTime = value;
		}
	}

	[ProtoMember(7, Name = "join_armada_week_discovery")]
	public uint JoinArmadaWeekDiscovery
	{
		get
		{
			return __pbn__JoinArmadaWeekDiscovery.GetValueOrDefault();
		}
		set
		{
			__pbn__JoinArmadaWeekDiscovery = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeArmadaWeekDiscovery()
	{
		return __pbn__ArmadaWeekDiscovery.HasValue;
	}

	public void ResetArmadaWeekDiscovery()
	{
		__pbn__ArmadaWeekDiscovery = null;
	}

	public bool ShouldSerializeSelfWeekDiscovery()
	{
		return __pbn__SelfWeekDiscovery.HasValue;
	}

	public void ResetSelfWeekDiscovery()
	{
		__pbn__SelfWeekDiscovery = null;
	}

	public bool ShouldSerializeNextRefreshTime()
	{
		return __pbn__NextRefreshTime.HasValue;
	}

	public void ResetNextRefreshTime()
	{
		__pbn__NextRefreshTime = null;
	}

	public bool ShouldSerializeCurTime()
	{
		return __pbn__CurTime.HasValue;
	}

	public void ResetCurTime()
	{
		__pbn__CurTime = null;
	}

	public bool ShouldSerializeJoinTime()
	{
		return __pbn__JoinTime.HasValue;
	}

	public void ResetJoinTime()
	{
		__pbn__JoinTime = null;
	}

	public bool ShouldSerializeJoinArmadaWeekDiscovery()
	{
		return __pbn__JoinArmadaWeekDiscovery.HasValue;
	}

	public void ResetJoinArmadaWeekDiscovery()
	{
		__pbn__JoinArmadaWeekDiscovery = null;
	}
}
