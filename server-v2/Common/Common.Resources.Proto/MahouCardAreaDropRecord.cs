using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardAreaDropRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "area_id")]
	public uint AreaId { get; set; }

	[ProtoMember(2, Name = "cur_drop_num")]
	public uint CurDropNum { get; set; }

	[ProtoMember(3, Name = "cur_site_id")]
	public uint CurSiteId { get; set; }

	[ProtoMember(4, Name = "is_unlocked_today")]
	public bool IsUnlockedToday { get; set; }

	[ProtoMember(5, Name = "unlocked_times")]
	public uint UnlockedTimes { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
