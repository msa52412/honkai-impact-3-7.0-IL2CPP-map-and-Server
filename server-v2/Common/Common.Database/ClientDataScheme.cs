using Common.Resources.Proto;

namespace Common.Database;

public class ClientDataScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public ClientDataType Type { get; set; }

	public uint ClientDataId { get; set; }

	public byte[] Data { get; set; }
}
