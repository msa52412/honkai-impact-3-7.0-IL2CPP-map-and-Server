using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardLevelUpCardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7917
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "site_id")]
	public uint SiteId { get; set; }

	[ProtoMember(3, Name = "room_id")]
	public uint RoomId { get; set; }

	[ProtoMember(4, Name = "cost_unique_id")]
	public uint CostUniqueId { get; set; }

	[ProtoMember(5, Name = "target_unique_id")]
	public uint TargetUniqueId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
