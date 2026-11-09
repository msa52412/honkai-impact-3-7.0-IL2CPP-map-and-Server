using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownBuilding : IExtensible
{
	[ProtoContract]
	public class SkillTriggerTimes : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__SkillId;

		private uint? __pbn__TriggerTimes;

		[ProtoMember(1, Name = "skill_id")]
		public uint SkillId
		{
			get
			{
				return __pbn__SkillId.GetValueOrDefault();
			}
			set
			{
				__pbn__SkillId = value;
			}
		}

		[ProtoMember(2, Name = "trigger_times")]
		public uint TriggerTimes
		{
			get
			{
				return __pbn__TriggerTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__TriggerTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeSkillId()
		{
			return __pbn__SkillId.HasValue;
		}

		public void ResetSkillId()
		{
			__pbn__SkillId = null;
		}

		public bool ShouldSerializeTriggerTimes()
		{
			return __pbn__TriggerTimes.HasValue;
		}

		public void ResetTriggerTimes()
		{
			__pbn__TriggerTimes = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__UniqueId;

	private uint? __pbn__BuildingId;

	private uint? __pbn__Position;

	private uint? __pbn__LeftHp;

	private uint? __pbn__NextIntervalSkillRound;

	[ProtoMember(1, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(2, Name = "building_id")]
	public uint BuildingId
	{
		get
		{
			return __pbn__BuildingId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuildingId = value;
		}
	}

	[ProtoMember(3, Name = "position")]
	public uint Position
	{
		get
		{
			return __pbn__Position.GetValueOrDefault();
		}
		set
		{
			__pbn__Position = value;
		}
	}

	[ProtoMember(4, Name = "occupy_position_list")]
	public uint[] OccupyPositionLists { get; set; }

	[ProtoMember(5, Name = "skill_trigger_times_list")]
	public List<SkillTriggerTimes> SkillTriggerTimesLists { get; } = new List<SkillTriggerTimes>();


	[ProtoMember(6, Name = "left_hp")]
	public uint LeftHp
	{
		get
		{
			return __pbn__LeftHp.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftHp = value;
		}
	}

	[ProtoMember(7, Name = "activated_duration_skill_id_list")]
	public uint[] ActivatedDurationSkillIdLists { get; set; }

	[ProtoMember(8, Name = "waiting_avatar_id_list")]
	public uint[] WaitingAvatarIdLists { get; set; }

	[ProtoMember(9, Name = "next_interval_skill_round")]
	public uint NextIntervalSkillRound
	{
		get
		{
			return __pbn__NextIntervalSkillRound.GetValueOrDefault();
		}
		set
		{
			__pbn__NextIntervalSkillRound = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeBuildingId()
	{
		return __pbn__BuildingId.HasValue;
	}

	public void ResetBuildingId()
	{
		__pbn__BuildingId = null;
	}

	public bool ShouldSerializePosition()
	{
		return __pbn__Position.HasValue;
	}

	public void ResetPosition()
	{
		__pbn__Position = null;
	}

	public bool ShouldSerializeLeftHp()
	{
		return __pbn__LeftHp.HasValue;
	}

	public void ResetLeftHp()
	{
		__pbn__LeftHp = null;
	}

	public bool ShouldSerializeNextIntervalSkillRound()
	{
		return __pbn__NextIntervalSkillRound.HasValue;
	}

	public void ResetNextIntervalSkillRound()
	{
		__pbn__NextIntervalSkillRound = null;
	}
}
