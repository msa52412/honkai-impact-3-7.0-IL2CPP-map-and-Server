using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;
using ProtoRewardData = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 登录授勋（七日授勋）的服务端实现，对应协议：
///   GetLoginActivityReq 4192 / GetLoginActivityRsp 4193
///   TakeLoginActivityRewardReq 4194 / TakeLoginActivityRewardRsp 4195
///
/// 数据链路（客户端侧看反编译源码 SeaRelWin8.4.0.proto + LoginRewardSchedule.json）：
///   1. 登录后客户端发 GetLoginActivityReq；
///   2. 服务端每条有效活动回一个 LoginActivityData：
///        id                  = LoginRewardSchedule.ID（7.0 只有 581「七日授勋」没过期）
///        login_days          = 账号累计登录天数（决定第几天可领）
///        has_take_days_reward_list = 已领的天数（普通奖）
///        accept_time         = 活动开始时间（= 建号时间）
///        duration_end_time   = 活动结束时间（配置 endTime，不是「建号 + N 天」）
///   3. 客户端拿 id 查 LoginRewardSchedule，rewardList 的 7 项就是 7 个格子；
///   4. 点领取发 TakeLoginActivityRewardReq{id, take_days, reward_type}，
///      服务端按 rewardList[take_days - 1] 去 RewardData 取奖励、发放、落库，回 RewardData 列表。
///
/// 「累计登录天数」为什么不是 LoginDays 字段自己算：
///   Logins 表每次 PlayerLoginReq 插一行，同一天多次登录会有多行，
///   <see cref="Login.GetUserLoginDays"/> 按本地日期去重后计数，这才是「天数」而不是「次数」。
/// </summary>
internal static class LoginActivityService
{
	/// <summary>0 = 普通奖（rewardList），1 = 额外奖（LoginBonusRewardSchedule）。</summary>
	internal const uint RewardTypeNormal = 0u;

	internal const uint RewardTypeBonus = 1u;

	/// <summary>当前仍然有效的授勋活动。</summary>
	internal static List<LoginRewardScheduleExcel> Active()
	{
		return BaseExcelReader<LoginRewardSchedule, LoginRewardScheduleExcel>.GetInstance().Active();
	}

	internal static LoginRewardScheduleExcel? Config(uint id)
	{
		return BaseExcelReader<LoginRewardSchedule, LoginRewardScheduleExcel>.GetInstance().FromId((int)id);
	}

	/// <summary>
	/// 界面上的「累计登录天数」。
	///
	/// 有真实建号时间（CreatedAt &gt; 0）→ 按 Logins 表去重后的真实登录天数；
	/// 建号时间是 0（v1 迁移账号，界面那句「首次登录」会显示 1970-01-01）→
	/// 天数也按 1970-01-01 至今算，两边才对得上，而不是一个 1970 一个 1 天。
	/// </summary>
	internal static uint LoginDays(uint uid, uint creationTime)
	{
		if (creationTime > 0)
		{
			return Login.GetUserLoginDays(uid);
		}

		return (uint)Math.Max(0, Global.GetUnixInSeconds() / 86400L);
	}

	/// <summary>组一条 LoginActivityData。</summary>
	internal static LoginActivityData Build(uint uid, LoginRewardScheduleExcel cfg, uint creationTime)
	{
		return new LoginActivityData
		{
			Id = (uint)cfg.Id,
			LoginDays = LoginDays(uid, creationTime),
			// accept_time 保持原值（0 = 1970-01-01），界面上的「首次登录 1970」不动。
			AcceptTime = creationTime,
			DurationEndTime = LoginRewardSchedule.EndTimeUnix(cfg),
			HasTakeDaysRewardLists = LoginActivityTake.TakenDays(uid, (uint)cfg.Id, RewardTypeNormal),
			// 7.0 没有任何一条 LoginBonusRewardSchedule 挂在 581 上（表里只有 116/117），
			// 但字段必须给空数组而不是 null —— 客户端会直接遍历它。
			HasTakeDaysBonusRewardLists = LoginActivityTake.TakenDays(uid, (uint)cfg.Id, RewardTypeBonus)
		};
	}

	/// <summary>
	/// 领奖。校验顺序对齐客户端 Retcode 语义：
	/// 活动不存在/过期 → DURATION_END；等级不够 → LEVEL_LACK；天数不够 → LOGIN_DAYS_LACK；
	/// 已领 → HAS_TAKE；奖励配置缺失 → FAIL。
	/// </summary>
	internal static TakeLoginActivityRewardRsp.Retcode Take(
		Player player, uint id, uint takeDays, uint rewardType, out ProtoRewardData reward)
	{
		reward = null!;

		LoginRewardScheduleExcel? cfg = Config(id);
		if (cfg?.RewardList == null || cfg.RewardList.Length == 0)
		{
			Packet.c.Warn($"[LoginActivity] 未知授勋活动 id={id}");
			return TakeLoginActivityRewardRsp.Retcode.Fail;
		}

		if (LoginRewardSchedule.EndTimeUnix(cfg) <= (uint)Global.GetUnixInSeconds())
		{
			return TakeLoginActivityRewardRsp.Retcode.DurationEnd;
		}

		uint level = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance()
			.CalculateLevel(player.User.Exp).Level;
		if (level < (uint)cfg.PlayerLevel)
		{
			return TakeLoginActivityRewardRsp.Retcode.LevelLack;
		}

		if (takeDays < 1u || takeDays > (uint)cfg.RewardList.Length)
		{
			return TakeLoginActivityRewardRsp.Retcode.Fail;
		}

		if (LoginDays(player.User.Uid, player.User.GetCreationTime()) < takeDays)
		{
			return TakeLoginActivityRewardRsp.Retcode.LoginDaysLack;
		}

		if (LoginActivityTake.Has(player.User.Uid, id, takeDays, rewardType))
		{
			return TakeLoginActivityRewardRsp.Retcode.HasTake;
		}

		int rewardId = cfg.RewardList[takeDays - 1];
		RewardDataExcel? rewardCfg = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(rewardId);
		if (rewardCfg == null)
		{
			Packet.c.Warn($"[LoginActivity] RewardData 里找不到 rewardId={rewardId}（活动 {id} 第 {takeDays} 天）");
			return TakeLoginActivityRewardRsp.Retcode.Fail;
		}

		reward = RewardService.Grant(player, rewardCfg);
		LoginActivityTake.Mark(player.User.Uid, id, takeDays, rewardType);
		player.User.Save();
		player.Equipment.Save();

		Packet.c.Log($"[LoginActivity] uid={player.User.Uid} 领 {id} 第 {takeDays} 天奖（rewardId={rewardId}）");
		return TakeLoginActivityRewardRsp.Retcode.Succ;
	}
}
