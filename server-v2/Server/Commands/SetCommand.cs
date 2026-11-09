using System;
using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

/// <summary>
/// 直接把玩家某项数值改成指定值（不是累加）。
///
/// 为什么要这个命令：`give gold` 只能加金币，水晶/体力根本没有入口；
/// 改 sqlite 又必须先停服，否则内存里的旧值会覆盖回去。
///
/// 用法：
///     set stamina 167
///     set hcoin 5200
///     set scoin 5200000
///     set exp 5000
/// </summary>
[CommandHandler("set", "<stamina|hcoin|scoin|exp> <数值>", CommandType.Player,
	new string[] { "stamina 167", "hcoin 5200", "scoin 5200000", "exp 5000" })]
internal class SetCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		if (args.Length < 2)
		{
			throw new ArgumentException("用法：set <stamina|hcoin|scoin|exp> <数值>");
		}

		string field = args[0].ToLowerInvariant();
		long value = long.Parse(args[1]);
		Player player = session.Player;

		switch (field)
		{
		case "stamina":
			player.User.Stamina = (int)value;
			break;
		case "hcoin":
			player.User.Hcoin = (int)value;
			break;
		case "scoin":
		case "gold":
		{
			// 金币是材料 100，AddMaterial 只做加减，所以先算出差额
			uint current = player.Equipment.MaterialList
				.Where(m => m.Id == 100)
				.Select(m => m.Num)
				.FirstOrDefault();
			player.Equipment.AddMaterial(100, (int)(value - current));
			player.Equipment.Save();
			break;
		}
		case "exp":
			player.User.Exp = (int)value;
			break;
		default:
			throw new ArgumentException("不认识的字段：" + field);
		}

		player.User.Save();
		c.Log("已设置 " + field + " = " + value);

		// 立刻推一份新的主数据，客户端不用重登就能看到
		session.ProcessPacket(Packet.FromProto(new GetMainDataReq(), CmdId.GetMainDataReq));
	}
}
