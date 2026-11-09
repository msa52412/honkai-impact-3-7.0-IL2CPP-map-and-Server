using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Common.Utils.ExcelReader;

/// <summary>
/// 登录授勋活动表。服务端只用它两件事：取奖励清单、判断活动有没有过期。
/// </summary>
public class LoginRewardSchedule : BaseExcelReader<LoginRewardSchedule, LoginRewardScheduleExcel>
{
	public override string FileName => "LoginRewardSchedule.json";

	public LoginRewardScheduleExcel? FromId(int id)
	{
		return base.All.FirstOrDefault(x => x.Id == id);
	}

	/// <summary>活动结束时间（Unix 秒）。配置里是本地时间，解析失败按「永不过期」处理。</summary>
	public static uint EndTimeUnix(LoginRewardScheduleExcel cfg)
	{
		if (cfg?.EndTime == null)
		{
			return uint.MaxValue;
		}

		if (!DateTime.TryParse(cfg.EndTime, CultureInfo.InvariantCulture,
				DateTimeStyles.AssumeLocal, out DateTime end))
		{
			return uint.MaxValue;
		}

		return (uint)new DateTimeOffset(end).ToUnixTimeSeconds();
	}

	/// <summary>
	/// 当前仍然有效的登录授勋活动（结束时间没到）。7.0 的配置里只有 581 命中。
	/// </summary>
	public List<LoginRewardScheduleExcel> Active()
	{
		uint now = (uint)Global.GetUnixInSeconds();
		return base.All
			.Where(x => x.RewardList != null && x.RewardList.Length > 0)
			.Where(x => EndTimeUnix(x) > now)
			.OrderBy(x => x.Id)
			.ToList();
	}
}
