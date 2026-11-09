using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldGetDishInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2457
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScheduleId;

	private bool? __pbn__IsPrepareRewardTaken;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "schedule_id")]
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

	[ProtoMember(3, Name = "is_prepare_reward_taken")]
	public bool IsPrepareRewardTaken
	{
		get
		{
			return __pbn__IsPrepareRewardTaken.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPrepareRewardTaken = value;
		}
	}

	[ProtoMember(4, Name = "dish_reward_list")]
	public List<ChatworldDishReward> DishRewardLists { get; } = new List<ChatworldDishReward>();


	[ProtoMember(5, Name = "taken_dish_limited_reward_list")]
	public uint[] TakenDishLimitedRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScheduleId()
	{
		return __pbn__ScheduleId.HasValue;
	}

	public void ResetScheduleId()
	{
		__pbn__ScheduleId = null;
	}

	public bool ShouldSerializeIsPrepareRewardTaken()
	{
		return __pbn__IsPrepareRewardTaken.HasValue;
	}

	public void ResetIsPrepareRewardTaken()
	{
		__pbn__IsPrepareRewardTaken = null;
	}
}
