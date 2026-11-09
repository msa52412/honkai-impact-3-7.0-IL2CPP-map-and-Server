using System;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // Verified request: role1337 is HoRb (avatar714). The local export stops
    // at1335. These four unlock/EXP rows use the last exported role template;
    // they are bounded compatibility data, not recovered full7.0 tables.
    private static readonly (uint Role,uint Avatar,uint Exp)[] EntryRoles={
        (1336,3601,119783),(1337,714,119784),(1338,3701,119785),(1339,507,119786)};
    internal static (uint Begin,uint End) EntryWindow(uint tale,long now)
    {
        if(tale!=30)return (0,uint.MaxValue);
        long day=(now+14400)/86400,week=(day+3)/7;
        long reset=week*604800-259200-14400;
        return (checked((uint)(reset+21600)),checked((uint)(reset+604800)));
    }
    private static bool WeeklyEntryOpen(long now){var w=EntryWindow(30,now);return now>=w.Begin&&now<w.End;}
    private static uint[] SupportIds(uint[] ids)=>(ids??Array.Empty<uint>()).Where(x=>x!=0&&x!=uint.MaxValue).ToArray();
    private static void CompleteEntryTable(string name,JArray rows)
    {
        if(name=="GodWarRelationData")foreach(var map in EntryRoles)
        {
            if(rows.Any(x=>(uint)x["RoleID"]==map.Role))continue;
            foreach(var source in rows.Where(x=>(uint)x["RoleID"]==1335).ToArray())
            {var x=(JObject)source.DeepClone();x["RoleID"]=map.Role;x["AvatarID"]=map.Avatar;x["ExpMaterialID"]=map.Exp;x["IconPath"]="SpriteOutput/AvatarCardFigures/"+(60000+map.Avatar);x["FigurePath"]=x["IconPath"].DeepClone();rows.Add(x);}
        }
        if(name=="GodWarMainAvatar")foreach(var map in EntryRoles)
        {
            if(rows.Any(x=>(uint)x["MainAvatarID"]==map.Avatar))continue;
            // Client supplies its native avatar adjustments. Do not copy a
            // different battlesuit's ability or fabricate character-trial IDs.
            rows.Add(new JObject{["GodWarID"]=1,["MainAvatarID"]=map.Avatar,["AbilityName"]="",["ParamList"]=new JArray(),["AvatarAbilityID"]=new JArray(),["AvatarMissionList"]=new JArray(),["AvatarChallengeStageIDList"]=new JArray(),["StepUnlockMissionMap"]=new JArray()});
        }
        if(name=="GodWarAvatarTaleSchedule")foreach(var row in rows)
            foreach(var map in EntryRoles)if(!row["MainAvatarList"].Values<uint>().Contains(map.Avatar))((JArray)row["MainAvatarList"]).Add(map.Avatar);
    }
    private static System.Collections.Generic.Dictionary<uint,uint> EntryRoleDefaults;
    private static void RestoreMissingEntryRoles(State s)
    {
        EntryRoleDefaults??=Table("GodWarRelationData").GroupBy(x=>(uint)x["RoleID"])
            .ToDictionary(g=>g.Key,g=>EntryRoles.Any(x=>x.Role==g.Key)?0u:g.Min(x=>(uint)x["Level"]));
        foreach(var row in EntryRoleDefaults)
            if(!s.Roles.ContainsKey(row.Key))s.Roles[row.Key]=new Role{Level=row.Value};
    }
}
