using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetOpenworldMapReq)]
internal class GetOpenworldMapReqHandler : IPacketHandler
{
	/// <summary>已打过日志的 mapId，避免刷屏。</summary>
	private static readonly HashSet<uint> Logged = new HashSet<uint>();

	public void Handle(Session session, Packet packet)
	{
		GetOpenworldMapReq Data = packet.GetDecodedBody<GetOpenworldMapReq>();
		OpenWorldScheme openWorldScheme = session.Player.OpenWorlds.FirstOrDefault((OpenWorldScheme x) => x.MapId == Data.MapId);

		bool first;
		lock (Logged)
		{
			first = Logged.Add(Data.MapId);
		}

		if (openWorldScheme == null)
		{
			// 原来这里直接 return —— 客户端一个字节都收不到，只能干等到超时。
			// 开放世界页面就是这么被判成「空」然后 2 秒退出的。
			// 现在改成：现场补一条临时 scheme 并正常应答，同时把 mapId 打出来。
			openWorldScheme = new OpenWorldScheme
			{
				OwnerUid = session.Player.User.Uid,
				MapId = Data.MapId,
				Cycle = 0u,
				QuestLevel = 1u,
				HasTakeFinishRewardCycle = 0u,
				SpawnPoint = ""
			};
			Packet.c.Log($"[Abyss] GetOpenworldMapReq mapId={Data.MapId} 存档里没有 → 临时补一条");
		}
		else if (first)
		{
			Packet.c.Log($"[Abyss] GetOpenworldMapReq mapId={Data.MapId} cycle={openWorldScheme.Cycle} spawn='{openWorldScheme.SpawnPoint}'");
		}

		GetOpenworldMapRsp proto = new GetOpenworldMapRsp
		{
			retcode = GetOpenworldMapRsp.Retcode.Succ,
			MapId = Data.MapId,
			Cycle = openWorldScheme.Cycle,
			EventRandomSeed = Global.GetRandomSeed(),
			SpawnPoint = openWorldScheme.SpawnPoint,
			Status = 3u,
			QuestData = new OpenworldQuestData
			{
				IsOpen = false,
				DayOpenTimes = 0u,
				RefreshLeftTimes = 0u,
				NextRefreshCost = 0u,
				IsCanAbandon = true,
				ChallengeScore = 0u,
				IsQuestFinish = false,
				OpenQuestTime = 0u
			},
			TechData = new OpenworldTechData
			{
				MapId = Data.MapId
			},
			HasTakeFinishRewardCycle = openWorldScheme.HasTakeFinishRewardCycle
		};
		session.Send(Packet.FromProto(proto, CmdId.GetOpenworldMapRsp));
	}
}
