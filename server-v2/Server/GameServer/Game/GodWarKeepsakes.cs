using System;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
using Reward = Common.Resources.Proto.RewardData;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    internal const uint OldDustMission = 660247;
    private const uint OldDust = 119305;
    private const uint LightningWinsRequired = 100;
    private static readonly uint[] VesselLevel4 = {119306,0,119326,119307,119327,119308,119309,119310,119328,119311,119329,119312,119330};
    private static readonly uint[] VesselLevel8 = {119314,0,119344,119315,119345,119316,119317,119318,119346,119319,119347,119320,119348};

    private static void CompleteVesselLevels(JArray rows)
    {
        var template = rows.Where(x => (uint)x["GodWarID"] == 1 && (uint)x["RoleID"] == 1)
            .ToDictionary(x => (uint)x["Level"]);
        foreach (var group in rows.OfType<JObject>().Where(x => (uint)x["GodWarID"] == 1 && (uint)x["RoleID"] is >= 1 and <= 13)
                     .GroupBy(x => (uint)x["RoleID"]).ToArray())
        {
            var last = group.OrderBy(x => (uint)x["Level"]).Last();
            uint level = (uint)last["Level"];
            if (level >= 11) continue;
            foreach (var row in group) row["MaxLevel"] = 11;
            last["Exp"] = template[level]["Exp"].DeepClone();
            // The export lacks these rows. Use the existing vessel EXP curve,
            // keeping all recorded costs and adding no guessed story-item costs.
            for (uint next = level + 1; next <= 11; next++)
            {
                var row = (JObject)last.DeepClone();
                row["Level"] = next;
                row["Exp"] = template[next]["Exp"].DeepClone();
                row["RewardID"] = 0;
                row["LvUpMaterial"] = new JObject { ["MaterialID"] = 0, ["Num"] = 0 };
                rows.Add(row);
            }
        }
    }

    private static IEnumerable<(uint Level, uint Id)> VesselSigils(uint roleId)
    {
        // The 7.0 vessel page uses roles 14–26. The older vessel
        // records 1–13 share the same sigils and inventory ownership.
        if (roleId is >= 14 and <= 26) roleId -= 13;
        if (roleId is < 1 or > 13) yield break;
        if (VesselLevel4[roleId-1] != 0) yield return (4,VesselLevel4[roleId-1]);
        if (VesselLevel8[roleId-1] != 0) yield return (8,VesselLevel8[roleId-1]);
        yield return (11,119330+roleId);
    }

    private static IEnumerable<(uint Level, uint Id)> MissingVesselSigils(Player p, uint roleId, Role role) =>
        VesselSigils(roleId).Where(item => role.Level >= item.Level && Held(p,item.Id) == 0);

    private static uint VesselClaimLevel(Player p, uint roleId, Role role) =>
        MissingVesselSigils(p,roleId,role).Select(x => x.Level-1).Append(role.RewardLevel).Min();

    private static void CountLightningSupportWin(State state)
    {
        if (state.LightningSupportWins < LightningWinsRequired) state.LightningSupportWins++;
    }

    internal static void ApplyKeepsakeMission(Player p, GetMissionDataRsp response)
    {
        lock (Gate)
        {
            var mission = response.MissionLists.FirstOrDefault(x => x.MissionId == OldDustMission);
            if (mission == null) return;
            var state = Load(p.User);
            bool held = Held(p,OldDust) > 0;
            mission.Progress = held ? LightningWinsRequired : Math.Min(state.LightningSupportWins,LightningWinsRequired);
            mission.Status = held ? MissionStatus.MissionClose :
                mission.Progress == LightningWinsRequired ? MissionStatus.MissionFinish : MissionStatus.MissionDoing;
        }
    }

    internal static GetMissionRewardRsp.Retcode TakeKeepsakeMission(Player p, uint[] ids,
        out Reward reward, out uint[] granted)
    {
        reward = new();
        granted = Array.Empty<uint>();
        lock (p.Equipment) lock (Gate)
        {
            if (ids.Length != 1) return GetMissionRewardRsp.Retcode.MissionIdError;
            var state = Load(p.User);
            if (Held(p,OldDust) > 0 || state.LightningSupportWins < LightningWinsRequired)
                return GetMissionRewardRsp.Retcode.MissionStatusError;
            _ = MissionTake.collection;
            var plan = DailyGameplay.Reward(1460588);
            Reward issued = null;
            if (!Commit(p,state,() => {issued = DailyGameplay.Grant(p,plan); MissionTake.Mark(p.User.Uid,OldDustMission);}))
                return GetMissionRewardRsp.Retcode.MissionStatusError;
            reward = issued;
            granted = new[] {OldDustMission};
            return GetMissionRewardRsp.Retcode.Succ;
        }
    }
}
