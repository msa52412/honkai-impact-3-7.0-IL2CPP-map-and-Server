using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RefreshOpenworldQuestRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4481
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "HAS_CHOOSE")]
		HasChoose,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "MAP_NOT_FOUND")]
		MapNotFound
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__RefreshLeftTimes;

	private uint? __pbn__NextRefreshCost;

	private uint? __pbn__MapId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(3, Name = "refresh_left_times")]
	public uint RefreshLeftTimes
	{
		get
		{
			return __pbn__RefreshLeftTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshLeftTimes = value;
		}
	}

	[ProtoMember(4, Name = "next_refresh_cost")]
	public uint NextRefreshCost
	{
		get
		{
			return __pbn__NextRefreshCost.GetValueOrDefault();
		}
		set
		{
			__pbn__NextRefreshCost = value;
		}
	}

	[ProtoMember(5, Name = "map_id")]
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

	public bool ShouldSerializeRefreshLeftTimes()
	{
		return __pbn__RefreshLeftTimes.HasValue;
	}

	public void ResetRefreshLeftTimes()
	{
		__pbn__RefreshLeftTimes = null;
	}

	public bool ShouldSerializeNextRefreshCost()
	{
		return __pbn__NextRefreshCost.HasValue;
	}

	public void ResetNextRefreshCost()
	{
		__pbn__NextRefreshCost = null;
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
