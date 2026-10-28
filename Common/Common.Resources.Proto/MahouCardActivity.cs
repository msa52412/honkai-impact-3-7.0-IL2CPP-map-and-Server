using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "area_daily_drop_record_list")]
	public List<MahouCardAreaDropRecord> AreaDailyDropRecordLists { get; } = new List<MahouCardAreaDropRecord>();


	[ProtoMember(3, Name = "site_list")]
	public List<MahouCardSite> SiteLists { get; } = new List<MahouCardSite>();


	[ProtoMember(4, Name = "role_list")]
	public List<MahouCardRole> RoleLists { get; } = new List<MahouCardRole>();


	[ProtoMember(5, Name = "has_got_card_id_list", IsPacked = true)]
	public uint[] HasGotCardIdLists { get; set; }

	[ProtoMember(6, Name = "cur_site_id")]
	public uint CurSiteId { get; set; }

	[ProtoMember(7, Name = "site_record_list")]
	public List<MahouCardSiteRecord> SiteRecordLists { get; } = new List<MahouCardSiteRecord>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
