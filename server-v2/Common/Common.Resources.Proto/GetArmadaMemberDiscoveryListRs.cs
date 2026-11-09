using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaMemberDiscoveryListRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2666
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

	[ProtoMember(3, Name = "member_discovery_list")]
	public List<ArmadaMemberDiscovery> MemberDiscoveryLists { get; } = new List<ArmadaMemberDiscovery>();


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
}
