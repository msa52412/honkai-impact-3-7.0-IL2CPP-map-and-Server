using System;
using System.Collections.Generic;
using System.Linq;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Game;

/// <summary>
/// A numeric SiteID bucket is only a selector, not a complete schedule.
/// Later schedules cross bucket boundaries (623 contains two heads and the tail
/// of 6229). Resolve four unique map nodes through their prerequisite links.
/// </summary>
internal static class UltraEndlessSites
{
    public static bool TryResolveGroup(int group, UltraEndlessSiteExcel[] all,
        out UltraEndlessSiteExcel[] sites)
    {
        sites = Array.Empty<UltraEndlessSiteExcel>();
        if (group <= 0) return false;
        IEnumerable<UltraEndlessSiteExcel> Children(int parent, string node) =>
            all.Where(s => s.SiteNodeName == node && (s.PreSiteList?.Contains(parent) ?? false));

        // Keep table order deterministic when a bucket contains multiple heads.
        foreach (var head in all.Where(s => s.SiteId / 10 == group &&
                     s.SiteNodeName == "Area1" && (s.PreSiteList == null || s.PreSiteList.Length == 0)))
        foreach (var area2 in Children(head.SiteId, "Area2"))
        foreach (var area3 in Children(area2.SiteId, "Area3"))
        foreach (var area4 in Children(head.SiteId, "Area4"))
        {
            var candidate = new[] { head, area2, area3, area4 };
            var ids = candidate.Select(s => s.SiteId).ToHashSet();
            if (ids.Count == 4 && candidate.All(s =>
                    s.PreSiteList == null || s.PreSiteList.All(ids.Contains)))
            {
                sites = candidate;
                return true;
            }
        }
        return false;
    }

    public static UltraEndlessSiteExcel[] ResolveGroups(int[] groups, UltraEndlessSiteExcel[] all)
    {
        var selected = new List<UltraEndlessSiteExcel>();
        foreach (int group in groups.Distinct())
        {
            if (!TryResolveGroup(group, all, out var chain) || !UltraEndlessValidation.Playable(chain))
                throw new ArgumentException($"超弦配置 {group} 找不到完整的 Area1/2/3/4 前置链。");
            selected.AddRange(chain);
        }
        return selected.GroupBy(s => s.SiteId).Select(g => g.First()).ToArray();
    }

    public static UltraEndlessSiteExcel[] Active()
    {
        var all = BaseExcelReader<UltraEndlessSite, UltraEndlessSiteExcel>.GetInstance().All;
        try
        {
            var sites = ResolveGroups(UltraEndlessRank.ActiveSiteGroups(), all);
            if (sites.Length > 0) return sites;
        }
        catch (ArgumentException error)
        {
            Packet.c.Warn("[Ultra] " + error.Message + " 使用默认完整关卡链。");
        }
        return ResolveGroups(UltraEndlessRank.DefaultSiteGroups, all);
    }
}
