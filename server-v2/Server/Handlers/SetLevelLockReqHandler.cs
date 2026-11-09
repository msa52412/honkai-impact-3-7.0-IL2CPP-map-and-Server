using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// SetLevelLockReq (1488) / SetLevelLockRsp (1489)
///
/// 这是「突破等级锁」的协议 —— 玩家在 80 级打满突破经验后点那个按钮，
/// 客户端发 level_lock_id=2 上来，服务端校验通过就回 Succ，
/// 之后 GetMainDataRsp.level_lock_id 变成 2，客户端把头像框换成红框、
/// 等级条上限放到 88、深渊切到终极区档位。
///
/// v2 原来这条协议完全没实现（日志里 0 次出现，说明请求被当成未知包丢掉，
/// 客户端那边也就一直卡在「未突破」）。
/// </summary>
[PacketCmdId(CmdId.SetLevelLockReq)]
internal class SetLevelLockReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetLevelLockReq req = packet.GetDecodedBody<SetLevelLockReq>();
		UserScheme user = session.Player.User;
		int level = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance()
			.CalculateLevel(user.Exp).Level;

		uint currentId;
		SetLevelLockRsp.Retcode ret = LevelLockConfig.Validate(req.LevelLockId, level, out currentId);

		Packet.c.Log($"[LevelLock] SetLevelLockReq target={req.LevelLockId} level={level} current={currentId} -> {ret}");

		if (ret == SetLevelLockRsp.Retcode.Succ)
		{
			// 切过去之后 Resolve() 会按新档位算，所以这里直接把 ForcedLockId 钉住，
			// 保证 GetMainData 下次下发的一定是玩家刚选的这档。
			LevelLockConfig.ForcedLockId = req.LevelLockId;
			Packet.c.Log("[LevelLock] 已切换 -> " + LevelLockConfig.Describe(level));
		}

		session.Send(Packet.FromProto(new SetLevelLockRsp
		{
			retcode = ret,
			// 失败时也要把当前档位回给客户端，让它的 UI 回到正确状态
			LevelLockId = (ret == SetLevelLockRsp.Retcode.Succ) ? req.LevelLockId : currentId
		}, CmdId.SetLevelLockRsp));
	}
}
