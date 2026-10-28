using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InLevelAttackSnapshotInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "context")]
	public InLevelContext Context { get; set; }

	[ProtoMember(2, Name = "snapshot_list")]
	public List<InLevelAttackSnapshot> SnapshotLists { get; } = new List<InLevelAttackSnapshot>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
