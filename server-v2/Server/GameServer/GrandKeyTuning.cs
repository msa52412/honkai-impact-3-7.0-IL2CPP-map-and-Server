using System;
using System.IO;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer;

/// <summary>
/// 神之键（GrandKey）运行期调参，**改文件即生效，不用重启服务端**。
/// 写法照抄 <see cref="SanctuaryTuning"/>：读 run\grandkey.json，按文件 mtime 重载。
///
/// 为什么要这个开关
/// ----------------
/// 官方规则（用户口述 + GrandKey.json 实证）：
///   「获得对应的武器即可解锁神之键，升级神之键提升全局属性」
/// GrandKey.json 里每把神之键绑定一组武器：
///     201 -> [10158]                  202 -> [10163, 10207, 10199] + 副武器 [0, 24014, 0]
///     203 -> [20000, 10169]           204 -> [10180, 10222, 10267]
///     205 -> [10211, 10212]           206 -> [10234, 10235]
///     207 -> [10247, 10248]           208 -> [10261]
/// 但本服现有账号（uid 1002）仓库里 414 种武器**全是 2★/3★/4★（20xxx/24xxx）**，
/// 一把 10xxx 的 5★ 都没有，所以纯按武器解锁只会亮 202 一把。
/// 因此默认 unlock_all = true，让系统先能用；想还原官方规则就改成 false。
///
/// 文件：run\grandkey.json（不存在就用下面代码里的默认值，服务端照常启动）
/// {
///   "unlock_all": true,        // true = 8 把全部解锁（默认）
///   "unlock_by_weapon": true,  // true = 持有对应武器也解锁（unlock_all=false 时才有意义）
///   "default_level": 1,        // 新解锁的神之键初始等级
///   "force_max_level": true,   // true = 每次同步都把等级拉到该把神之键的配置上限（默认）
///   "force_level": 0,          // >0 = 拉到指定等级（force_max_level=false 时才有意义）
///   "max_level": 0,            // 0 = 用配置表（GrandKeyLevel / maxGrandKeyLv）
///   "check_materials": false   // true = 升级时校验 upgradeMaterial（默认关，避免背包被扣）
/// }
///
/// 关于「满级」的两层上限
/// ---------------------
/// GrandKey.json 的 maxGrandKeyLv 是**每把各自的**上限：
///   201/202/203/205 -> 10 级     204/206/207/208 -> 1 级
/// 所以默认 force_max_level 拉满是 10/10/10/1/10/1/1/1，这是官方设定。
/// 想让 8 把全部顶到 10 级，就把 "max_level": 10 打开（会覆盖每把自己的上限）。
/// </summary>
internal static class GrandKeyTuning
{
	private const string FILE_NAME = "grandkey.json";

	private class Dto
	{
		/// <summary>true = 不校验武器，8 把神之键全部解锁。</summary>
		[JsonProperty("unlock_all")]
		public bool UnlockAll { get; set; } = true;

		/// <summary>true = 持有 GrandKey.json 里绑定的武器即解锁。</summary>
		[JsonProperty("unlock_by_weapon")]
		public bool UnlockByWeapon { get; set; } = true;

		/// <summary>新解锁时的初始等级。0 视为 1。</summary>
		[JsonProperty("default_level")]
		public uint DefaultLevel { get; set; }

		/// <summary>true = 每次同步都把等级拉到该把神之键的配置上限。</summary>
		[JsonProperty("force_max_level")]
		public bool ForceMaxLevel { get; set; } = true;

		/// <summary>&gt;0 = 拉到指定等级（会被上限夹住）。</summary>
		[JsonProperty("force_level")]
		public uint ForceLevel { get; set; }

		/// <summary>等级上限覆盖。0 = 用配置表。</summary>
		[JsonProperty("max_level")]
		public uint MaxLevel { get; set; }

		/// <summary>升级时是否校验并扣除 upgradeMaterial。</summary>
		[JsonProperty("check_materials")]
		public bool CheckMaterials { get; set; }

		/// <summary>
		/// true = 每次同步都把技能设成「当前超限档位下可用的同步技能」并把到期时间推到很远的将来，
		/// 这样界面上「技能同步」是满的。false = 只保证 SkillId 是真实技能 ID，同步状态交给客户端。
		/// </summary>
		[JsonProperty("auto_sync_skill")]
		public bool AutoSyncSkill { get; set; } = true;
	}

	private static DateTime _stamp = DateTime.MinValue;

	private static Dto _dto = new Dto();

	public static bool UnlockAll => Load().UnlockAll;

	public static bool UnlockByWeapon => Load().UnlockByWeapon;

	public static bool CheckMaterials => Load().CheckMaterials;

	public static bool ForceMaxLevel => Load().ForceMaxLevel;

	public static uint ForceLevel => Load().ForceLevel;

	public static bool AutoSyncSkill => Load().AutoSyncSkill;

	public static uint DefaultLevel
	{
		get
		{
			uint v = Load().DefaultLevel;
			return v == 0u ? 1u : v;
		}
	}

	public static uint MaxLevelOverride => Load().MaxLevel;

	private static Dto Load()
	{
		try
		{
			string path = Path.Combine(Directory.GetCurrentDirectory(), FILE_NAME);
			if (!File.Exists(path))
			{
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

			Packet.c.Log("[GrandKey] 读取 " + FILE_NAME + " -> "
				+ "unlock_all=" + _dto.UnlockAll
				+ " unlock_by_weapon=" + _dto.UnlockByWeapon
				+ " default_level=" + DefaultLevel
				+ " force_max_level=" + _dto.ForceMaxLevel
				+ " force_level=" + (_dto.ForceLevel == 0u ? "(不指定)" : _dto.ForceLevel.ToString())
				+ " max_level=" + (MaxLevelOverride == 0u ? "(用配置表)" : MaxLevelOverride.ToString())
				+ " check_materials=" + _dto.CheckMaterials
				+ " auto_sync_skill=" + _dto.AutoSyncSkill);
		}
		catch (Exception ex)
		{
			_dto = new Dto();
			_stamp = DateTime.MinValue;
			Packet.c.Warn("[GrandKey] 读取 " + FILE_NAME + " 失败，忽略：" + ex.Message);
		}
		return _dto;
	}
}
