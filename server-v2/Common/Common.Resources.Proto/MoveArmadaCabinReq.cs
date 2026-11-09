using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MoveArmadaCabinReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2635
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "grid_list")]
	public List<ArmadaGrid> GridLists { get; } = new List<ArmadaGrid>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
