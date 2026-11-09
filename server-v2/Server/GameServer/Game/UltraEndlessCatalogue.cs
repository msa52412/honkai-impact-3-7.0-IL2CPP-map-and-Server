using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class UltraEndlessCatalogue
{
 internal sealed record Entry(int Group,int[] Sites,int[] Stages,int[] Buffs,string[] Weather,string[] Enemies,uint MaxScore);
 private static JArray Read(string n)=>JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/"+n+".json"));
 internal static Entry[] Build()
 {
  var all=BaseExcelReader<UltraEndlessSite,UltraEndlessSiteExcel>.GetInstance().All;
  var text=Read("TextMap").GroupBy(x=>(int)x["ID"]["hash"]).ToDictionary(x=>x.Key,x=>(string)x.First()["Text"]);
  string Name(JToken x)=>x==null?null:text.GetValueOrDefault((int?)x["hash"]??0);
  var buffs=Read("UltraEndlessBuff").ToDictionary(x=>(int)x["BuffID"]);
  var details=Read("StageDetail").ToDictionary(x=>(int)x["StageID"]);
  var monsters=Read("UniqueMonsterData").ToDictionary(x=>(int)x["uniqueID"]);
  var floors=BaseExcelReader<UltraEndlessFloor,UltraEndlessFloorExcel>.GetInstance();var result=new List<Entry>();
  foreach(int group in all.Where(s=>s.SiteNodeName=="Area1"&&(s.PreSiteList?.Length??0)==0).Select(s=>s.SiteId/10).Distinct().OrderBy(x=>x))
  {
   if(!UltraEndlessSites.TryResolveGroup(group,all,out var chain)||!UltraEndlessValidation.Playable(chain))continue;
   var weather=chain.Select(s=>Name(buffs[s.BuffId]["BuffNameText"])??("天气ID "+s.BuffId)).ToArray();
   var enemies=chain.Select(s=>string.Join("、",details[s.StageId]["EnemyList"].Select(x=>{int id=(int)x;return monsters.TryGetValue(id,out var m)?(Name(m["name"])??(string)m["monsterName"]??"敌人")+" ("+id+")":"敌人 "+id;}))).ToArray();
   uint maximum=checked((uint)chain.Sum(s=>floors.GetFloorDatasFromStageId(s.StageId).Sum(f=>(long)f.MaxScore)));
   result.Add(new Entry(group,chain.Select(x=>x.SiteId).ToArray(),chain.Select(x=>x.StageId).ToArray(),chain.Select(x=>x.BuffId).ToArray(),weather,enemies,maximum));
  }return result.ToArray();
 }
 internal static string Render()
 {
  var entries=Build();string E(object s)=>WebUtility.HtmlEncode(Convert.ToString(s));var html=new StringBuilder("<!doctype html><html lang='zh-CN'><meta charset='utf-8'><title>超弦空间配置</title><style>body{background:#111827;color:#e5e7eb;font:14px sans-serif;padding:24px}a{color:#67e8f9}input{padding:9px;width:420px;margin:16px 0}table{border-collapse:collapse;white-space:nowrap}td,th{border:1px solid #374151;padding:10px;text-align:left}th{background:#1f2937;position:sticky;top:0}.note{color:#9ca3af}small{display:block;color:#9ca3af}</style><h1>超弦空间 · 已验证配置</h1><a href='/gm'>返回控制台</a><p class='note'>按实际前置关系解析四关；每个配置只列运行时选中的完整链。旧数字分桶表中的混组、尾段不再列作可选配置。敌人与天气名称来自本地配置文本。</p>");
  html.Append("<p>共 "+entries.Length+" 组</p><input id='q' placeholder='搜索配置ID、站点、天气或敌人'><div style='overflow:auto'><table><thead><tr><th>配置ID</th>");
  foreach(var n in new[]{"Area1","Area2","Area3","Area4"})html.Append("<th>"+n+"</th>");html.Append("<th>总分上限</th></tr></thead><tbody>");
  foreach(var r in entries){html.Append("<tr data-group='"+r.Group+"'><td>"+r.Group+"</td>");for(int i=0;i<4;i++)html.Append("<td>站点 "+r.Sites[i]+" · "+E(r.Weather[i])+"<small>关卡 "+r.Stages[i]+" · 天气ID "+r.Buffs[i]+"</small>"+E(string.IsNullOrEmpty(r.Enemies[i])?"未列展示敌人":r.Enemies[i])+"</td>");html.Append("<td>"+r.MaxScore+"</td></tr>");}
  html.Append("</tbody></table></div><script>document.getElementById('q').addEventListener('input',function(){const q=this.value.toLowerCase();document.querySelectorAll('tbody tr').forEach(r=>r.hidden=!r.textContent.toLowerCase().includes(q));});</script></html>");return html.ToString();
 }
}
