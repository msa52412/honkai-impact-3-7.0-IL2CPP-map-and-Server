using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GobackGrowUpActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "joined_rank_id_list")]
	public uint[] JoinedRankIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
