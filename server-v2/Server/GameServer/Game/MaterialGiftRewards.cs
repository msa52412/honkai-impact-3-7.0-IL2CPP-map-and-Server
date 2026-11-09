using System;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using RewardConfig = Common.Utils.ExcelReader.RewardData;
using ProtoReward = Common.Resources.Proto.RewardData;
namespace PemukulPaku.GameServer.Game;

// Deterministic material gifts only. Random boxes and virtual currencies require their
// own rules; an unsupported component rejects the whole gift before any consumption.
internal static class MaterialGiftRewards
{
    private sealed record Grant(int Id, uint Level, uint Num, string Kind, int Limit = 0, int AvatarId = 0);
    internal static UseMaterialRsp Use(Player player, UseMaterialReq request, MaterialUseDataExcel config, bool validateOnly = false)
    {
        var rsp = new UseMaterialRsp { MaterialId = request.MaterialId, Num = request.Num };
        UseMaterialRsp Fail(UseMaterialRsp.Retcode code) { rsp.retcode = code; return rsp; }
        if (player.User == null || player.User.Uid != player.Equipment.OwnerUid) return Fail(UseMaterialRsp.Retcode.Fail);
        if (config.UseType != 1 && config.UseType != 11) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        if (config.EventId != 0 || config.UseMin != 0 || config.UseMax != 0)
            return Fail(UseMaterialRsp.Retcode.FeatureClosed); // Eligibility of historical/event-only variants not certified.
        if (request.Num == 0 || (config.MultiUse <= 0 && request.Num != 1)) return Fail(UseMaterialRsp.Retcode.InvalidNum);
        if (request.ConsumeItemList?.ItemLists.Count > 0) return Fail(UseMaterialRsp.Retcode.ConsumeItemNotExist);
        var options = config.ParaStr ?? Array.Empty<string>();
        string selected;
        if (config.UseType == 1)
        {
            if (options.Length != 1) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            selected = options[0];
        }
        else
        {
            if (request.Parameter == 0) return Fail(UseMaterialRsp.Retcode.Fail);
            var matches = options.Where(x => x != null && x.Split(':')[0] == request.Parameter.ToString()).ToArray();
            if (matches.Length != 1) return Fail(UseMaterialRsp.Retcode.Fail);
            selected = matches[0];
        }
        var parts = selected?.Split(':');
        if (parts == null || parts.Length > 2 || !int.TryParse(parts[0], out int rewardId) || rewardId <= 0)
            return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        // Nonzero suffixes have additional owned-reward rules; do not silently discard them.
        if (parts.Length == 2 && parts[1] != "0") return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        if (config.UseType == 1 && request.Parameter != 0 && request.Parameter != rewardId) return Fail(UseMaterialRsp.Retcode.Fail);
        var reward = BaseExcelReader<RewardConfig, RewardDataExcel>.GetInstance().FromId(rewardId);
        if (reward == null || reward.RewardExp < 0 || reward.RewardHCoin < 0 || reward.RewardStamina < 0 ||
            reward.RewardFriendPoint != 0 || reward.RewardDutyPoint != 0) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        long exp = (long)reward.RewardExp * request.Num, crystals = (long)reward.RewardHCoin * request.Num,
            stamina = (long)reward.RewardStamina * request.Num;
        if (exp + player.User.Exp > int.MaxValue || crystals + player.User.Hcoin > int.MaxValue || stamina + player.User.Stamina > int.MaxValue)
            return Fail(UseMaterialRsp.Retcode.EquipmentMaterialLimit);
        // Stamina gift descriptions prohibit exceeding the normal level-dependent cap.
        if (stamina > 0)
        {
            // 单机版体力锁死：喝体力药水不会涨，直接按「体力已满」拒绝，
            // 免得药水被消耗掉却一点效果都没有。
            if (GmLocks.StaminaLocked) return Fail(UseMaterialRsp.Retcode.StaminaFull);
            var levels = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance();
            var cap = levels.FromLevel(levels.CalculateLevel(player.User.Exp).Level)?.Stamina;
            if (!cap.HasValue || cap <= 0) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            if (player.User.Stamina + stamina > cap) return Fail(UseMaterialRsp.Retcode.StaminaFull);
        }
        var ids = new[] { reward.RewardItem1Id, reward.RewardItem2Id, reward.RewardItem3Id, reward.RewardItem4Id, reward.RewardItem5Id, reward.RewardItem6Id };
        var nums = new[] { reward.RewardItem1Num, reward.RewardItem2Num, reward.RewardItem3Num, reward.RewardItem4Num, reward.RewardItem5Num, reward.RewardItem6Num };
        var levelsList = new[] { reward.RewardItem1Level, reward.RewardItem2Level, reward.RewardItem3Level, reward.RewardItem4Level, reward.RewardItem5Level, reward.RewardItem6Level };
        var grants = new List<Grant>();
        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i] == 0 && nums[i] == 0) continue;
            long amount = (long)nums[i] * request.Num;
            if (ids[i] <= 0 || nums[i] <= 0 || amount > int.MaxValue || levelsList[i] < 0)
                return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            uint level = (uint)Math.Max(1, levelsList[i]);
            var weapon = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().FromId(ids[i]);
            if (weapon != null)
            {
                if (level > weapon.MaxLv) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
                grants.Add(new(ids[i], level, (uint)amount, "weapon")); continue;
            }
            var stigma = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().FromId(ids[i]);
            if (stigma != null)
            {
                if (level > stigma.MaxLv) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
                grants.Add(new(ids[i], level, (uint)amount, "stigmata")); continue;
            }
            var avatars = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All;
            var fragment = avatars.FirstOrDefault(x => x.AvatarFragmentId == ids[i]);
            if (fragment != null)
            {
                if (fragment.AvatarId >= 9011 && fragment.AvatarId <= 9051) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
                grants.Add(new(ids[i], 1, (uint)amount, "fragment", AvatarId: fragment.AvatarId)); continue;
            }
            // Card/dress/virtual items must not be reported as ordinary inventory materials.
            if (avatars.Any(x => x.AvatarCardId == ids[i])) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            var material = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId((uint)ids[i]);
            if (material == null) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            grants.Add(new(ids[i], 1, (uint)amount, "material", material.QuantityLimit));
        }
        if (grants.Count == 0 && exp == 0 && crystals == 0 && stamina == 0) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        foreach (var group in grants.Where(x => x.Kind == "material").GroupBy(x => x.Id))
        {
            long owned = player.Equipment.MaterialList.Where(x => x.Id == group.Key).Sum(x => (long)x.Num);
            long final = owned - (group.Key == request.MaterialId ? request.Num : 0) + group.Sum(x => (long)x.Num);
            int limit = group.First().Limit;
            if (final > uint.MaxValue || (limit > 0 && final > limit)) return Fail(UseMaterialRsp.Retcode.EquipmentMaterialLimit);
        }
        var oldAvatars = player.AvatarList ?? Array.Empty<AvatarScheme>();
        foreach (var group in grants.Where(x => x.Kind == "fragment").GroupBy(x => x.AvatarId))
        {
            var avatar = oldAvatars.FirstOrDefault(x => x.AvatarId == group.Key);
            if ((long)(avatar?.Fragment ?? 0) + group.Sum(x => (long)x.Num) > uint.MaxValue)
                return Fail(UseMaterialRsp.Retcode.EquipmentFragmentLimit);
        }
        long gearCount = grants.Where(x => x.Kind == "weapon" || x.Kind == "stigmata").Sum(x => (long)x.Num);
        if ((long)player.Equipment.WeaponList.Length + player.Equipment.StigmataList.Length + gearCount > GachaService.EquipmentSizeLimit(player.Equipment))
            return Fail(UseMaterialRsp.Retcode.EquipmentFull);
        if (validateOnly) { rsp.retcode = UseMaterialRsp.Retcode.Succ; return rsp; }
        // Ensure schemas exist outside the transaction, including the allocation sequence table.
        _ = User.collection; _ = Common.Database.Avatar.collection; _ = AutoIncrement.collection;
        var equipment = player.Equipment;
        var oldMaterials = equipment.MaterialList; var oldCounts = oldMaterials.Select(x => x.Num).ToArray();
        var oldWeapons = equipment.WeaponList; var oldStigmata = equipment.StigmataList;
        var fragments = oldAvatars.Select(x => x.Fragment).ToArray();
        int oldExp = player.User.Exp, oldHcoin = player.User.Hcoin, oldStamina = player.User.Stamina;
        var display = new ProtoReward { Exp = (uint)exp, Hcoin = (uint)crystals, Stamina = (uint)stamina };
        try
        {
            Global.Store.InTransaction(() =>
            {
                uint remaining = request.Num;
                foreach (var material in oldMaterials.Where(x => x.Id == request.MaterialId))
                { uint debit = Math.Min(material.Num, remaining); material.Num -= debit; remaining -= debit; }
                if (remaining != 0) throw new InvalidOperationException("Gift inventory changed");
                if (exp > 0) player.User.AddExp((int)exp);
                player.User.Hcoin += (int)crystals; player.User.Stamina += (int)stamina;
                foreach (var grant in grants)
                {
                    switch (grant.Kind)
                    {
                        case "material": equipment.AddMaterial(grant.Id, (int)grant.Num); break;
                        case "weapon":
                            for (uint n = 0; n < grant.Num; n++) equipment.AddWeapon(grant.Id).Level = grant.Level;
                            break;
                        case "stigmata":
                            for (uint n = 0; n < grant.Num; n++) equipment.AddStigmata(grant.Id).Level = grant.Level;
                            break;
                        case "fragment":
                            var avatar = player.AvatarList?.FirstOrDefault(x => x.AvatarId == grant.AvatarId);
                            if (avatar == null)
                            {
                                avatar = new AvatarScheme { OwnerUid = player.User.Uid, AvatarId = (uint)grant.AvatarId, Star = 0, Level = 1, SkillLists = new(), DressLists = Array.Empty<uint>() };
                                avatar.Fragment = grant.Num; Common.Database.Avatar.collection.InsertOne(avatar);
                                player.AvatarList = (player.AvatarList ?? Array.Empty<AvatarScheme>()).Append(avatar).ToArray();
                            }
                            else { avatar.Fragment += grant.Num; avatar.Save(); }
                            break;
                    }
                    display.ItemLists.Add(new RewardItemData { Id = (uint)grant.Id, Num = grant.Num, Level = grant.Level });
                }
                equipment.Save(); player.User.Save();
            });
        }
        catch (Exception error)
        {
            for (int i = 0; i < oldMaterials.Length; i++) oldMaterials[i].Num = oldCounts[i];
            equipment.MaterialList = oldMaterials; equipment.WeaponList = oldWeapons; equipment.StigmataList = oldStigmata;
            for (int i = 0; i < oldAvatars.Length; i++) oldAvatars[i].Fragment = fragments[i];
            player.AvatarList = oldAvatars; player.User.Exp = oldExp; player.User.Hcoin = oldHcoin; player.User.Stamina = oldStamina;
            Packet.c.Warn("[MaterialGift] Transaction rolled back: " + error.Message);
            return Fail(UseMaterialRsp.Retcode.Fail);
        }
        rsp.retcode = UseMaterialRsp.Retcode.Succ; rsp.GiftRewardLists.Add(display); return rsp;
    }
}
