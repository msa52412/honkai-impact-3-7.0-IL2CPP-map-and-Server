using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCDailyQuestData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__LeftAcceptTimes;

	private uint? __pbn__RefreshLeftTimes;

	private uint? __pbn__RefreshCostMaterialId;

	private uint? __pbn__RefreshCostMaterialNum;

	[ProtoMember(1, Name = "quest_list")]
	public List<OpenworldQuest> QuestLists { get; } = new List<OpenworldQuest>();


	[ProtoMember(2, Name = "left_accept_times")]
	public uint LeftAcceptTimes
	{
		get
		{
			return __pbn__LeftAcceptTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftAcceptTimes = value;
		}
	}

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

	[ProtoMember(4, Name = "refresh_cost_material_id")]
	public uint RefreshCostMaterialId
	{
		get
		{
			return __pbn__RefreshCostMaterialId.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshCostMaterialId = value;
		}
	}

	[ProtoMember(5, Name = "refresh_cost_material_num")]
	public uint RefreshCostMaterialNum
	{
		get
		{
			return __pbn__RefreshCostMaterialNum.GetValueOrDefault();
		}
		set
		{
			__pbn__RefreshCostMaterialNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeLeftAcceptTimes()
	{
		return __pbn__LeftAcceptTimes.HasValue;
	}

	public void ResetLeftAcceptTimes()
	{
		__pbn__LeftAcceptTimes = null;
	}

	public bool ShouldSerializeRefreshLeftTimes()
	{
		return __pbn__RefreshLeftTimes.HasValue;
	}

	public void ResetRefreshLeftTimes()
	{
		__pbn__RefreshLeftTimes = null;
	}

	public bool ShouldSerializeRefreshCostMaterialId()
	{
		return __pbn__RefreshCostMaterialId.HasValue;
	}

	public void ResetRefreshCostMaterialId()
	{
		__pbn__RefreshCostMaterialId = null;
	}

	public bool ShouldSerializeRefreshCostMaterialNum()
	{
		return __pbn__RefreshCostMaterialNum.HasValue;
	}

	public void ResetRefreshCostMaterialNum()
	{
		__pbn__RefreshCostMaterialNum = null;
	}
}
