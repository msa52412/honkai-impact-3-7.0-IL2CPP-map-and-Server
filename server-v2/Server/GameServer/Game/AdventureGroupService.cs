using System;
using System.Collections.Generic;
using System.IO;
using Common;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 家园小队（Adventure Group）等级 —— 舰桥 → 家园 → 家园小队 用的那一套。
///
/// 与 <see cref="SanctuaryService"/> 的区别（别再搞混）
/// ------------------------------------------------
///   Sanctuary（cmd 4305~4314）是**旧活动界面**，家园小队主页根本不读它；
///   正常入口走 Adventure 系列页面，等级来自 **3900/3901 GetAdventureGroup**。
///   所以只改 Sanctuary 存档或配置，家园小队页面不会变。
///
/// 7.0 协议字段（关键，别按旧模型猜）
/// --------------------------------
/// 服务端生成的 GetAdventureGroupRsp.cs 来自旧协议，把 **tag 2 命名为 Level**；
/// 但 7.0 客户端实际读取的是：
///
///   tag 2  → old_level               旧等级（兼容保留，客户端只用它算升级弹窗/经验）
///   tag 18 → level                   当前等级 ← 界面上显示的就是这个
///   tag 19 → badge                   徽章
///   tag 20 → extra_badge_cost_num    额外徽章消耗数
///
/// 依据：rea\_rea\src\proto.cs 里 7.0 原生结构体
///   old_level / old_badge / … / level / badge / extra_badge_cost_num
/// 旧模型缺 18/19/20 三个 tag，所以只能在响应上用 protobuf-net 的 IExtensible 补写，
/// 不去手改生成的 Common 协议文件（改了下一次重新生成又丢）。
///
/// 配置
/// ----
/// 文件：run\adventure.json，**每次请求重新读**，改完不用重启、不用重编译。
///
///   {
///     "player_levels": { "1002": 45 },
///     "min_level": 1,
///     "max_level": 45
///   }
///
/// 未配置的账号取 1 级；配置值会被夹到 [min_level, max_level]。
/// 注意：AdventureData.json（等级属性表）只到 **38 级**，配到 39~45 时界面数字照常显示，
/// 但客户端按等级查属性会查空（属性栏可能显示占位值）。见 Load() 里的告警。
/// </summary>
internal static class AdventureGroupService
{
	private const string FILE_NAME = "adventure.json";

	/// <summary>未配置账号的默认等级。</summary>
	private const uint DefaultLevel = 1u;

	/// <summary>AdventureData.json 里 AdventureLevel 的最大值（实测 38 行，1~38 连续）。</summary>
	private const uint TableMaxLevel = 38u;

	private class Dto
	{
		[JsonProperty("player_levels")]
		public Dictionary<string, uint>? PlayerLevels { get; set; }

		[JsonProperty("min_level")]
		public uint MinLevel { get; set; }

		[JsonProperty("max_level")]
		public uint MaxLevel { get; set; }
	}

	private static Dto _dto = new Dto();

	private static DateTime _stamp = DateTime.MinValue;

	/// <summary>取某个账号的家园小队等级（已夹紧到配置区间）。</summary>
	public static uint LevelFor(uint uid)
	{
		Dto dto = Load();
		uint level = DefaultLevel;
		if (dto.PlayerLevels != null)
		{
			// 键统一按字符串存，uid 用十进制无符号写法，和手写 json 的习惯一致。
			if (dto.PlayerLevels.TryGetValue(uid.ToString(), out uint configured))
			{
				level = configured;
			}
		}

		uint min = dto.MinLevel == 0u ? 1u : dto.MinLevel;
		uint max = dto.MaxLevel == 0u ? 45u : dto.MaxLevel;
		if (max < min)
		{
			max = min;
		}
		if (level < min)
		{
			level = min;
		}
		if (level > max)
		{
			level = max;
		}
		return level;
	}

	private static Dto Load()
	{
		try
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), FILE_NAME);
			if (!File.Exists(path))
			{
				// 没有配置文件 = 所有人 1 级，和客户端默认表现一致，不报错。
				_dto = new Dto();
				_stamp = DateTime.MinValue;
				return _dto;
			}

			DateTime stamp = File.GetLastWriteTimeUtc(path);
			if (stamp == _stamp)
			{
				return _dto;
			}

			Dto? dto = JsonConvert.DeserializeObject<Dto>(File.ReadAllText(path));
			_dto = dto ?? new Dto();
			_stamp = stamp;

			int count = _dto.PlayerLevels?.Count ?? 0;
			Packet.c.Log("[Adventure] 读取 " + FILE_NAME + " -> 配置账号数=" + count
				+ $" min={(_dto.MinLevel == 0u ? 1u : _dto.MinLevel)}"
				+ $" max={(_dto.MaxLevel == 0u ? 45u : _dto.MaxLevel)}"
				+ (count > 0 ? " 明细=" + Describe(_dto) : string.Empty));

			// 超过属性表上限不拦，但要说清楚：界面数字会显示，属性查表可能空。
			if (_dto.PlayerLevels != null)
			{
				foreach (KeyValuePair<string, uint> kv in _dto.PlayerLevels)
				{
					if (kv.Value > TableMaxLevel)
					{
						Packet.c.Warn($"[Adventure] uid={kv.Key} 配置等级 {kv.Value} 超过"
							+ $" AdventureData 表上限 {TableMaxLevel}，属性加成会查空，仅显示等级");
					}
				}
			}
		}
		catch (Exception ex)
		{
			// 配置写坏了就当没配，绝不因为一个 json 让服务端抛异常。
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[Adventure] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}

	private static string Describe(Dto dto)
	{
		if (dto.PlayerLevels == null)
		{
			return "{}";
		}
		List<string> parts = new List<string>();
		foreach (KeyValuePair<string, uint> kv in dto.PlayerLevels)
		{
			parts.Add(kv.Key + "=" + kv.Value);
		}
		return "{" + string.Join(",", parts) + "}";
	}
}
