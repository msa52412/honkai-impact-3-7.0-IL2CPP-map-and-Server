using System;using System.Linq;using Common.Database;using Common.Resources.Proto;using Common.Utils.ExcelReader;using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static class WitchShopGameplay
{
 internal static bool CurrencyUnlocked(Player p,int currency)
 {
  if(ElfFragmentGameplay.CurrencyUnlocked(p,currency))return true;
  if(currency==3134)return false;
  var meta=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All;
  return p.AvatarList.Any(a=>a.Star>=5&&meta.Any(m=>m.AvatarId==a.AvatarId&&BaseExcelReader<AvatarFragmentData,AvatarFragmentDataExcel>.GetInstance().FromId(m.AvatarFragmentId)?.ReturnMaterialId==currency));
 }
 internal static bool Open(Player p)=>CurrencyUnlocked(p,3129)||CurrencyUnlocked(p,3130)||CurrencyUnlocked(p,3134);
 internal static bool Visible(Player p,JObject goods)=>CurrencyUnlocked(p,(int)goods["CostItemId1"]);
 internal static ElfDataExcel ElfFragment(int item)=>BaseExcelReader<ElfData,ElfDataExcel>.GetInstance().All.FirstOrDefault(x=>x.ElfFragmentId==item);
 internal static void GrantElfFragment(Player p,int item,int num)
 {
  var row=ElfFragment(item)??throw new InvalidOperationException("Unknown elf fragment");
  var values=(p.Elfs.ElfFragments??Array.Empty<ElfFragment>()).Select(x=>new ElfFragment{ElfId=x.ElfId,FragmentNum=x.FragmentNum}).ToList();
  var value=values.SingleOrDefault(x=>x.ElfId==row.ElfId);if(value==null){value=new ElfFragment{ElfId=(uint)row.ElfId};values.Add(value);}value.FragmentNum=checked(value.FragmentNum+(uint)num);p.Elfs.ElfFragments=values.ToArray();p.Elfs.Save();
 }
 internal static SyncElfFragmentNotify Snapshot(Player p){var rsp=new SyncElfFragmentNotify();rsp.ElfFragmentLists.AddRange(p.Elfs.ElfFragments??Array.Empty<ElfFragment>());return rsp;}
}
