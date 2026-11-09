using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleComponentPosition : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "postion")]
	public QCandyBattleVector3 Postion { get; set; }

	[ProtoMember(2, Name = "forward")]
	public QCandyBattleVector3 Forward { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
