using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCFever : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__FeverSlotNum;

	private uint? __pbn__HasTakeRewardLevel;

	private uint? __pbn__ActiveSuiteId;

	[ProtoMember(1, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(2, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(3, Name = "fever_slot_num")]
	public uint FeverSlotNum
	{
		get
		{
			return __pbn__FeverSlotNum.GetValueOrDefault();
		}
		set
		{
			__pbn__FeverSlotNum = value;
		}
	}

	[ProtoMember(4, Name = "has_take_reward_level")]
	public uint HasTakeRewardLevel
	{
		get
		{
			return __pbn__HasTakeRewardLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakeRewardLevel = value;
		}
	}

	[ProtoMember(5, Name = "skill_suite_list")]
	public List<FeverSkillSuite> SkillSuiteLists { get; } = new List<FeverSkillSuite>();


	[ProtoMember(6, Name = "active_suite_id")]
	public uint ActiveSuiteId
	{
		get
		{
			return __pbn__ActiveSuiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActiveSuiteId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeFeverSlotNum()
	{
		return __pbn__FeverSlotNum.HasValue;
	}

	public void ResetFeverSlotNum()
	{
		__pbn__FeverSlotNum = null;
	}

	public bool ShouldSerializeHasTakeRewardLevel()
	{
		return __pbn__HasTakeRewardLevel.HasValue;
	}

	public void ResetHasTakeRewardLevel()
	{
		__pbn__HasTakeRewardLevel = null;
	}

	public bool ShouldSerializeActiveSuiteId()
	{
		return __pbn__ActiveSuiteId.HasValue;
	}

	public void ResetActiveSuiteId()
	{
		__pbn__ActiveSuiteId = null;
	}
}
