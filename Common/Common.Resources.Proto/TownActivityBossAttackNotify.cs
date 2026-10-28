using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TownActivityBossAttackNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6465
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "head")]
	public TownCommonHead Head { get; set; }

	[ProtoMember(2, Name = "attacked_position_list")]
	public uint[] AttackedPositionLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
