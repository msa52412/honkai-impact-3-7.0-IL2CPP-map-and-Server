using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 任务 / 成就领奖（CmdId 114 / 115）。
/// 之前没有 handler，成就页点「领取」日志里只有 `GetMissionRewardReq not handled!`。
/// </summary>
[PacketCmdId(CmdId.GetMissionRewardReq)]
internal class GetMissionRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetMissionRewardReq req = packet.GetDecodedBody<GetMissionRewardReq>();
		uint[] ids = req.MissionIdLists ?? new uint[0];

		// 诊断：成就页的「领取」会带 mission_id，直接看日志就知道 7.0 客户端的成就 id 落在哪个区间。
		Packet.c.Log($"[Mission] uid={session.Player.User.Uid} GetMissionRewardReq mission_ids=[{string.Join(",", ids.Take(20))}] count={ids.Length}");

		GetMissionRewardRsp.Retcode retcode = MissionService.Take(session.Player, ids, out ProtoRewardData reward, out uint[] granted);

		GetMissionRewardRsp rsp = new GetMissionRewardRsp
		{
			retcode = retcode,
			RewardData = reward,
			MissionIdLists = granted,
			RewardUnlockTime = 0u
		};

		session.Send(Packet.FromProto(rsp, CmdId.GetMissionRewardRsp));

		// 领完之后主动补一份任务列表：客户端成就页只有在收到 GetMissionDataRsp 时才更新内部
		// mission dict，否则「领取」按钮会一直亮着、顶部计数也不动（刷新时才读 dict）。
		if (retcode == GetMissionRewardRsp.Retcode.Succ)
		{
            ArmadaContributionGameplay.Push(session);
            BattlePassGameplay.Push(session);
            DailyGameplay.PushInventory(session);
            if(ids.Any(GodWarProgression.IsDeepMission))session.ProcessPacket(Packet.FromProto(new GetMedalDataReq(),CmdId.GetMedalDataReq));

            DailyGameplay.PushDuty(session);
			session.Send(Packet.FromProto(MissionDataBuilder.Build(session.Player), CmdId.GetMissionDataRsp));
		}
	}
}
