using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 113
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

	private bool? __pbn__IsAll;

	private bool? __pbn__IsInActivity;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "mission_list")]
	public List<Mission> MissionLists { get; } = new List<Mission>();


	[ProtoMember(7, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	[ProtoMember(8, Name = "is_in_activity")]
	public bool IsInActivity
	{
		get
		{
			return __pbn__IsInActivity.GetValueOrDefault();
		}
		set
		{
			__pbn__IsInActivity = value;
		}
	}

	[ProtoMember(9, Name = "challenge_mission")]
	public ChallengeMissionData ChallengeMission { get; set; }

	[ProtoMember(10, Name = "close_mission_list")]
	public uint[] CloseMissionLists { get; set; }

	[ProtoMember(11, Name = "mainline_step")]
	public MainlineStepMission MainlineStep { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}

	public bool ShouldSerializeIsInActivity()
	{
		return __pbn__IsInActivity.HasValue;
	}

	public void ResetIsInActivity()
	{
		__pbn__IsInActivity = null;
	}
}
