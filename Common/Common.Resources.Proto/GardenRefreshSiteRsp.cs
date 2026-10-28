using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GardenRefreshSiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4294
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GardenId;

	private uint? __pbn__LastGrowTime;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "garden_id")]
	public uint GardenId
	{
		get
		{
			return __pbn__GardenId.GetValueOrDefault();
		}
		set
		{
			__pbn__GardenId = value;
		}
	}

	[ProtoMember(3, Name = "site_list")]
	public List<GardenSite> SiteLists { get; } = new List<GardenSite>();


	[ProtoMember(4, Name = "last_grow_time")]
	public uint LastGrowTime
	{
		get
		{
			return __pbn__LastGrowTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastGrowTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGardenId()
	{
		return __pbn__GardenId.HasValue;
	}

	public void ResetGardenId()
	{
		__pbn__GardenId = null;
	}

	public bool ShouldSerializeLastGrowTime()
	{
		return __pbn__LastGrowTime.HasValue;
	}

	public void ResetLastGrowTime()
	{
		__pbn__LastGrowTime = null;
	}
}
