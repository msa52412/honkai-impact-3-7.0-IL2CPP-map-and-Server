using System;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("abyss", "<sel> [#]", CommandType.Player, new string[] { "temp 400", "bracket [1-9]", "legacy [1-6]", "next [0-1]", "lock [0-2]", "owlevel [11-35]", "owinfo" })]
internal class AbyssCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		Run(session.Player, args);
	}

	public override void Run(Player player, string[] args)
	{
		string text = args[0];
		// args[1] 可能根本不存在（比如只打 `abyss legacy`），
		// 原来的写法直接 args[1] 会越界。
		string raw = (args.Length > 1) ? args[1] : null;
		uint num = 0u;
		if (raw != null && !uint.TryParse(raw, out num))
		{
			num = 0u;
		}
		switch (text)
		{
		case "disturbance":
		case "d":
		case "temp":
			player.User.AbyssDynamicHard = num;
			break;
		case "bracket":
		case "group":
			player.User.AbyssGroupLevel = ((num != 0 && num < 10) ? num : 9u);
			break;
		// 切换「挑战」面板主推哪套深渊，热生效，不用重启服务端。
		//   0 不选  1 普通  2 迪拉克之海  3 量子奇点  4 超弦空间  5 贪婪深渊  6 前哨无尽
		// 客户端下次请求 GetEndlessStatusReq 时就会拿到新值。
		case "legacy":
		case "l":
			AbyssConfig.SetSelected(num);
			Packet.c.Log("[Abyss] " + AbyssConfig.Describe());
			break;
		// 旧深渊下发的分组等级（迪拉克之海 / 量子奇点），热生效。
		//
		// 2026-10-25 17:45 修正：这张表有两条阶梯，段位名按 IconGroup_3..7 排：
		//     Type=2 playerGroup 2 : 21 22 23 24      → 原罪/禁忌/苦痛/红莲
		//     Type=2 playerGroup 3 : 31 32 33 34 35   → 原罪/禁忌/苦痛/红莲/无限 ★
		// 之前默认下发 21，落在 4 档阶梯里，客户端按 5 档查不到就退到第一档，
		// 于是界面永远显示「原罪」。现在默认 35（无限）。
		// 下发表里没有的值会被 OWEndlessGroup.Normalize 夹紧，不会炸客户端。
		//   abyss owlevel 35   无限
		//   abyss owlevel 34   红莲
		//   abyss owlevel 33   苦痛
		//   abyss owlevel 32   禁忌
		//   abyss owlevel 31   原罪
		//   abyss owinfo       打印全部档位 + 对应的扰动幅度
		case "owlevel":
		case "owl":
			AbyssConfig.OwGroupLevel = num;
			Packet.c.Log("[Abyss] OwGroupLevel=" + AbyssConfig.OwGroupLevel);
			break;
		// 打印 OWEndlessGroup 里 Type=2/3 的全部档位，用来核对「下发哪个值会显示成什么」。
		case "owinfo":
			foreach (int t in new int[] { 2, 3 })
			{
				foreach (OWEndlessGroupExcel row in OWEndlessGroup.GetInstance().Ladder(t))
				{
					Packet.c.Log($"[Abyss] Type={row.Type} playerGroup={row.PlayerGroup} "
						+ $"GroupLevel={row.GroupLevel} 扰动={row.GroupHardLevel} "
						+ $"容量={row.MaxCapacity} 晋级名次={row.PromoteRank} icon={row.SelectPrefabPath}");
				}
			}
			break;
		// 控制 GetEndlessStatusRsp.NextStatusLists 要不要挂其它深渊（默认开）。
		//   abyss next 1  开（挑战面板尝试显示多套深渊）
		//   abyss next 0  关（回到 v2 原状，只有 CurStatus 一张卡）
		case "next":
			AbyssConfig.ShowOtherEndless = (num != 0);
			Packet.c.Log("[Abyss] ShowOtherEndless=" + AbyssConfig.ShowOtherEndless);
			break;
		// 等级锁（高级区 / 终极区）热切换。
		//   lock 0  自动（按等级推，88 级 → 2 终极区）
		//   lock 1  强制高级区（MaxLevel 80，蓝框）
		//   lock 2  强制终极区（MaxLevel 88，红框）
		// 改完客户端要重新请求一次 GetMainDataReq 才看得到 —— 回主界面再进即可。
		case "lock":
			if (num > 2)
			{
				throw new ArgumentException("lock 只接受 0/1/2");
			}
			LevelLockConfig.ForcedLockId = num;
			int lv = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance()
				.CalculateLevel(player.User.Exp).Level;
			Packet.c.Log("[LevelLock] " + LevelLockConfig.Describe(lv));
			break;
		default:
			throw new ArgumentException("Unrecognized action");
		}
		player.User.Save();
	}
}
