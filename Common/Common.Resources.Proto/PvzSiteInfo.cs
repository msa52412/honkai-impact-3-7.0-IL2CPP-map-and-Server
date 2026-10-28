using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzSiteInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__SiteId;

	private uint? __pbn__TowerId;

	private PvzSiteType? __pbn__SiteType;

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

	[ProtoMember(2, Name = "tower_id")]
	public uint TowerId
	{
		get
		{
			return __pbn__TowerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TowerId = value;
		}
	}

	[ProtoMember(4, Name = "site_type")]
	[DefaultValue(PvzSiteType.PvzSiteNormal)]
	public PvzSiteType SiteType
	{
		get
		{
			return __pbn__SiteType ?? PvzSiteType.PvzSiteNormal;
		}
		set
		{
			__pbn__SiteType = value;
		}
	}

	[ProtoMember(5, Name = "has_drop_material_list")]
	public List<PvzSiteDropMaterial> HasDropMaterialLists { get; } = new List<PvzSiteDropMaterial>();


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

	public bool ShouldSerializeTowerId()
	{
		return __pbn__TowerId.HasValue;
	}

	public void ResetTowerId()
	{
		__pbn__TowerId = null;
	}

	public bool ShouldSerializeSiteType()
	{
		return __pbn__SiteType.HasValue;
	}

	public void ResetSiteType()
	{
		__pbn__SiteType = null;
	}
}
