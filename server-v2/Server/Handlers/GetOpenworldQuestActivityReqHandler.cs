using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4546 / 4547 —— 委托面板打开时问「本周活动委托信息」。
///
/// 这条是 OpenWorldQuestMapStubPageContext.SetupView 里**第一条**发出的请求
/// （反汇编 0x1819CF6D0 第一处业务调用就是 RequsetGetOpenworldQuestActivityReq），
/// 服务端原来没 handler，日志里 148 次全部是 not handled。
///
/// 客户端收到 4547 后不管内容如何都会调 MonoOpenWorldQuestMapUIManager.Reset(mapID)
/// （反汇编 0x1819CE8E0 → jmp 0x1831BA6D0），也就是说这条回包本身就是「解除转圈」的钥匙。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldQuestActivityReq)]
internal class GetOpenworldQuestActivityReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;

		// 客户端这条 Req 不带 map_id，只能汇总该玩家所有地图的委托存档。
		List<OwQuestScheme> states = OwQuest.collection.All().Where((OwQuestScheme x) => x.OwnerUid == uid).ToList();
		uint taken = 0u;
		uint totalScore = 0u;
		uint level = 1u;
		foreach (OwQuestScheme state in states)
		{
			taken += state.CycleQuestTakenNum;
			totalScore += state.TotalStar;
			level = System.Math.Max(level, state.QuestLevel);
		}

		// activity_id = 0 表示「当前没有活动委托」。
		// 表 OpenWorldQuestActivity 里樱色轮回那两条的 EndTime 是 2018-07-23，早就过期了，
		// 回 0 才是真实语义。
		GetOpenworldQuestActivityRsp proto = new GetOpenworldQuestActivityRsp
		{
			retcode = GetOpenworldQuestActivityRsp.Retcode.Succ,
			ActivityId = 0u,
			TotalScore = totalScore,
			CycleQuestTakenNum = taken
		};
		Packet.c.Log($"[OWQuest] GetOpenworldQuestActivityReq -> Succ activity=0 taken={taken} score={totalScore} lv={level}");
		session.Send(Packet.FromProto(proto, CmdId.GetOpenworldQuestActivityRsp));
	}
}
