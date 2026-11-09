using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MahouCardUnlockDailySiteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7934
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId { get; set; }

	[ProtoMember(2, Name = "area_id")]
	public uint AreaId { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
