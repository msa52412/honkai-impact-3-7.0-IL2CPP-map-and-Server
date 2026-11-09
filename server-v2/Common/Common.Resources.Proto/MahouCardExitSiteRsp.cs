using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardExitSiteRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7926
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
		[ProtoEnum(Name = "NOT_IN_SITE")]
		NotInSite
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(3, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(4, Name = "reward")]
	public RewardData Reward { get; set; }

	[ProtoMember(5, Name = "daily_site_drop_num")]
	public uint DailySiteDropNum { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
