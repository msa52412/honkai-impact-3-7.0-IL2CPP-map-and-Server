using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer;

/// <summary>
/// 开放世界「可传送点」补齐。
///
/// `GetOpenworldMapRsp.spawn_point` 是一个 "出生点名:areaId" 的逗号列表，
/// 客户端地图页上的传送点就是它。这个字符串平时由客户端
/// `ReportOpenworldSpawnPointReq` 上报，但**只有它自己解锁过的才会报**——
/// 没去过的区域永远进不去，表现就是「部分区域没开放」。
///
/// 后崩坏书 2 的 map 601 一开始整个字符串是空的，实测补齐后
/// 从 'TPPoint (1):6012,MapTransition_Front:6010' 变成含 6011/6012 全部切换点。
///
/// 补法：读 OpenWorldArea.switchPoint（"目标areaId:出生点名"），
/// 反写成 "出生点名:目标areaId"，只保留同一张地图里的目标区域。
/// </summary>
public static class OwSpawnPoints
{
	/// <summary>
	/// 把配置里的切换点并进 scheme.SpawnPoint（去重、保持已有顺序）。
	/// 返回是否有改动。
	/// </summary>
	public static bool Fill(OpenWorldScheme scheme)
	{
		if (!OwMapTuning.FillSpawnPoints || scheme == null)
		{
			return false;
		}

		List<OpenWorldAreaDataExcel> areas = OpenWorldAreaData.GetInstance().OfMap(scheme.MapId);
		if (areas.Count == 0)
		{
			return false;
		}

		HashSet<uint> areaIds = new HashSet<uint>();
		foreach (OpenWorldAreaDataExcel a in areas)
		{
			areaIds.Add(a.AreaId);
		}

		List<string> entries = new List<string>();
		HashSet<string> seen = new HashSet<string>();
		if (!string.IsNullOrEmpty(scheme.SpawnPoint))
		{
			foreach (string part in scheme.SpawnPoint.Split(','))
			{
				string p = part.Trim();
				if (p.Length > 0 && seen.Add(p))
				{
					entries.Add(p);
				}
			}
		}
		int before = entries.Count;

		foreach (OpenWorldAreaDataExcel area in areas)
		{
			if (string.IsNullOrEmpty(area.SwitchPoint))
			{
				continue;
			}
			foreach (string part in area.SwitchPoint.Split(','))
			{
				string p = part.Trim();
				int idx = p.IndexOf(':');
				if (idx <= 0 || idx >= p.Length - 1)
				{
					continue;
				}
				if (!uint.TryParse(p.Substring(0, idx), out uint targetArea) || !areaIds.Contains(targetArea))
				{
					continue;
				}
				string name = p.Substring(idx + 1);
				string entry = name + ":" + targetArea;
				if (seen.Add(entry))
				{
					entries.Add(entry);
				}
			}
		}

		if (entries.Count == before)
		{
			return false;
		}

		scheme.SpawnPoint = string.Join(",", entries);
		scheme.Save();
		Packet.c.Log($"[OW] mapId={scheme.MapId} 补齐传送点 {before} -> {entries.Count}：{scheme.SpawnPoint}");
		return true;
	}
}
