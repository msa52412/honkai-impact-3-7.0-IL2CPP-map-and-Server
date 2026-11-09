using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Openworld;

/// <summary>
/// 4464 / 4465 —— 开放世界「关卡结束」。
///
/// 原来服务端没有 handler（日志里几十次 `OpenworldStageEndReq not handled!`），
/// 客户端退出一层 / 打完一层后一直等 `OpenworldStageEndRsp`，
/// 表现就是结算画面转圈，而且**后续包全被卡住**：
/// 后崩坏书 2 爬塔要先拿到这个 Rsp 才会继续发 `ScDLCTowerStageEndReq`。
///
/// 服务端不模拟战斗，直接回 SUCC + 原样带回 map_id。
/// 血量 / 能量 / 狂热分这些字段客户端会自己按本地结果刷新，不回填也不影响。
/// </summary>
[PacketCmdId(CmdId.OpenworldStageEndReq)]
internal class OpenworldStageEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldStageEndReq req = packet.GetDecodedBody<OpenworldStageEndReq>();

		Packet.c.Log($"[OW] StageEnd map={req.MapId} area={req.AreaId} "
			+ $"avatars={req.AvatarLists.Count} drops={req.DropItemLists.Count} "
			+ $"fever={req.ScDlcFeverScore} climax={req.ScDlcClimaxScore} -> Succ");

		session.Send(Packet.FromProto(new OpenworldStageEndRsp
		{
			retcode = OpenworldStageEndRsp.Retcode.Succ,
			MapId = req.MapId
		}, CmdId.OpenworldStageEndRsp));
	}
}
