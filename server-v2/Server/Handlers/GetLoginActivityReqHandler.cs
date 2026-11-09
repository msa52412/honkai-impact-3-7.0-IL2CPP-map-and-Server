using System;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 4192 GetLoginActivityReq —— 「授勋」界面的数据源。
///
/// 原来写死一条 id=581、只给 login_days，没有 has_take_days_reward_list：
///   1. 领取后重登红点/按钮又变回「可领取」；
///   2. duration_end_time 用「建号 + 14 天」，14 天后客户端直接判活动结束。
///
/// 现在活动从 LoginRewardSchedule.json 里挑（结束时间没过的都发），
/// 已领取天数从 LoginActivityTakes 表读，结束时间用配置的 endTime。
/// </summary>
[PacketCmdId(CmdId.GetLoginActivityReq)]
internal class GetLoginActivityReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetLoginActivityRsp rsp = new GetLoginActivityRsp
		{
			retcode = GetLoginActivityRsp.Retcode.Succ
		};

		uint uid = session.Player.User.Uid;
		uint creationTime = session.Player.User.GetCreationTime();

		foreach (var cfg in LoginActivityService.Active())
		{
			var data = LoginActivityService.Build(uid, cfg, creationTime);
			rsp.LoginLists.Add(data);
			Packet.c.Log($"[LoginActivity] 下发 id={data.Id} login_days={data.LoginDays} accept={data.AcceptTime} end={data.DurationEndTime} 已领=[{string.Join(",", data.HasTakeDaysRewardLists ?? Array.Empty<uint>())}]");
		}

		session.Send(Packet.FromProto(rsp, CmdId.GetLoginActivityRsp));
	}
}
