using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EditAdventureRoomReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3920
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "decoration_slot")]
	public AdventureDecorationSlot DecorationSlot { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
