using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 4194 TakeLoginActivityRewardReq —— 点授勋格子领取。
///
/// 请求字段：id（活动）、take_days（第几天，从 1 开始）、reward_type（0 普通 / 1 额外）。
/// 服务端按 LoginRewardSchedule[ID].rewardList[take_days - 1] 找 RewardData，
/// 校验「累计登录天数 >= take_days」且「没领过」，发奖落库后把 RewardData 回给客户端。
///
/// 这个 handler 之前完全不存在 —— 客户端点领取没有任何回包，界面只能一直转圈/无反应。
/// </summary>
[PacketCmdId(CmdId.TakeLoginActivityRewardReq)]
internal class TakeLoginActivityRewardReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		TakeLoginActivityRewardReq req = packet.GetDecodedBody<TakeLoginActivityRewardReq>();
		uint rewardType = (uint)req.RewardType;

		TakeLoginActivityRewardRsp.Retcode retcode = LoginActivityService.Take(
			session.Player, req.Id, req.TakeDays, rewardType, out RewardData reward);

		TakeLoginActivityRewardRsp rsp = new TakeLoginActivityRewardRsp
		{
			retcode = retcode,
			Id = req.Id,
			TakeDays = req.TakeDays,
			RewardType = req.RewardType
		};

		if (retcode == TakeLoginActivityRewardRsp.Retcode.Succ && reward != null)
		{
			rsp.RewardLists.Add(reward);
		}

		session.Send(Packet.FromProto(rsp, CmdId.TakeLoginActivityRewardRsp));
	}
}
