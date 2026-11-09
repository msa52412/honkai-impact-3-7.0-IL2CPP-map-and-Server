using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;

// MaterialUseType.RAND_GIFT = 3. Each paraStr entry is a configured rewardID:weight.
// Types with no explicit weights (including RANDOM_MATERIAL_GIFT_BOX=12) remain separate.
internal static class WeightedMaterialGifts
{
    private sealed class Rejected : Exception
    {
        internal readonly UseMaterialRsp.Retcode Code;
        internal Rejected(UseMaterialRsp.Retcode code) { Code = code; }
    }
    internal static UseMaterialRsp Use(Player player, UseMaterialReq request, MaterialUseDataExcel config, Func<int,int> draw = null)
    {
        var rsp = new UseMaterialRsp { MaterialId = request.MaterialId, Num = request.Num };
        UseMaterialRsp Fail(UseMaterialRsp.Retcode code) { rsp.retcode = code; return rsp; }
        if (config.UseType != 3 || request.Parameter != 0 || request.SimulateMagic != 0)
            return Fail(UseMaterialRsp.Retcode.Fail);
        if (request.Num == 0 || request.Num > int.MaxValue || (config.MultiUse <= 0 ? request.Num != 1 : request.Num > config.MultiUse))
            return Fail(UseMaterialRsp.Retcode.InvalidNum);
        var options = new List<(int RewardId, int Weight)>(); long total = 0;
        foreach (var text in config.ParaStr ?? Array.Empty<string>())
        {
            var parts = text?.Split(':');
            if (parts == null || parts.Length != 2 || !int.TryParse(parts[0], out int id) || id <= 0 ||
                !int.TryParse(parts[1], out int weight) || weight <= 0)
                return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            total += weight;
            if (total > int.MaxValue) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            options.Add((id, weight));
        }
        if (options.Count == 0) return Fail(UseMaterialRsp.Retcode.FeatureClosed);
        MaterialUseDataExcel Recipe(int rewardId) => new() { UseType = 1, MultiUse = config.MultiUse,
            EventId = config.EventId, UseMin = config.UseMin, UseMax = config.UseMax, ParaStr = new[] { rewardId.ToString() } };
        // Validate every possible outcome, including worst-case batch caps, before rolling.
        // Otherwise unsupported/full outcomes could be retried for free and bias the odds.
        foreach (int id in options.Select(x => x.RewardId).Distinct())
        {
            var check = MaterialGiftRewards.Use(player, request, Recipe(id), validateOnly: true);
            if (check.retcode != UseMaterialRsp.Retcode.Succ) return Fail(check.retcode);
        }
        _ = User.collection; _ = Common.Database.Avatar.collection; _ = AutoIncrement.collection;
        var equipment = player.Equipment;
        var materials = equipment.MaterialList; var counts = materials.Select(x => x.Num).ToArray();
        var weapons = equipment.WeaponList; var stigmas = equipment.StigmataList;
        var avatars = player.AvatarList ?? Array.Empty<AvatarScheme>(); var fragments = avatars.Select(x => x.Fragment).ToArray();
        int exp = player.User.Exp, crystals = player.User.Hcoin, stamina = player.User.Stamina;
        draw ??= RandomNumberGenerator.GetInt32;
        try
        {
            Global.Store.InTransaction(() =>
            {
                for (uint i = 0; i < request.Num; i++)
                {
                    int roll = draw((int)total);
                    if (roll < 0 || roll >= total) throw new InvalidOperationException("Invalid weighted draw");
                    int selected = 0;
                    foreach (var option in options)
                    {
                        if (roll < option.Weight) { selected = option.RewardId; break; }
                        roll -= option.Weight;
                    }
                    if (selected == 0) throw new InvalidOperationException("Empty weighted result");
                    var single = new UseMaterialReq { MaterialId = request.MaterialId, Num = 1, ConsumeItemList = request.ConsumeItemList };
                    var result = MaterialGiftRewards.Use(player, single, Recipe(selected));
                    if (result.retcode != UseMaterialRsp.Retcode.Succ) throw new Rejected(result.retcode);
                    rsp.GiftRewardLists.AddRange(result.GiftRewardLists);
                }
            });
        }
        catch (Exception error)
        {
            for (int i = 0; i < materials.Length; i++) materials[i].Num = counts[i];
            equipment.MaterialList = materials; equipment.WeaponList = weapons; equipment.StigmataList = stigmas;
            for (int i = 0; i < avatars.Length; i++) avatars[i].Fragment = fragments[i];
            player.AvatarList = avatars; player.User.Exp = exp; player.User.Hcoin = crystals; player.User.Stamina = stamina;
            rsp.GiftRewardLists.Clear();
            Packet.c.Warn("[WeightedGift] Batch rolled back: " + error.Message);
            return Fail(error is Rejected rejected ? rejected.Code : UseMaterialRsp.Retcode.Fail);
        }
        rsp.retcode = UseMaterialRsp.Retcode.Succ; return rsp;
    }
}
