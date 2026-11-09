using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 完成家园剧情（FinishDormTalkReq 641）：按 DormitoryTalkEvent 发奖，
/// 记录已完成 plot_id，并给对话里的角色加好感经验（touchExp）。
/// </summary>
[PacketCmdId(CmdId.FinishDormTalkReq)]
internal class FinishDormTalkReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishDormTalkReq req = packet.GetDecodedBody<FinishDormTalkReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		FinishDormTalkRsp rsp = new FinishDormTalkRsp
		{
			retcode = FinishDormTalkRsp.Retcode.Succ
		};

		DormitoryTalkEventExcel cfg = DormService.TalkConfig(req.PlotId);
		if (cfg == null)
		{
			rsp.retcode = FinishDormTalkRsp.Retcode.PlotError;
		}
		else if ((dorms.FinishTalkPlotIdLists ?? new uint[0]).Contains(req.PlotId))
		{
			rsp.retcode = FinishDormTalkRsp.Retcode.PlotFinished;
		}
		else
		{
			ProtoRewardData reward = DormService.GrantReward(session.Player, cfg.Reward);
			if (reward != null)
			{
				rsp.RewardLists.Add(reward);
				DormService.AppendDropItems(rsp.DropItemLists, reward);
			}

			dorms.FinishTalkPlotIdLists = (dorms.FinishTalkPlotIdLists ?? new uint[0]).Append(req.PlotId).ToArray();
			dorms.Save();

			foreach (uint avatarId in req.AvatarIdLists ?? new uint[0])
			{
				DormService.AddGoodfeel(session.Player, avatarId, cfg.TouchExp);
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.FinishDormTalkRsp));
	}
}
