using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("level", "<1-88>", CommandType.Player, new string[] { })]
internal class LevelCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		Run(session.Player, args);
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.Level = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(session.Player.User.Exp).Level;
		getMainDataRsp.Exp = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(session.Player.User.Exp).Exp;
		getMainDataRsp.TypeLists = new uint[2] { 3u, 4u };
		// 2026-10-22：这条响应原来只回 Level/Exp/TypeLists，没带 LevelLockId。
		// 客户端收到一条 level_lock_id 缺省的 GetMainDataRsp 会把它当成 0（无效档），
		// 于是刚打完 `level 80` 反而把等级锁状态搞乱。
		// 这里补上，让「改等级 → 档位自动跟着变」这条链路自洽。
		getMainDataRsp.LevelLockId = LevelLockConfig.Resolve((int)getMainDataRsp.Level);
		Packet.c.Log("[LevelLock] level 命令后 -> " + LevelLockConfig.Describe((int)getMainDataRsp.Level));
		GetMainDataRsp getMainDataRsp2 = getMainDataRsp;
		session.Send(Packet.FromProto(getMainDataRsp2, CmdId.GetMainDataRsp), Packet.FromProto(new PlayerLevelUpNotify
		{
			NewLevel = getMainDataRsp2.Level
		}, CmdId.PlayerLevelUpNotify));
	}

	public override void Run(Player player, string[] args)
	{
		int level = int.Parse(args[0]);
		player.User.Exp = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateExpForLevel(level).Exp;
		player.User.Save();
	}
}
