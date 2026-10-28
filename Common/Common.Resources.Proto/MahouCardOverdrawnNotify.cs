using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardOverdrawnNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7938
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "overdrawn_card_list", IsPacked = true)]
	public uint[] OverdrawnCardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
