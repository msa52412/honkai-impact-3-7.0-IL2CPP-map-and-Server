using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleComponentSet : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "position_component")]
	public QCandyBattleComponentPosition PositionComponent { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
