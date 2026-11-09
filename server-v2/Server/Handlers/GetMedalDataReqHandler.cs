using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 收藏 → 勋章（449 → 450）。
///
/// 列表和 end_time 统一走 <see cref="MedalService.AllMedals"/>，与
/// GetMainDataRsp.on_medal_id / MedalOpReq 的判定口径保持一致 —— 三处必须同一套规则，
/// 否则会出现「列表里是灰的但能戴」「戴上了刷新又没了」这类自相矛盾的状态。
///
/// 本地服策略：MedalData.json 里有的都算拥有（130 枚）。永久勋章 end_time = 0；
/// 限时勋章给账号一条共用的 999 天截止时间（见 MedalService.LimitedDays）。
/// </summary>
[PacketCmdId(CmdId.GetMedalDataReq)]
internal class GetMedalDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetMedalDataRsp rsp = new GetMedalDataRsp
		{
			retcode = GetMedalDataRsp.Retcode.Succ,
			IsAll = true
		};
		rsp.MedalLists.AddRange(MedalService.AllMedals(session.Player.User.Uid));

		session.Send(Packet.FromProto(rsp, CmdId.GetMedalDataRsp));
	}
}
