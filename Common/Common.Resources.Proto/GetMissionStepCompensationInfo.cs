using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionStepCompensationInfoReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1632
	}

	private IExtension __pbn__extensionData;

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class GetMissionStepCompensationInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1633
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_REWARD")]
		NoReward
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__HasTakeReward;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "challenge_mission_compensation_list")]
	public List<MissionStepCompensation> ChallengeMissionCompensationLists { get; } = new List<MissionStepCompensation>();


	[ProtoMember(3, Name = "mainline_misssion_compensation")]
	public MissionStepCompensation MainlineMisssionCompensation { get; set; }

	[ProtoMember(4, Name = "has_take_reward")]
	public bool HasTakeReward
	{
		get
		{
			return __pbn__HasTakeReward.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeReward = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHasTakeReward()
	{
		return __pbn__HasTakeReward.HasValue;
	}

	public void ResetHasTakeReward()
	{
		__pbn__HasTakeReward = null;
	}
}
