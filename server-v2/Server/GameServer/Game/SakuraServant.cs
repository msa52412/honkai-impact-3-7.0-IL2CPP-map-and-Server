using System;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static class SakuraServant
{
    private const uint SakuraMap = 1;
    private const uint PartnerFunction = 8;
    private const uint SakuraResonance = 5591;
    private const uint GatherExp = 100;
    private static JArray Read(string name) => JArray.Parse(File.ReadAllText($"Resources/ExcelOutputAsset/{name}.json"));
    private static readonly Lazy<JObject[]> Levels = new(() => Read("MapLevelData").OfType<JObject>()
        .Where(row => (uint)row["MapId"] == SakuraMap).OrderBy(row => (uint)row["Level"]).ToArray());
    private static readonly Lazy<JObject[]> Skills = new(() => Read("MapSkillData").OfType<JObject>()
        .Where(row => (uint)row["MapID"] == SakuraMap).ToArray());
    private static readonly Lazy<System.Collections.Generic.Dictionary<uint, JObject>> GatherPoints = new(() =>
        Read("OpenWorldEventData").OfType<JObject>().Where(row => (uint)row["EventMap"] == SakuraMap &&
            (uint)row["Type"] == 2).ToDictionary(row => (uint)row["EventID"]));
    private static readonly Lazy<uint> UnlockStory = new(() =>
    {
        var rows = JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/OpenWorldFunction.json"));
        var partner = rows.Single(row => (uint)row["MapId"] == SakuraMap && (uint)row["Function"] == PartnerFunction);
        return (uint)partner["UnlockPara"];
    });

    internal static OpenworldTechData Snapshot(UserScheme user, uint map, OpenWorldScheme state = null)
    {
        var tech = new OpenworldTechData { MapId = map };
        if (map == SakuraMap &&
            (OwMapTuning.AllStoryDone || user.OpenWorldStory.Any(story => story.StoryId == UnlockStory.Value && story.IsDone)))
        {
            tech.Level = Math.Max(1u, state?.PartnerLevel ?? 0);
            tech.Exp = state?.PartnerExp ?? 0;
            tech.SkillPoint = 0;
            tech.UnlockSkillLists = Skills.Value.Where(row => (uint)row["UnlockMapLv"] <= tech.Level &&
                (uint)row["UnlockType"] == 0 && (uint)row["UnlockCost"] == 0 &&
                (uint)row["PreSkillID"] == 0 && (uint)row["UnlockStoryId"] == 0)
                .Select(row => (uint)row["SkillID"]).ToArray();
        }
        return tech;
    }

    internal static AddTechExpRsp.Retcode Feed(Player player, AddTechExpReq request)
    {
        lock (player.User)
        lock (player.Equipment)
        {
            var state = player.OpenWorlds?.FirstOrDefault(row => row.MapId == request.MapId);
            if (request.MapId != SakuraMap || state == null || !Snapshot(player.User, SakuraMap, state).ShouldSerializeLevel())
                return AddTechExpRsp.Retcode.TechNotOpen;

            var material = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(request.MaterialId);
            // The local item description specifies 500 EXP for Sakura Resonance even when its numeric field is zero.
            int perItem = material?.ServantExpProvide > 0 ? material.ServantExpProvide : request.MaterialId == SakuraResonance && material != null ? 500 : 0;
            if (perItem == 0 || request.MaterialNum == 0)
                return AddTechExpRsp.Retcode.MaterailCanNotAddExp;
            if (request.MaterialNum > int.MaxValue || player.Equipment.MaterialList.Where(item => item.Id == request.MaterialId).Sum(item => (long)item.Num) < request.MaterialNum)
                return AddTechExpRsp.Retcode.MaterailNotEnough;

            uint oldLevel = state.PartnerLevel, oldExp = state.PartnerExp;
            uint level = Math.Max(1u, oldLevel);
            uint maxLevel = (uint)Levels.Value.Last()["Level"];
            if (level >= maxLevel)
                return AddTechExpRsp.Retcode.TechLevelFull;
            ulong exp = oldExp + (ulong)perItem * request.MaterialNum;
            while (level < maxLevel)
            {
                var row = Levels.Value.Single(entry => (uint)entry["Level"] == level);
                uint required = (uint)row["Exp"];
                if (exp < required)
                    break;
                uint quest = (uint)row["keyQuestID"];
                if (quest != 0 && state.CompletedPartnerQuests?.Contains(quest) != true)
                    return AddTechExpRsp.Retcode.TechLevelFull;
                exp -= required;
                level++;
            }
            if (DailyGameplay.Atomic(player, () =>
            {
                state.PartnerLevel = level;
                state.PartnerExp = level == maxLevel ? 0 : checked((uint)exp);
                player.Equipment.AddMaterial((int)request.MaterialId, -checked((int)request.MaterialNum));
                state.Save();
            }))
                return AddTechExpRsp.Retcode.Succ;
            state.PartnerLevel = oldLevel;
            state.PartnerExp = oldExp;
            return AddTechExpRsp.Retcode.Fail;
        }
    }

    internal static UnlockOpenworldTechSkillRsp.Retcode CheckSkill(Player player, UnlockOpenworldTechSkillReq request)
    {
        var state = player.OpenWorlds?.FirstOrDefault(row => row.MapId == request.MapId);
        var tech = Snapshot(player.User, request.MapId, state);
        if (!tech.ShouldSerializeLevel())
            return UnlockOpenworldTechSkillRsp.Retcode.TechLock;
        var skill = Skills.Value.FirstOrDefault(row => (uint)row["SkillID"] == request.SkillId);
        if (skill == null)
            return UnlockOpenworldTechSkillRsp.Retcode.Fail;
        if ((uint)skill["UnlockMapLv"] > tech.Level)
            return UnlockOpenworldTechSkillRsp.Retcode.LevelLack;
        return tech.UnlockSkillLists.Contains(request.SkillId)
            ? UnlockOpenworldTechSkillRsp.Retcode.AlreadaUnlock : UnlockOpenworldTechSkillRsp.Retcode.Fail;
    }

    internal static TakeOpenworldEventRewardRsp Gather(Player player, TakeOpenworldEventRewardReq request, out bool changed)
    {
        changed = false;
        var reply = new TakeOpenworldEventRewardRsp { MapId = request.MapId, retcode = TakeOpenworldEventRewardRsp.Retcode.MapNotFound };
        if (request.MapId != SakuraMap)
            return reply;
        lock (player.User)
        lock (player.Equipment)
        {
            var map = player.OpenWorlds?.FirstOrDefault(row => row.MapId == SakuraMap);
            if (map == null)
                return reply;
            reply.FinishOnceEventLists = map.SakuraGatheredOnce?.ToArray() ?? Array.Empty<uint>();
            var tech = Snapshot(player.User, SakuraMap, map);
            if (!GatherPoints.Value.TryGetValue(request.EventId, out var point) || (uint)point["UnlockCycle"] > map.Cycle)
            {
                reply.retcode = TakeOpenworldEventRewardRsp.Retcode.EventNotFound;
                return reply;
            }
            var day = DailyGameplay.Day(DailyGameplay.Clock());
            bool once = (uint)point["RefreshType"] == 3;
            var today = map.SakuraGatherDay >= day ? map.SakuraGatheredToday : null;
            var recorded = once ? map.SakuraGatheredOnce : today;
            reply.retcode = TakeOpenworldEventRewardRsp.Retcode.Succ;
            if (recorded?.Contains(request.EventId) == true)
                return reply;

            uint level = tech.Level, exp = tech.Exp;
            if (tech.ShouldSerializeLevel() && level < (uint)Levels.Value.Last()["Level"])
            {
                var row = Levels.Value.Single(entry => (uint)entry["Level"] == level);
                uint required = (uint)row["Exp"], quest = (uint)row["keyQuestID"];
                exp += GatherExp;
                if (exp >= required && (quest == 0 || map.CompletedPartnerQuests?.Contains(quest) == true))
                {
                    level++;
                    exp = level == (uint)Levels.Value.Last()["Level"] ? 0 : exp - required;
                }
                else
                    exp = Math.Min(exp, required - 1);
            }

            var oldToday = map.SakuraGatheredToday;
            var oldOnce = map.SakuraGatheredOnce;
            long oldDay = map.SakuraGatherDay;
            uint oldLevel = map.PartnerLevel, oldExp = map.PartnerExp;
            try
            {
                var updated = (recorded ?? Enumerable.Empty<uint>()).Append(request.EventId).ToList();
                if (once)
                    map.SakuraGatheredOnce = updated;
                else
                {
                    map.SakuraGatherDay = Math.Max(oldDay, day);
                    map.SakuraGatheredToday = updated;
                }
                map.PartnerLevel = level;
                map.PartnerExp = exp;
                map.Save();
            }
            catch (Exception error)
            {
                map.SakuraGatheredToday = oldToday;
                map.SakuraGatheredOnce = oldOnce;
                map.SakuraGatherDay = oldDay;
                map.PartnerLevel = oldLevel;
                map.PartnerExp = oldExp;
                Packet.c.Warn("[OW] Sakura gathering: " + error.Message);
                reply.retcode = TakeOpenworldEventRewardRsp.Retcode.Fail;
                return reply;
            }
            changed = level != tech.Level || exp != tech.Exp;
            reply.FinishOnceEventLists = map.SakuraGatheredOnce?.ToArray() ?? Array.Empty<uint>();
            return reply;
        }
    }

    internal static OwQuestSlot CurrentQuest(OpenWorldScheme map)
    {
        if (map?.MapId != SakuraMap || map.PartnerLevel == 0)
            return null;
        if (map.PartnerQuest != null)
            return map.PartnerQuest;
        var id = (uint)Levels.Value.Single(row => (uint)row["Level"] == map.PartnerLevel)["keyQuestID"];
        if (id == 0 || map.CompletedPartnerQuests?.Contains(id) == true)
            return null;
        return new OwQuestSlot { QuestId = id };
    }

    internal static TakeOpenworldKeyQuestRewardRsp ClaimQuest(Player player, uint mapId)
    {
        var result = new TakeOpenworldKeyQuestRewardRsp { retcode = TakeOpenworldKeyQuestRewardRsp.Retcode.NotKeyQuest };
        lock (player.User)
        lock (player.Equipment)
        {
            var map = player.OpenWorlds?.FirstOrDefault(row => row.MapId == mapId);
            var quest = CurrentQuest(map);
            if (quest == null)
                return result;
            if (quest.Status != (uint)OpenworldQuestStatus.OpenworldQuestFinish)
            {
                result.retcode = TakeOpenworldKeyQuestRewardRsp.Retcode.NotFinish;
                return result;
            }

            var definition = OwQuestService.Def(mapId, quest.QuestId);
            if (definition == null)
                return result;
            var previous = map.CompletedPartnerQuests;
            if (!DailyGameplay.Atomic(player, () =>
            {
                result.RewardLists.Add(DailyGameplay.Grant(player, DailyGameplay.Reward(definition.RewardId)));
                map.CompletedPartnerQuests = (previous ?? Enumerable.Empty<uint>()).Append(quest.QuestId).ToList();
                map.PartnerQuest = null;
                map.Save();
            }))
            {
                result.RewardLists.Clear();
                map.CompletedPartnerQuests = previous;
                map.PartnerQuest = quest;
                result.retcode = TakeOpenworldKeyQuestRewardRsp.Retcode.Fail;
                return result;
            }
            var questData = OwQuest.GetOrCreate(player.User.Uid, mapId);
            result.Level = questData.QuestLevel;
            result.Star = questData.TotalStar;
            result.retcode = TakeOpenworldKeyQuestRewardRsp.Retcode.Succ;
            return result;
        }
    }
}
