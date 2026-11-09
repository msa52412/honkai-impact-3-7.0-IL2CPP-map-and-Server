using System;
using System.IO;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// 收藏（图鉴）补洞用的运行期开关。
///
/// CG 这一栏的解锁在客户端分三种类型（反编译 CGUnlockType：Level / Material / OWStory），
/// 服务端这份 6.6 配置只覆盖到 CgID 50126，7.0 客户端表更靠后，靠区间把 id 空间补齐。
/// 区间写在运行目录 collection_cg.txt 里，格式 `40001-60000`，改完刷新收藏页即生效，不用重启。
/// </summary>
internal static class CollectionPad
{
	/// <summary>读 collection_cg.txt 里的 CG id 区间；没配就返回空数组。</summary>
	internal static uint[] CgRange()
	{
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, "collection_cg.txt");
			if (!File.Exists(path))
			{
				return new uint[0];
			}

			string[] parts = File.ReadAllText(path).Trim().Split('-');
			if (parts.Length != 2)
			{
				return new uint[0];
			}

			if (!uint.TryParse(parts[0].Trim(), out uint from) || !uint.TryParse(parts[1].Trim(), out uint to))
			{
				return new uint[0];
			}

			if (to < from || to - from > 200000u)
			{
				return new uint[0];
			}

			uint[] range = new uint[to - from + 1];
			for (int i = 0; i < range.Length; i++)
			{
				range[i] = from + (uint)i;
			}

			return range;
		}
		catch
		{
			return new uint[0];
		}
	}

	/// <summary>运行目录下某个开关文件里写的是不是 0（文件不存在时返回 def）。</summary>
	internal static bool Switch(string fileName, bool def)
	{
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, fileName);
			if (File.Exists(path))
			{
				return File.ReadAllText(path).Trim() != "0";
			}
		}
		catch
		{
			// 读不到就用默认值
		}

		return def;
	}
}
