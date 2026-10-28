using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PupilMainData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LastDismissTime;

	private uint? __pbn__PupilId;

	private string __pbn__Nickname;

	private uint? __pbn__CurStepId;

	private uint? __pbn__LeftAssistantTimes;

	private uint? __pbn__LeftFrozenTime;

	private uint? __pbn__PoolFameNum;

	private bool? __pbn__IsGraduated;

	[ProtoMember(1, Name = "step_list")]
	public List<PupilStep> StepLists { get; } = new List<PupilStep>();


	[ProtoMember(2, Name = "last_dismiss_time")]
	public uint LastDismissTime
	{
		get
		{
			return __pbn__LastDismissTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastDismissTime = value;
		}
	}

	[ProtoMember(3, Name = "pupil_id")]
	public uint PupilId
	{
		get
		{
			return __pbn__PupilId.GetValueOrDefault();
		}
		set
		{
			__pbn__PupilId = value;
		}
	}

	[ProtoMember(4, Name = "nickname")]
	[DefaultValue("")]
	public string Nickname
	{
		get
		{
			return __pbn__Nickname ?? "";
		}
		set
		{
			__pbn__Nickname = value;
		}
	}

	[ProtoMember(5, Name = "cur_step_id")]
	public uint CurStepId
	{
		get
		{
			return __pbn__CurStepId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurStepId = value;
		}
	}

	[ProtoMember(6, Name = "left_assistant_times")]
	public uint LeftAssistantTimes
	{
		get
		{
			return __pbn__LeftAssistantTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftAssistantTimes = value;
		}
	}

	[ProtoMember(7, Name = "left_frozen_time")]
	public uint LeftFrozenTime
	{
		get
		{
			return __pbn__LeftFrozenTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftFrozenTime = value;
		}
	}

	[ProtoMember(8, Name = "master_brief")]
	public PlayerFriendBriefData MasterBrief { get; set; }

	[ProtoMember(9, Name = "pool_fame_num")]
	public uint PoolFameNum
	{
		get
		{
			return __pbn__PoolFameNum.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolFameNum = value;
		}
	}

	[ProtoMember(10, Name = "is_graduated")]
	public bool IsGraduated
	{
		get
		{
			return __pbn__IsGraduated.GetValueOrDefault();
		}
		set
		{
			__pbn__IsGraduated = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLastDismissTime()
	{
		return __pbn__LastDismissTime.HasValue;
	}

	public void ResetLastDismissTime()
	{
		__pbn__LastDismissTime = null;
	}

	public bool ShouldSerializePupilId()
	{
		return __pbn__PupilId.HasValue;
	}

	public void ResetPupilId()
	{
		__pbn__PupilId = null;
	}

	public bool ShouldSerializeNickname()
	{
		return __pbn__Nickname != null;
	}

	public void ResetNickname()
	{
		__pbn__Nickname = null;
	}

	public bool ShouldSerializeCurStepId()
	{
		return __pbn__CurStepId.HasValue;
	}

	public void ResetCurStepId()
	{
		__pbn__CurStepId = null;
	}

	public bool ShouldSerializeLeftAssistantTimes()
	{
		return __pbn__LeftAssistantTimes.HasValue;
	}

	public void ResetLeftAssistantTimes()
	{
		__pbn__LeftAssistantTimes = null;
	}

	public bool ShouldSerializeLeftFrozenTime()
	{
		return __pbn__LeftFrozenTime.HasValue;
	}

	public void ResetLeftFrozenTime()
	{
		__pbn__LeftFrozenTime = null;
	}

	public bool ShouldSerializePoolFameNum()
	{
		return __pbn__PoolFameNum.HasValue;
	}

	public void ResetPoolFameNum()
	{
		__pbn__PoolFameNum = null;
	}

	public bool ShouldSerializeIsGraduated()
	{
		return __pbn__IsGraduated.HasValue;
	}

	public void ResetIsGraduated()
	{
		__pbn__IsGraduated = null;
	}
}
