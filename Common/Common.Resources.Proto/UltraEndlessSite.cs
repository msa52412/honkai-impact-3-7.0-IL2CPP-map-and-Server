using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessSite : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__MaxScoreCostTime;

	[ProtoMember(1, Name = "site_id")]
	public uint SiteId
	{
		get
		{
			return __pbn__SiteId.GetValueOrDefault();
		}
		set
		{
			__pbn__SiteId = value;
		}
	}

	[ProtoMember(2, Name = "floor_list")]
	public List<UltraEndlessFloor> FloorLists { get; } = new List<UltraEndlessFloor>();


	[ProtoMember(3, Name = "max_score_cost_time")]
	public uint MaxScoreCostTime
	{
		get
		{
			return __pbn__MaxScoreCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScoreCostTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSiteId()
	{
		return __pbn__SiteId.HasValue;
	}

	public void ResetSiteId()
	{
		__pbn__SiteId = null;
	}

	public bool ShouldSerializeMaxScoreCostTime()
	{
		return __pbn__MaxScoreCostTime.HasValue;
	}

	public void ResetMaxScoreCostTime()
	{
		__pbn__MaxScoreCostTime = null;
	}
}
