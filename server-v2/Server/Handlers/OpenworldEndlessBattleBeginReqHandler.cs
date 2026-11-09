using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using System.Linq;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3438 —— 旧深渊进关后的「战斗开始」包。
///
/// 客户端 LevelQTEndlessPlugin 在场景加载完就发这一包并等 3439；
/// 之前服务端没实现，包被丢弃，插件会一直停在等待态。
/// 服务端只需要记下 battle_id 并回 Succ —— 真正的进度推进在 3424。
/// </summary>
[PacketCmdId(CmdId.OpenworldEndlessBattleBeginReq)]
internal class OpenworldEndlessBattleBeginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldEndlessBattleBeginReq req = packet.GetDecodedBody<OpenworldEndlessBattleBeginReq>();
		uint type = (uint)req.Type;
		if (type == 0u)
		{
			type = (uint)EndlessType.EndlessTypeOpenworld;
		}

		OwEndlessState state = OwEndlessState.Get(session.Player.User.Uid, type);
		lock (state.Gate)
		{
			if (req.BattleId != 0u)
			{
				state.LastBattleId = req.BattleId;
			}
			if (req.GroupLevel != 0u)
			{
				state.GroupLevel = req.GroupLevel;
			}
		}

		// 道具状态也要记住「当前战斗」：3422 的 proto 里根本没有 battle_id
		// （NetworkManager.RequestUseOpenworldEndlessItemReq 0x181AD4F60 会把第 4 个参数直接丢掉），
		// 所以关卡内用陷阱时只能靠这一包记下来的 battle_id 决定落点。
		OwEndlessItemState items = OwEndlessItems.Get(session.Player.User.Uid, type);
		lock (items.Gate)
		{
			if (req.BattleId != 0u)
			{
				items.LastBattleId = req.BattleId;
				OWEndlessBattleAreaExcel? row = OWEndlessBattleArea.GetInstance()
					.All.FirstOrDefault((OWEndlessBattleAreaExcel r) => r.BattleID == (int)req.BattleId);
				if (row != null)
				{
					items.LastAreaId = (uint)row.AreaId;
				}
			}
		}

		Packet.c.Log($"[Abyss] BattleBegin type={type} battle={req.BattleId} "
			+ $"group={req.MonsterGroupId} layer={req.LayerNumber} tx={req.TransactionStr}");

		session.Send(Packet.FromProto(new OpenworldEndlessBattleBeginRsp
		{
			retcode = OpenworldEndlessBattleBeginRsp.Retcode.Succ
		}, CmdId.OpenworldEndlessBattleBeginRsp));
	}
}
