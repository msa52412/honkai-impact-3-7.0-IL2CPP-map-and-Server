using System;
using System.Linq;
using Common;
using Common.Resources.Proto;
namespace PemukulPaku.GameServer.Game;
internal static class EquipmentProtection
{
 internal static UpdateEquipmentProtectedStatusRsp Update(Player p,UpdateEquipmentProtectedStatusReq req)
 {
  var rsp=new UpdateEquipmentProtectedStatusRsp{retcode=UpdateEquipmentProtectedStatusRsp.Retcode.Fail};lock(p.Equipment){
   var ws=req.WeaponUniqueIdLists??Array.Empty<uint>();var ss=req.StigmataUniqueIdLists??Array.Empty<uint>();
   if(ws.Length+ss.Length==0||ws.Length+ss.Length>10000||ws.Distinct().Count()!=ws.Length||ss.Distinct().Count()!=ss.Length)return rsp;
   var weapons=p.Equipment.WeaponList.Where(w=>ws.Contains(w.UniqueId)).ToArray();var stigmata=p.Equipment.StigmataList.Where(s=>ss.Contains(s.UniqueId)).ToArray();
   if(weapons.Length!=ws.Length||stigmata.Length!=ss.Length){rsp.retcode=UpdateEquipmentProtectedStatusRsp.Retcode.EquipmentNotExist;return rsp;}
   var wb=weapons.Select(w=>w.IsProtected).ToArray();var sb=stigmata.Select(s=>s.IsProtected).ToArray();
   try{Global.Store.InTransaction(()=>{foreach(var w in weapons)w.IsProtected=req.IsProtected;foreach(var s in stigmata)s.IsProtected=req.IsProtected;p.Equipment.Save();});rsp.retcode=0;}
   catch(Exception e){for(int i=0;i<weapons.Length;i++)weapons[i].IsProtected=wb[i];for(int i=0;i<stigmata.Length;i++)stigmata[i].IsProtected=sb[i];Packet.c.Warn("[EquipmentProtection] rolled back: "+e.Message);}
   return rsp;
  }
 }
}
