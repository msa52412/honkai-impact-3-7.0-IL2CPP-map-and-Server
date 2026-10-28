using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldCycleFinishRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4490
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Cycle;

	private uint? __pbn__MapId;

	[ProtoMember(1, Name = "cycle")]
	public uint Cycle
	{
		get
		{
			return __pbn__Cycle.GetValueOrDefault();
		}
		set
		{
			__pbn__Cycle = value;
		}
	}

	[ProtoMember(2, Name = "map_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCycle()
	{
		return __pbn__Cycle.HasValue;
	}

	public void ResetCycle()
	{
		__pbn__Cycle = null;
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}
}
[ProtoContract]
public class TakeOpenworldCycleFinishRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4491
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "STORY_NOT_FINISH")]
		StoryNotFinish,
		[ProtoEnum(Name = "CYCLE_NOT_MATCH")]
		CycleNotMatch,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__Cycle;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(3, Name = "map_id")]
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

	[ProtoMember(4, Name = "cycle")]
	public uint Cycle
	{
		get
		{
			return __pbn__Cycle.GetValueOrDefault();
		}
		set
		{
			__pbn__Cycle = value;
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

	public bool ShouldSerializeCycle()
	{
		return __pbn__Cycle.HasValue;
	}

	public void ResetCycle()
	{
		__pbn__Cycle = null;
	}
}
