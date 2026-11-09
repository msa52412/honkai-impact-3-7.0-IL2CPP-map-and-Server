using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class UltraEndlessValidation
{
 private static JArray Read(string name)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/"+name+".json"));
 private static readonly Lazy<HashSet<int>> StageIds=new(()=>Read("StageDetail").Select(x=>(int)x["StageID"]).ToHashSet());
 private static readonly Lazy<HashSet<int>> BuffIds=new(()=>Read("UltraEndlessBuff").Select(x=>(int)x["BuffID"]).ToHashSet());
 internal static bool Playable(UltraEndlessSiteExcel[] sites)
 {
  var reader=BaseExcelReader<UltraEndlessFloor,UltraEndlessFloorExcel>.GetInstance();
  return sites.Length==4&&sites.Select(s=>s.SiteNodeName).Distinct().Count()==4&&sites.All(s=>{
   var f=reader.GetFloorDatasFromStageId(s.StageId);return StageIds.Value.Contains(s.StageId)&&BuffIds.Value.Contains(s.BuffId)&&f.Count>0&&f.All(x=>x.FloorId>0&&x.MaxScore>0&&x.NeedScore>=0&&x.NeedScore<=x.MaxScore)&&f.Select(x=>x.FloorId).Distinct().Count()==f.Count;
  });
 }
 private static readonly Lazy<HashSet<uint>> UsableSchedules=new(()=>{
  var battle=Read("UltraEndlessBattleConfig").ToDictionary(x=>(int)x["BattleConfigID"]);
  return Read("UltraEndlessSchedule").Where(s=>(uint)s["ScheduleID"]>0&&(uint)s["ScheduleID"]<=ushort.MaxValue&&Enumerable.Range(1,9).All(level=>s["BattleConfigList"].Any(x=>(int)x["GroupLevel"]==level&&battle.TryGetValue((int)x["BattleConfigID"],out var b)&&!string.IsNullOrWhiteSpace((string)b["SiteMapName"])))).Select(s=>(uint)s["ScheduleID"]).ToHashSet();
 });
 internal static bool Schedule(uint id)=>UsableSchedules.Value.Contains(id);
 internal static bool ContainsSite(uint id)=>UltraEndlessSites.Active().Any(s=>(uint)s.SiteId==id);
}
