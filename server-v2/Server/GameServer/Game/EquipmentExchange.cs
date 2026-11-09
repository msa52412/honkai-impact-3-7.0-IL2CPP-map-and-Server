using System;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
namespace PemukulPaku.GameServer.Game;
internal static class EquipmentExchange
{
 internal static ExchangeAvatarWeaponRsp Execute(Player p,ExchangeAvatarWeaponReq req){lock(p.Equipment){
  var r=new ExchangeAvatarWeaponRsp{retcode=ExchangeAvatarWeaponRsp.Retcode.AvatarError};
  if(req.AvatarId1==req.AvatarId2)return r;
  var a=p.AvatarList.SingleOrDefault(x=>x.AvatarId==req.AvatarId1&&x.Star>0);var b=p.AvatarList.SingleOrDefault(x=>x.AvatarId==req.AvatarId2&&x.Star>0);if(a==null||b==null)return r;
  uint first=a.WeaponUniqueId,second=b.WeaponUniqueId;
  var aw=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==first);var bw=p.Equipment.WeaponList.SingleOrDefault(x=>x.UniqueId==second);
  if(aw==null||bw==null||first==second)return r;
  if(p.AvatarList.Any(x=>x!=a&&x!=b&&(x.WeaponUniqueId==first||x.WeaponUniqueId==second)))return r;
  if(!EquipmentDress.Accepts(a,bw)||!EquipmentDress.Accepts(b,aw)){r.retcode=ExchangeAvatarWeaponRsp.Retcode.WeaponTypeError;return r;}
  try{Global.Store.InTransaction(()=>{a.WeaponUniqueId=second;b.WeaponUniqueId=first;a.Save();b.Save();});r.retcode=0;}
  catch(Exception e){a.WeaponUniqueId=first;b.WeaponUniqueId=second;r.retcode=ExchangeAvatarWeaponRsp.Retcode.Fail;Packet.c.Warn("[EquipmentExchange] rolled back: "+e.Message);}return r;
 }}
}
