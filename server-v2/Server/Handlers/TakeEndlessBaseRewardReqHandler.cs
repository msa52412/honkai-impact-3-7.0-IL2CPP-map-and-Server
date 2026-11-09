using System;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3417/3418 —— 领取旧深渊基础奖励。
///
/// 服务端还没有奖励配置/背包发奖链路，这里只回 Succ + 空奖励列表，
/// 让客户端把「已领取」状态关掉；真要发奖时往 RewardLists / RewardIdLists 里填。
/// </summary>
[PacketCmdId(CmdId.TakeEndlessBaseRewardReq)]
internal class TakeEndlessBaseRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeEndlessBaseRewardReq req = packet.GetDecodedBody<TakeEndlessBaseRewardReq>();
		EndlessType type = req.Type;
		if (type == EndlessType.EndlessTypeNone)
		{
			type = AbyssConfig.CurStatusType;
		}

		Packet.c.Log($"[Abyss] TakeBaseReward type={(int)type}");

		session.Send(Packet.FromProto(new TakeEndlessBaseRewardRsp
		{
			retcode = TakeEndlessBaseRewardRsp.Retcode.Succ,
			RewardProgress = 0u,
			RewardIdLists = Array.Empty<uint>(),
			Type = type
		}, CmdId.TakeEndlessBaseRewardRsp));
	}
}
