using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class NinjaActivityNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4404
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "slot_list")]
	public List<NinjaSlot> SlotLists { get; } = new List<NinjaSlot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
