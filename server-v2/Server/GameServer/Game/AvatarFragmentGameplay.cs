using System;
using System.Linq;
using System.Collections.Generic;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;

internal static class AvatarFragmentGameplay
{
 // Native RequestAvatarFragmentTransform (181AACD80) sends fragment item IDs,
 // not avatar IDs. Native FilterForTransform requires an owned SSS avatar.
 internal static AvatarFragmentTransformRsp Transform(Player p, AvatarFragmentTransformReq req)
 {
  var rsp = new AvatarFragmentTransformRsp { retcode = AvatarFragmentTransformRsp.Retcode.Fail };
  lock (p.Equipment)
  {
   bool batch = req.FragmentLists.Count > 0;
   if (batch && (req.ShouldSerializeId() || req.ShouldSerializeNum())) return rsp;
   var input = batch ? req.FragmentLists : new List<GenericItemNum> { new() { Id = req.Id, Num = req.Num } };
   if (input.Count == 0 || input.Count > 256 || input.Any(x => x == null || x.Id == 0 || x.Num == 0 || x.Level > 1)) return rsp;
   var debits = new List<(AvatarScheme Avatar, uint Num, uint Before)>();
   var rewards = new Dictionary<int, long>();
   foreach (var group in input.GroupBy(x => x.Id))
   {
    var fragment = BaseExcelReader<AvatarFragmentData, AvatarFragmentDataExcel>.GetInstance().FromId((int)group.Key);
    var meta = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All.FirstOrDefault(a => a.AvatarFragmentId == group.Key);
    if (fragment == null || meta == null || fragment.ReturnMaterialId <= 0 || fragment.ReturnNum <= 0) return rsp;
    var avatars = p.AvatarList.Where(a => a.AvatarId == meta.AvatarId).ToArray();
    if (avatars.Length != 1 || avatars[0].Star < 5) { rsp.retcode = AvatarFragmentTransformRsp.Retcode.NeedSss; return rsp; }
    ulong amount = group.Aggregate(0UL, (sum, x) => sum + x.Num);
    var avatar = avatars[0];
    if (amount > avatar.Fragment) { rsp.retcode = AvatarFragmentTransformRsp.Retcode.Lack; return rsp; }
    debits.Add((avatar, (uint)amount, avatar.Fragment));
    rewards[fragment.ReturnMaterialId] = rewards.GetValueOrDefault(fragment.ReturnMaterialId) + (long)amount * fragment.ReturnNum;
   }
   foreach (var reward in rewards)
   {
    var material = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId((uint)reward.Key);
    if (material == null) return rsp;
    long held = p.Equipment.MaterialList.Where(m => m.Id == reward.Key).Sum(m => (long)m.Num);
    long cap = material.QuantityLimit > 0 ? Math.Min(material.QuantityLimit, int.MaxValue) : int.MaxValue;
    if (reward.Value > int.MaxValue || held + reward.Value > cap) { rsp.retcode = AvatarFragmentTransformRsp.Retcode.Full; return rsp; }
   }
   if (!DailyGameplay.Atomic(p, () => {
    foreach (var debit in debits) { debit.Avatar.Fragment -= debit.Num; debit.Avatar.Save(); }
    foreach (var reward in rewards) p.Equipment.AddMaterial(reward.Key, checked((int)reward.Value));
   })) { foreach (var debit in debits) debit.Avatar.Fragment = debit.Before; return rsp; }
   foreach (var reward in rewards.OrderBy(x => x.Key)) rsp.ReturnLists.Add(new GenericItemNum { Id = (uint)reward.Key, Num = (uint)reward.Value });
   if (!batch && rsp.ReturnLists.Count == 1) { rsp.ReturnId = rsp.ReturnLists[0].Id; rsp.ReturnNum = rsp.ReturnLists[0].Num; rsp.ReturnLists.Clear(); }
   rsp.retcode = AvatarFragmentTransformRsp.Retcode.Succ;
   return rsp;
  }
 }
}
