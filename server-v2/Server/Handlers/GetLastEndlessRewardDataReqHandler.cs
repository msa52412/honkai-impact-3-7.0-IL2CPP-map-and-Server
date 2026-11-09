using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3406/3407 —— 上次周期结算奖励数据。
///
/// 客户端在深渊主界面请求它来画「上期结算」横幅。
/// 服务端没有真正的周期结算，回一份「未参战」的空数据：
/// Rank=0 / RewardId=0 / 空奖励列表。字段齐全即可，客户端不会自己造错误。
/// </summary>
[PacketCmdId(CmdId.GetLastEndlessRewardDataReq)]
internal class GetLastEndlessRewardDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint type = (uint)AbyssConfig.CurStatusType;
		OWEndlessGroupExcel group = OwEndlessConfig.ResolveGroup((int)type, (int)OwTuning.GroupLevel);

		uint score = 0u;
		OwEndlessState? state = OwEndlessState.Find(session.Player.User.Uid, type);
		if (state != null)
		{
			lock (state.Gate)
			{
				score = state.Score;
			}
		}

		session.Send(Packet.FromProto(new GetLastEndlessRewardDataRsp
		{
			retcode = GetLastEndlessRewardDataRsp.Retcode.Succ,
			BeginTime = 0u,
			GroupLevel = (uint)group.GroupLevel,
			CurGroupLevel = (uint)group.GroupLevel,
			Rank = 0u,
			ProgressOrScore = score,
			RewardType = EndlessRewardType.EndlessNormal,
			RewardId = 0u,
			Type = AbyssConfig.SelectedEndlessType
		}, CmdId.GetLastEndlessRewardDataRsp));
	}
}
