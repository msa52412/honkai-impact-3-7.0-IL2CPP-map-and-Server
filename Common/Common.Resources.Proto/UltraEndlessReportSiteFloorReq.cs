using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UltraEndlessReportSiteFloorReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5206
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__Floor;

	private uint? __pbn__Score;

	private uint? __pbn__CostTime;

	private uint? __pbn__TotalCostTime;

	private bool? __pbn__IsUpFloor;

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

	[ProtoMember(2, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	[ProtoMember(3, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(4, Name = "cost_time")]
	public uint CostTime
	{
		get
		{
			return __pbn__CostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CostTime = value;
		}
	}

	[ProtoMember(5, Name = "total_cost_time")]
	public uint TotalCostTime
	{
		get
		{
			return __pbn__TotalCostTime.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalCostTime = value;
		}
	}

	[ProtoMember(6, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(7, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	[ProtoMember(8, Name = "is_up_floor")]
	public bool IsUpFloor
	{
		get
		{
			return __pbn__IsUpFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUpFloor = value;
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

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeCostTime()
	{
		return __pbn__CostTime.HasValue;
	}

	public void ResetCostTime()
	{
		__pbn__CostTime = null;
	}

	public bool ShouldSerializeTotalCostTime()
	{
		return __pbn__TotalCostTime.HasValue;
	}

	public void ResetTotalCostTime()
	{
		__pbn__TotalCostTime = null;
	}

	public bool ShouldSerializeIsUpFloor()
	{
		return __pbn__IsUpFloor.HasValue;
	}

	public void ResetIsUpFloor()
	{
		__pbn__IsUpFloor = null;
	}
}
