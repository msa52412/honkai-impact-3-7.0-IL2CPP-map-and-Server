
namespace Common.Database;

public class OpenWorldScheme : IRow
{
	public long Id { get; set; }

	public long CreatedAt { get; set; }

	public uint OwnerUid { get; set; }

	public uint MapId { get; set; }

	public uint Cycle { get; set; }

	public uint QuestLevel { get; set; }

	public uint HasTakeFinishRewardCycle { get; set; }

	public string SpawnPoint { get; set; }

	public void Save()
	{
		OpenWorld.collection.ReplaceOne(this);
	}

}
