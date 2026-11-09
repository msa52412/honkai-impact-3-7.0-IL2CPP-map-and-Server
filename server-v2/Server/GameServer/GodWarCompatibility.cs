using System;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using ProtoBuf;
using ClientData = Common.Database.ClientData;

namespace PemukulPaku.GameServer.Game;

internal static class GodWarCompatibility
{
	// 7.0 client has these missions, but the bundled 6.6 server table does not.
	// 90200..90202 were read directly from the failing role (3601); all twelve
	// IDs were verified against the running 7.0 client's MissionData index.
	// Keep these records available even when no server reward metadata exists.
	internal static readonly uint[] AvatarMissionIds = Enumerable.Range(90200, 12).Select(x => (uint)x).ToArray();

	internal static void AddMissingMissions(GetMissionDataRsp rsp, bool autoClose, uint[] taken)
	{
		var present = rsp.MissionLists.Select(x => x.MissionId)
			.Concat(rsp.CloseMissionLists ?? Array.Empty<uint>()).ToHashSet();
		foreach (uint id in AvatarMissionIds.Where(id => !present.Contains(id)))
		{
			rsp.MissionLists.Add(new Mission
			{
				MissionId = id,
				Status = autoClose || taken.Contains(id) ? MissionStatus.MissionClose : MissionStatus.MissionDoing,
				Progress = 0u
			});
		}
	}

	internal static void CompleteIntro(uint uid)
	{
		const int entryPerformId = 29;
		var saved = ClientData.GetClientData(uid, ClientDataType.ClientDataAugmentPerformance, 0);
		AugmentPerformanceLocalMessage data = saved?.Data?.Length > 0
			? Serializer.Deserialize<AugmentPerformanceLocalMessage>(new MemoryStream(saved.Data))
			: new AugmentPerformanceLocalMessage();
		var finished = data.FinishedEntryPerformIdLists ?? Array.Empty<int>();
		if (finished.Contains(entryPerformId)) return;
		data.FinishedEntryPerformIdLists = finished.Append(entryPerformId).ToArray();
		using var stream = new MemoryStream();
		Serializer.Serialize(stream, data);
		ClientData.SetClientData(uid, ClientDataType.ClientDataAugmentPerformance, 0, stream.ToArray());
		Packet.c.Log($"[GodWar] Saved completed intro entry=29 uid={uid}");
	}
}
