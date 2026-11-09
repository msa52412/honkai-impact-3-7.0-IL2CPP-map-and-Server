using System;using System.Linq;using System.Collections.Generic;using System.Security.Cryptography;using Common;using Common.Database;using Common.Resources.Proto;using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;
// Historical mechanics, with explicitly reconstructed base probabilities in the pool file.
internal static class DormSupply
{
 public sealed class State:IRow{public long Id{get;set;}public long CreatedAt{get;set;}public uint Uid{get;set;}public uint SCount{get;set;}public uint SinceNew{get;set;}}
 private static SqlTable<State> Store=>Global.Table<State>("DormSupplyStates",("owner_uid","INTEGER",x=>x.Uid,true));
 internal static State Load(uint uid)=>Store.All().FirstOrDefault(x=>x.Uid==uid)??new State{Uid=uid};
 internal static void Save(State s){if(s.Id==0)Store.InsertOne(s);else Store.ReplaceOne(s);}
 internal static uint Limit(State s)=>s.SCount<2?25u:100u;
 internal static Func<int,int> RandomBelow=RandomNumberGenerator.GetInt32;
 internal static int Random(int n){if(n<=0)throw new InvalidOperationException("Empty Dorm roll");int r=RandomBelow(n);if(r<0||r>=n)throw new InvalidOperationException("Invalid Dorm roll");return r;}
 internal static GachaDrop[] SCards(GachaPool pool)=>pool.Drops.Where(x=>x.Featured&&x.Kind.Equals("avatarCard",StringComparison.OrdinalIgnoreCase)).ToArray();
 internal static uint AvatarId(GachaDrop d)=>(uint)BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.Single(x=>x.AvatarCardId==d.Id).AvatarId;
 internal static GachaDrop[] Missing(GachaPool pool,IEnumerable<AvatarScheme> avatars){var owned=avatars.Where(x=>x.Star>0).Select(x=>x.AvatarId).ToHashSet();return SCards(pool).Where(d=>!owned.Contains(AvatarId(d))).ToArray();}
 internal static double NewProbability(int total,int missing)=>missing==0?0:missing==total?1:Math.Max(.4,2.0*missing/(total+missing));
 internal static Dictionary<GachaDrop,double> Distribution(GachaPool pool,EquipmentScheme.GachaProgress progress,State state,IEnumerable<AvatarScheme> avatars)
 {
  double total=pool.Drops.Sum(x=>(double)Math.Max(0,x.Weight));if(total<=0)throw new InvalidOperationException("Empty Dorm weights");
  var result=pool.Drops.ToDictionary(x=>x,x=>Math.Max(0,x.Weight)/total);var high=SCards(pool);var low=pool.Drops.Where(x=>x.Pity).ToArray();if(high.Length==0||low.Length==0)throw new InvalidOperationException("Incomplete Dorm pool");
  if(progress.Up>=Limit(state)-1){foreach(var d in pool.Drops)result[d]=d.Featured?1.0/high.Length:0;}
  else if(progress.Low>=9){double mass=0;foreach(var d in pool.Drops.Where(x=>!x.Pity&&!x.Featured)){mass+=result[d];result[d]=0;}double sum=low.Sum(x=>(double)x.Weight);foreach(var d in low)result[d]+=mass*d.Weight/sum;}
  double sMass=high.Sum(x=>result[x]);var missing=Missing(pool,avatars);var missingSet=missing.ToHashSet();var existing=high.Where(x=>!missingSet.Contains(x)).ToArray();double newMass=state.SinceNew>=2&&missing.Length>0?1:NewProbability(high.Length,missing.Length);
  foreach(var d in high)result[d]=sMass*(missingSet.Contains(d)?newMass/missing.Length:existing.Length==0?0:(1-newMass)/existing.Length);
  return result;
 }
 internal static GachaDrop Pick(GachaPool pool,EquipmentScheme.GachaProgress progress,State state,IEnumerable<AvatarScheme> avatars)
 {
  var dist=Distribution(pool,progress,state,avatars);double r=Random(1000000000)/1000000000.0;GachaDrop last=null;
  foreach(var kv in dist){if(kv.Value<=0)continue;last=kv.Key;if(r<kv.Value)return kv.Key;r-=kv.Value;}return last??throw new InvalidOperationException("Empty Dorm distribution");
 }
 internal static AdventureGachaData Display(EquipmentScheme equipment,GachaPool pool)
 {
  var s=Load(equipment.OwnerUid);var p=GachaService.PoolProgress(equipment,pool);int n=SCards(pool).Length,m=Missing(pool,Common.Database.Avatar.AvatarsFromUid(equipment.OwnerUid)).Length;
  return new AdventureGachaData{TicketHcoinCost=200,TicketMaterialId=1110,IsEnableBaodi=true,GachaTimes=p.Total,IsProtectDisplay=true,DisplayMaxTimes=Limit(s),NoProtectGachaTimes=p.Up,IsGotSAvatarProtect=s.SCount>=2,NewSAvatarProb=s.SinceNew>=2&&m>0?1:NewProbability(n,m),NextSAvatarAssureGachaTimes=Limit(s)-Math.Min(p.Up,Limit(s)-1),NextSAvatarAssureNewNeedTimes=m==0?0:3-Math.Min(s.SinceNew,2),FreeGachaTimes=0};
 }
}
internal static partial class GachaService
{
 private static List<GachaItem> DrawDorm(Session session,uint times,GachaPool pool)
 {
  var player=session.Player;var state=DormSupply.Load(player.User.Uid);var progress=PoolProgress(player.Equipment,pool);player.Equipment.GachaProgressByType??=new();player.Equipment.GachaProgressByType[20]=progress;var result=new List<GachaItem>();
  for(uint i=0;i<times;i++){
   var drop=DormSupply.Pick(pool,progress,state,player.AvatarList);bool isNew=drop.Featured&&!player.AvatarList.Any(x=>x.AvatarId==DormSupply.AvatarId(drop)&&x.Star>0);
   if(!TryGrant(drop,out var item)||item==null)throw new InvalidOperationException("Unresolvable Dorm reward");GrantInventory(player,drop,item);ConvertDuplicate(player,drop,item,pool);GrantGift(player,pool,item);UnlockAvatar(player,drop);GachaRecords.Append(player.User.Uid,20,item);result.Add(item);
   progress.Total=checked(progress.Total+1);progress.Low=drop.Pity||drop.Featured?0:progress.Low+1;
   if(drop.Featured){progress.Up=0;state.SCount=checked(state.SCount+1);state.SinceNew=isNew?0:Math.Min(2,state.SinceNew+1);}else progress.Up++;
  }
  DormSupply.Save(state);return result;
 }
 internal static void GrantShopCard(Player p,uint card){if(!GachaConfig.GrantAvatar)throw new InvalidOperationException("Avatar granting disabled");var drop=new GachaDrop{Kind="avatarCard",Id=card,Num=1};if(!TryGrant(drop,out var item))throw new InvalidOperationException("Invalid shop card");var row=FindByCardId((int)card);if(p.AvatarList.Any(x=>x.AvatarId==row.AvatarId&&x.Star>0))throw new InvalidOperationException("Shop card already owned");UnlockAvatar(p,drop);}
}
