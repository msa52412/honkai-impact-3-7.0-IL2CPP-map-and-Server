using System.Collections.Generic;
using Newtonsoft.Json;

namespace Common.Utils.ExcelReader;

/// <summary>
/// PlayerLevelLock.json 的一行 —— 也就是一档「等级锁」。
///
/// 这张表全表只有 2 行（7.0 版本）：
///   LockID=1  MaxLevel=80  LbFrameBlue  → 高级区（未突破）
///   LockID=2  MaxLevel=88  LbFrameRed   → 终极区（已突破）
///
/// 客户端拿 GetMainDataRsp.level_lock_id 去查这张表，决定：
///   1. 主界面头像框是蓝的（未突破）还是红的（已突破）
///   2. 等级条上限画到 80 还是 88
///   3. 「突破等级锁」那个引导/弹窗要不要出
///   4. 挑战面板里深渊的档位（高级区 / 终极区）
/// </summary>
public class PlayerLevelLockExcel
{
	/// <summary>这一档的等级上限。1 → 80，2 → 88。</summary>
	[JsonProperty("MaxLevel")]
	public int MaxLevel { get; set; }

	/// <summary>从这一档突破到下一档，需要额外攒多少「突破经验」。</summary>
	[JsonProperty("MaxLayUpExp")]
	public int MaxLayUpExp { get; set; }

	/// <summary>突破任务列表（LockID=2 那行有 45 条，LockID=1 是空的）。</summary>
	[JsonProperty("MissionList")]
	public List<int> MissionList { get; set; } = new List<int>();

	/// <summary>突破事件 ID（40388 / 40480），大概是突破弹窗用的事件。</summary>
	[JsonProperty("EventID")]
	public int EventID { get; set; }

	[JsonProperty("PlayerLockHintText")]
	public object PlayerLockHintText { get; set; }

	[JsonProperty("LevelGiftPackBG")]
	public string LevelGiftPackBG { get; set; }

	/// <summary>头像框资源名：LbFrameBlue / LbFrameRed。</summary>
	[JsonProperty("LevelGiftPackFrame")]
	public string LevelGiftPackFrame { get; set; }

	/// <summary>突破后播的教程视频 ID（LockID=2 是 20）。</summary>
	[JsonProperty("TutorialVideoID")]
	public int TutorialVideoID { get; set; }

	/// <summary>这一档的 ID，也就是协议里的 level_lock_id。</summary>
	[JsonProperty("LockID")]
	public uint LockID { get; set; }
}
