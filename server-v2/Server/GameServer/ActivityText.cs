using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 把 <see cref="GetConfigRsp"/> 里「本该是文本、却塞了配置 key」的字段换成实际显示文本。
///
/// 为什么需要这一步
/// ────────────────
/// <c>BulletinActivityConfig.title_name</c>(12) / <c>description</c>(16) 在 proto 里是
/// <b>string</b>，不是 TextID 结构。TextID 字段客户端会自己拿 hash 去查 TextMap 做本地化，
/// 而 string 字段拿到什么就原样画到 Text 组件上。
/// 配置表（BulletinActivity / 活动排期表）里这两个字段存的却是 key（DLC_Boxgacha_Title、
/// 3.5SPhuntinggacha_Des …），老服务端把这串 key 直接塞进 proto 下发，
/// 于是界面上显示的就是 `DLC_Boxgacha_Title` 这种「初始化字符」。
/// 爬塔主界面 →「补给」抽卡按钮打开的 <c>MonoBulletinEventGachaPanelV2</c> 读的正是这一条，
/// 所以那个面板整页都是 key。
///
/// 怎么换
/// ──────
/// 客户端 TextID(string) 的构造函数是标准 BKDR：
/// <code>h = 0; for c in s: h = h * 31 + ord(c)   // int32 回绕</code>
/// TextMap 里是 hash(int) → Text，所以 key → hash → 原文。
/// 这张 key→文本 的表由 <c>tools/gen_activity_text.py</c> 离线算好写进
/// <see cref="ActivityTextTable"/>，运行期不做 43MB JSON 解析。
///
/// 描述缺失时用标题兜底 —— 配置本身就是这样写的（5.4dlcgacha_Des 的原文
/// 和它的标题一模一样），所以兜底不会显得突兀。
/// </summary>
internal static class ActivityText
{
	/// <summary>
	/// 把一个字段值解析成可显示文本。
	/// </summary>
	/// <returns>解析出的文本；不像 key 或者表里没有时返回 null。</returns>
	internal static string? Resolve(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return null;
		}
		return ActivityTextTable.Text.TryGetValue(value.Trim(), out string? text) ? text : null;
	}

	/// <summary>就地修正一份 GetConfigRsp 里所有活动条目的标题 / 描述。</summary>
	/// <param name="rsp">待下发的配置包</param>
	/// <param name="log">是否把替换结果打进日志（默认打，方便对照客户端界面）</param>
	internal static void Apply(GetConfigRsp rsp, bool log = true)
	{
		int fixedCount = 0;

		foreach (BulletinActivityConfig activity in rsp.BulletinActivityLists)
		{
			// 标题：查得到就换，查不到保持原样（宁可显示 key 也不要显示空）。
			string title = activity.TitleName;
			string? titleText = Resolve(title);
			if (titleText != null)
			{
				activity.TitleName = titleText;
				fixedCount++;
			}

			// 描述：优先查表；查不到（TextMap 里确实没有这条 key）就用标题兜底。
			string desc = activity.Description;
			string? descText = Resolve(desc);
			if (descText != null)
			{
				activity.Description = descText;
				if (descText != desc)
				{
					fixedCount++;
				}
			}
			else if (titleText != null && desc != titleText)
			{
				activity.Description = titleText;
				fixedCount++;
			}
		}

		// 55 = armada_custom_content，舰团活动按钮的文案，同样是 key。
		string armada = rsp.ArmadaCustomContent;
		string? armadaText = Resolve(armada);
		if (armadaText != null)
		{
			rsp.ArmadaCustomContent = armadaText;
			fixedCount++;
		}

		if (log && fixedCount > 0)
		{
			Packet.c.Log($"[Config] 活动文案 key 已替换为实际文本：{fixedCount} 处 / "
				+ $"{rsp.BulletinActivityLists.Count} 条活动");
		}

		// 还有 key 没换上时点名，方便回头往 ActivityTextTable 里补。
		foreach (BulletinActivityConfig activity in rsp.BulletinActivityLists)
		{
			if (LooksLikeKey(activity.TitleName) || LooksLikeKey(activity.Description))
			{
				Packet.c.Warn($"[Config] 活动 {activity.ActivityId} 仍有未解析的 key："
					+ $"title={activity.TitleName} desc={activity.Description}");
			}
		}
	}

	/// <summary>粗判一个字符串像不像配置 key（纯 ASCII、无空格、无路径分隔符）。</summary>
	private static bool LooksLikeKey(string? value)
	{
		if (string.IsNullOrEmpty(value) || value.Contains('/') || value.Contains(' ')
			|| value.Contains('<'))
		{
			return false;
		}
		foreach (char c in value)
		{
			if (c >= 128)
			{
				return false;
			}
		}
		return true;
	}
}
