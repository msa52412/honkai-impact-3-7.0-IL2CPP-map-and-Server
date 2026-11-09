using System;
using System.Collections.Generic;
using System.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    private sealed record ExclusiveEnhancement(uint Role, uint Level, uint Normal, uint Enhanced);

    private static readonly Lazy<Dictionary<uint, ExclusiveEnhancement[]>> ExclusiveEnhancements = new(() =>
    {
        var buffs = PlayableBuffRows().Where(row => (uint)row["BuffQuality"] == 4)
            .ToDictionary(row => (uint)row["BuffID"]);
        var roles = Table("GodWarRelationData").Where(row => (uint)row["GodWarID"] == 1 && (uint)row["RoleType"] == 2)
            .GroupBy(row => (uint)row["AvatarID"]).ToDictionary(group => group.Key, group => (uint)group.First()["RoleID"]);
        var skills = Table("GodWarRoleSkillData").Where(row => (uint)row["NeedRelationLevel"] >= 3)
            .ToLookup(row => (uint)row["RoleID"]);
        var titles = Table("TextMap").GroupBy(row => (int)row["ID"]["hash"])
            .ToDictionary(group => group.Key, group => NormalizeSignetName((string)group.First()["Text"]));
        return buffs.Where(pair => pair.Key < 20000 && roles.ContainsKey((uint)pair.Value["BuffGroup"]))
            .Select(pair =>
            {
                var row = pair.Value;
                uint avatar = (uint)row["BuffGroup"], role = roles[avatar];
                if (!buffs.TryGetValue(pair.Key + 10000, out var enhanced) ||
                    ((uint)enhanced["BuffGroup"] != avatar && (string)enhanced["AbilityName"] != (string)row["AbilityName"]))
                    return (Avatar: avatar, Upgrade: (ExclusiveEnhancement)null);
                string name = titles.GetValueOrDefault((int)row["BuffName"]["hash"], "");
                var matches = skills[role].Where(skill => name.Length > 0 &&
                    titles.GetValueOrDefault((int)skill["SkillName"]["hash"], "").Contains(name, StringComparison.Ordinal)).ToArray();
                return (Avatar: avatar, Upgrade: matches.Length == 1
                    ? new ExclusiveEnhancement(role, (uint)matches[0]["NeedRelationLevel"], pair.Key, pair.Key + 10000)
                    : null);
            }).Where(pair => pair.Upgrade != null).GroupBy(pair => pair.Avatar)
            .ToDictionary(group => group.Key, group => group.Select(pair => pair.Upgrade).ToArray());
    });

    private static string NormalizeSignetName(string value) => new string((value ?? "").Normalize(
        System.Text.NormalizationForm.FormKC).Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());

    private static void ApplyExclusiveEnhancements(State state, ChallengeRun run)
    {
        if (run.Challenge.AvatarLists.Count != 1 ||
            !ExclusiveEnhancements.Value.TryGetValue(run.Challenge.AvatarLists[0].AvatarId, out var upgrades))
            return;
        var deep = run.Deep;
        foreach (var upgrade in upgrades)
        {
            if (!state.Roles.TryGetValue(upgrade.Role, out var role) || role.Level < upgrade.Level)
                continue;
            deep.EnhancedExclusives[upgrade.Normal] = upgrade.Enhanced;
            if (deep.Buffs.Remove(upgrade.Normal, out uint level))
                deep.Buffs[upgrade.Enhanced] = Math.Max(level, deep.Buffs.GetValueOrDefault(upgrade.Enhanced));
        }
        deep.Candidates = deep.Candidates.Select(id => deep.EnhancedExclusives.GetValueOrDefault(id,id)).Distinct().ToArray();
    }
}
