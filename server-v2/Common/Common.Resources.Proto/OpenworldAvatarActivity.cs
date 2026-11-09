using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldAvatarActivity : IExtensible
{
	[ProtoContract]
	public class Cultivate : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Id;

		private uint? __pbn__Level;

		[ProtoMember(1, Name = "id")]
		public uint Id
		{
			get
			{
				return __pbn__Id.GetValueOrDefault();
			}
			set
			{
				__pbn__Id = value;
			}
		}

		[ProtoMember(2, Name = "level")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeId()
		{
			return __pbn__Id.HasValue;
		}

		public void ResetId()
		{
			__pbn__Id = null;
		}

		public bool ShouldSerializeLevel()
		{
			return __pbn__Level.HasValue;
		}

		public void ResetLevel()
		{
			__pbn__Level = null;
		}
	}

	[ProtoContract]
	public class Talent : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__TalentId;

		private uint? __pbn__Level;

		[ProtoMember(1, Name = "talent_id")]
		public uint TalentId
		{
			get
			{
				return __pbn__TalentId.GetValueOrDefault();
			}
			set
			{
				__pbn__TalentId = value;
			}
		}

		[ProtoMember(2, Name = "level")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeTalentId()
		{
			return __pbn__TalentId.HasValue;
		}

		public void ResetTalentId()
		{
			__pbn__TalentId = null;
		}

		public bool ShouldSerializeLevel()
		{
			return __pbn__Level.HasValue;
		}

		public void ResetLevel()
		{
			__pbn__Level = null;
		}
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__ActivityId;

	private uint? __pbn__Level;

	private uint? __pbn__Exp;

	private uint? __pbn__DailyAcceptQuestTimes;

	private uint? __pbn__DailyRefreshQuestTimes;

	private uint? __pbn__TotalDailyQuestCurrencyNum;

	private uint? __pbn__ScheduleId;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "level")]
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

	[ProtoMember(4, Name = "exp")]
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

	[ProtoMember(5, Name = "daily_quest_list")]
	public List<OpenworldQuest> DailyQuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(6, Name = "daily_accept_quest_times")]
	public uint DailyAcceptQuestTimes
	{
		get
		{
			return __pbn__DailyAcceptQuestTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyAcceptQuestTimes = value;
		}
	}

	[ProtoMember(7, Name = "daily_refresh_quest_times")]
	public uint DailyRefreshQuestTimes
	{
		get
		{
			return __pbn__DailyRefreshQuestTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__DailyRefreshQuestTimes = value;
		}
	}

	[ProtoMember(8, Name = "special_quest_list")]
	public List<OpenworldQuest> SpecialQuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(9, Name = "talent_list")]
	public List<Talent> TalentLists { get; } = new List<Talent>();


	[ProtoMember(10, Name = "cultivate_list")]
	public List<Cultivate> CultivateLists { get; } = new List<Cultivate>();


	[ProtoMember(11, Name = "unlock_avatar_id_list")]
	public uint[] UnlockAvatarIdLists { get; set; }

	[ProtoMember(12, Name = "finished_quest_id_list")]
	public uint[] FinishedQuestIdLists { get; set; }

	[ProtoMember(13, Name = "total_daily_quest_currency_num")]
	public uint TotalDailyQuestCurrencyNum
	{
		get
		{
			return __pbn__TotalDailyQuestCurrencyNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalDailyQuestCurrencyNum = value;
		}
	}

	[ProtoMember(14, Name = "schedule_id")]
	public uint ScheduleId
	{
		get
		{
			return __pbn__ScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScheduleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
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

	public bool ShouldSerializeDailyAcceptQuestTimes()
	{
		return __pbn__DailyAcceptQuestTimes.HasValue;
	}

	public void ResetDailyAcceptQuestTimes()
	{
		__pbn__DailyAcceptQuestTimes = null;
	}

	public bool ShouldSerializeDailyRefreshQuestTimes()
	{
		return __pbn__DailyRefreshQuestTimes.HasValue;
	}

	public void ResetDailyRefreshQuestTimes()
	{
		__pbn__DailyRefreshQuestTimes = null;
	}

	public bool ShouldSerializeTotalDailyQuestCurrencyNum()
	{
		return __pbn__TotalDailyQuestCurrencyNum.HasValue;
	}

	public void ResetTotalDailyQuestCurrencyNum()
	{
		__pbn__TotalDailyQuestCurrencyNum = null;
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}
}
