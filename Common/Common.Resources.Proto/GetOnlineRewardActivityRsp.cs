using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOnlineRewardActivityRsp : IExtensible
{
	[ProtoContract]
	public class OnlineReward : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Id;

		private uint? __pbn__TakeTime;

		private bool? __pbn__IsTaken;

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

		[ProtoMember(2, Name = "take_time")]
		public uint TakeTime
		{
			get
			{
				return __pbn__TakeTime.GetValueOrDefault();
			}
			set
			{
				__pbn__TakeTime = value;
			}
		}

		[ProtoMember(3, Name = "is_taken")]
		public bool IsTaken
		{
			get
			{
				return __pbn__IsTaken.GetValueOrDefault();
			}
			set
			{
				__pbn__IsTaken = value;
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

		public bool ShouldSerializeTakeTime()
		{
			return __pbn__TakeTime.HasValue;
		}

		public void ResetTakeTime()
		{
			__pbn__TakeTime = null;
		}

		public bool ShouldSerializeIsTaken()
		{
			return __pbn__IsTaken.HasValue;
		}

		public void ResetIsTaken()
		{
			__pbn__IsTaken = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4197
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ACTIVITY_NOT_OPEN")]
		ActivityNotOpen,
		[ProtoEnum(Name = "LEVEL_NOT_SATISFY")]
		LevelNotSatisfy
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__BeginTime;

	private uint? __pbn__EndTime;

	private uint? __pbn__MinLevel;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "begin_time")]
	public uint BeginTime
	{
		get
		{
			return __pbn__BeginTime.GetValueOrDefault();
		}
		set
		{
			__pbn__BeginTime = value;
		}
	}

	[ProtoMember(3, Name = "end_time")]
	public uint EndTime
	{
		get
		{
			return __pbn__EndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__EndTime = value;
		}
	}

	[ProtoMember(4, Name = "min_level")]
	public uint MinLevel
	{
		get
		{
			return __pbn__MinLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__MinLevel = value;
		}
	}

	[ProtoMember(5, Name = "reward_list")]
	public List<OnlineReward> RewardLists { get; } = new List<OnlineReward>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeBeginTime()
	{
		return __pbn__BeginTime.HasValue;
	}

	public void ResetBeginTime()
	{
		__pbn__BeginTime = null;
	}

	public bool ShouldSerializeEndTime()
	{
		return __pbn__EndTime.HasValue;
	}

	public void ResetEndTime()
	{
		__pbn__EndTime = null;
	}

	public bool ShouldSerializeMinLevel()
	{
		return __pbn__MinLevel.HasValue;
	}

	public void ResetMinLevel()
	{
		__pbn__MinLevel = null;
	}
}
