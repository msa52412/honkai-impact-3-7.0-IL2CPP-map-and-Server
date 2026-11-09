using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleMoveNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6574
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "pos")]
	public QCandyBattleVector3 Pos { get; set; }

	[ProtoMember(2, Name = "forward")]
	public QCandyBattleVector3 Forward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
