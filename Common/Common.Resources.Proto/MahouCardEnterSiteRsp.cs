using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardEnterSiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7905
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "IN_OTHER_SITE")]
		InOtherSite,
		[ProtoEnum(Name = "ROLE_ERROR")]
		RoleError
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(4, Name = "cur_site")]
	public MahouCardSiteInfo CurSite { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
