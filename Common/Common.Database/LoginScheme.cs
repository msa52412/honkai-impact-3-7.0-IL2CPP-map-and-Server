using System;

namespace Common.Database;

public class LoginScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint GetCreationTime()
	{
		return (uint)CreatedAt;
	}
}
