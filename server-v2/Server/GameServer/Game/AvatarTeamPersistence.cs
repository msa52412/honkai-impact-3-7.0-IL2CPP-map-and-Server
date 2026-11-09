using System;
using System.Linq;
using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;
using Newtonsoft.Json;
namespace PemukulPaku.GameServer.Game;
internal static class AvatarTeamPersistence
{
 private static bool Valid(Player p,uint[] ids)=>ids!=null&&ids.Length>=1&&ids.Length<=3&&ids.Where(x=>x!=0).Distinct().Count()==ids.Count(x=>x!=0)&&ids.Any(x=>x!=0)&&ids.All(id=>id==0||p.AvatarList.Any(a=>a.AvatarId==id&&a.Star>0));
 internal static bool Save(Player p,AvatarTeam team){
  if(team==null||!Valid(p,team.AvatarIdLists))return false;
  lock(p.Equipment){var snapshot=JsonConvert.SerializeObject(p.User.AvatarTeamList);try{
   var old=p.User.AvatarTeamList.FirstOrDefault(x=>x.StageType==team.StageType);
   if(old!=null)old.AvatarIdLists=team.AvatarIdLists.ToArray();else p.User.AvatarTeamList.Add(team);
   p.User.Save();return true;
  }catch{p.User.AvatarTeamList=JsonConvert.DeserializeObject<List<AvatarTeam>>(snapshot);return false;}}
 }
 internal static bool SaveCustom(Player p,CustomAvatarTeam team){
  if(team==null||team.TeamId==0||!Valid(p,team.AvatarIdLists)||(team.ElfIdLists?.Length??0)>1||team.Name?.Length>64)return false;
  lock(p.Equipment){var snapshot=JsonConvert.SerializeObject(p.User.CustomAvatarTeamList);try{
   var old=p.User.CustomAvatarTeamList.FirstOrDefault(x=>x.TeamId==team.TeamId);
   if(old!=null){old.AvatarIdLists=team.AvatarIdLists.ToArray();old.ElfIdLists=team.ElfIdLists?.ToArray();old.Name=team.Name;}else p.User.CustomAvatarTeamList.Add(team);
   p.User.Save();return true;
  }catch{p.User.CustomAvatarTeamList=JsonConvert.DeserializeObject<List<CustomAvatarTeam>>(snapshot);return false;}}
 }
}
