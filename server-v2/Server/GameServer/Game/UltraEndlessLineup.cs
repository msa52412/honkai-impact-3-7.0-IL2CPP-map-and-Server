using System;
using System.IO;
using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

/// <summary>Battle-report equipment snapshots, independent of later loadout changes.</summary>
internal static class UltraEndlessLineup
{
    public static LineupBriefInfo Capture(Player player, uint[] avatarIds, uint[] elfIds)
    {
        var ids = (avatarIds ?? Array.Empty<uint>()).Where(id => id != 0).ToArray();
        var elves = (elfIds ?? Array.Empty<uint>()).Where(id => id != 0).ToArray();
        if (ids.Length == 0 || ids.Length > 3 || ids.Distinct().Count() != ids.Length || elves.Length > 1)
            return null;
        var result = new LineupBriefInfo();
        foreach (uint id in ids)
        {
            var avatar = player.AvatarList?.FirstOrDefault(a => a.AvatarId == id);
            if (avatar == null) return null;
            var weapon = player.Equipment?.WeaponList?.FirstOrDefault(w => w.UniqueId == avatar.WeaponUniqueId);
            uint Stigmata(uint uniqueId) => uniqueId == 0 ? 0 :
                player.Equipment?.StigmataList?.FirstOrDefault(s => s.UniqueId == uniqueId)?.Id ?? 0;
            result.AvatarInfoLists.Add(new AvatarBriefInfo
            {
                AvatarId = id, AvatarLevel = avatar.Level, AvatarStar = avatar.Star,
                WeaponId = weapon?.Id ?? 0, SubWeaponId = weapon?.SubWeaponId ?? 0,
                StigmataIdLists = new[] { Stigmata(avatar.StigmataUniqueId1), Stigmata(avatar.StigmataUniqueId2), Stigmata(avatar.StigmataUniqueId3) },
                DressId = avatar.DressId, IsArtifactSwitchOn = avatar.AvatarArtifact?.IsArtifactSwitchOn ?? false
            });
        }
        foreach (uint id in elves)
        {
            var elf = player.Elfs?.Elves?.FirstOrDefault(e => e.ElfId == id);
            if (elf == null) return null;
            var brief = new ElfBriefInfo
            {
                ElfId = id, ElfLevel = elf.Level, ElfStar = elf.Star,
                EquipTalentIdLists = elf.EquipTalentIdLists?.ToArray() ?? Array.Empty<uint>()
            };
            if (elf.SkillLists != null)
                brief.SkillLists.AddRange(elf.SkillLists.Select(s => new ElfBriefInfo.Skill { SkillId = s.SkillId, SkillLevel = s.SkillLevel }));
            result.ElfInfoLists.Add(brief);
        }
        return result;
    }

    public static LineupBriefInfo Copy(LineupBriefInfo value)
    {
        if (value == null) return null;
        using var stream = new MemoryStream();
        ProtoBuf.Serializer.Serialize(stream, value);
        stream.Position = 0;
        return ProtoBuf.Serializer.Deserialize<LineupBriefInfo>(stream);
    }
}
