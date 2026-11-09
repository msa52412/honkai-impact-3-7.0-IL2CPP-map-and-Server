using System;
using Common.Utils.ExcelReader;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer;

/// <summary>
/// 等级锁（高级区 / 终极区）运行期状态。
///
/// 为什么要有这个东西
/// ------------------
/// 崩坏3 从 70 级往后分两段：
///   LockID=1「高级区」  MaxLevel=80，蓝框，深渊按高级区档位算
///   LockID=2「终极区」  MaxLevel=88，红框，深渊是超弦空间的完整档位
/// 从 1 到 2 不是自动的，玩家得在 80 级打满突破经验、做完 45 条突破任务，
/// 点「突破等级锁」发 SetLevelLockReq(level_lock_id=2) 才会升上去。
///
/// v2 原来的问题
/// -------------
/// GetMainDataReqHandler 里是硬编码：
///     getMainDataRsp.LevelLockId = 1u;
/// 而存档里玩家是 88 级（Exp=300257）。
/// 于是客户端收到的状态是「你 88 级，但你还在高级区（封顶 80）」——
/// 这是自相矛盾的，客户端没法判断该按哪一档渲染深渊。
///
/// 而且 SetLevelLockReq(1488) 压根没实现，日志里 0 次出现，
/// 所以就算客户端想走「突破」流程也没人能应答。
///
/// 默认值取 Auto
/// -------------
/// Auto 的规则就是真实游戏的规则：等级超过当前档上限就自动认为已突破。
/// 88 级 → 解析成 2（终极区），和等级自洽。
/// 想强制回到高级区做对照实验，游戏内聊天打 `lock 1`。
/// </summary>
internal static class LevelLockConfig
{
	/// <summary>
	/// 0 = 自动（按玩家等级推），1/2/... = 强制指定。
	/// </summary>
	public static uint ForcedLockId = 0u;

	/// <summary>
	/// 算出当前该下发的 level_lock_id。
	///
	/// Auto 逻辑：从最低档开始往上升，只要玩家等级已经超过当前档的 MaxLevel，
	/// 就升到下一档。88 级会一路升到 2（终极区）。
	/// </summary>
	public static uint Resolve(int playerLevel)
	{
		PlayerLevelLock reader = BaseExcelReader<PlayerLevelLock, PlayerLevelLockExcel>.GetInstance();

		if (ForcedLockId != 0u)
		{
			return ForcedLockId;
		}

		uint cur = reader.MinLockId();
		// 防御：万一表里只有 1 行、或 Next 成环，最多循环 8 次。
		for (int guard = 0; guard < 8; guard++)
		{
			PlayerLevelLockExcel? row = reader.Get(cur);
			if (row == null)
			{
				break;
			}
			if (playerLevel <= row.MaxLevel)
			{
				break;
			}
			PlayerLevelLockExcel? next = reader.Next(cur);
			if (next == null)
			{
				break;
			}
			cur = next.LockID;
		}
		return cur;
	}

	/// <summary>
	/// 处理 SetLevelLockReq 的校验。
	///
	/// 三个 Retcode 的语义（从协议枚举反推）：
	///   Succ                —— 允许切换
	///   Fail                —— 目标档不存在 / 参数非法
	///   NotMeetMaxLevel     —— 还没到当前档的等级上限，不能突破
	///   NotNextId           —— 目标不是「下一档」（不能跳档，也不能往回退）
	///
	/// 注意：真实游戏里还有「突破经验 MaxLayUpExp」和 45 条突破任务两个前置。
	/// 私服不做这层校验 —— 否则玩家得先真去打 45 个任务才能试深渊。
	/// 想恢复严格模式，把 CheckMission 打开即可（默认关）。
	/// </summary>
	public static bool CheckMission = false;

	public static SetLevelLockRsp.Retcode Validate(uint targetId, int playerLevel, out uint currentId)
	{
		PlayerLevelLock reader = BaseExcelReader<PlayerLevelLock, PlayerLevelLockExcel>.GetInstance();
		currentId = Resolve(playerLevel);

		PlayerLevelLockExcel? target = reader.Get(targetId);
		if (target == null)
		{
			// 目标档不存在
			return SetLevelLockRsp.Retcode.Fail;
		}

		if (targetId == currentId)
		{
			// 已经是这档了，幂等返回成功，别让客户端卡住
			return SetLevelLockRsp.Retcode.Succ;
		}

		// 只允许往上升一档
		PlayerLevelLockExcel? next = reader.Next(currentId);
		if (next == null || next.LockID != targetId)
		{
			return SetLevelLockRsp.Retcode.NotNextId;
		}

		// 等级必须达到当前档的上限
		PlayerLevelLockExcel? curRow = reader.Get(currentId);
		if (curRow != null && playerLevel < curRow.MaxLevel)
		{
			return SetLevelLockRsp.Retcode.NotMeetMaxLevel;
		}

		if (CheckMission && curRow != null && curRow.MissionList.Count > 0)
		{
			// 严格模式下要求突破任务已完成 —— 私服目前没有任务系统，
			// 打开这个开关等价于「永远不能突破」，所以默认关。
			return SetLevelLockRsp.Retcode.Fail;
		}

		return SetLevelLockRsp.Retcode.Succ;
	}

	public static string Describe(int playerLevel)
	{
		PlayerLevelLock reader = BaseExcelReader<PlayerLevelLock, PlayerLevelLockExcel>.GetInstance();
		uint cur = Resolve(playerLevel);
		PlayerLevelLockExcel? row = reader.Get(cur);
		string frame = row?.LevelGiftPackFrame ?? "?";
		string zone = cur switch
		{
			1u => "高级区",
			2u => "终极区",
			_ => "未知区"
		};
		string forced = (ForcedLockId == 0u) ? "auto" : ForcedLockId.ToString();
		return $"LevelLockId={cur}({zone}, MaxLevel={row?.MaxLevel} {frame}) forced={forced} playerLevel={playerLevel}";
	}
}
