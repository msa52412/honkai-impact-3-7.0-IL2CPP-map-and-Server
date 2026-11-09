using System;
using System.Linq;
using System.Collections.Generic;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json;

namespace PemukulPaku.GameServer.Game;

internal static class GachaTransactions
{
    internal static BuyGachaTicketRsp BuyTicket(Player player,BuyGachaTicketReq req)
    {
        lock(player.Equipment)
        {
            var rsp=new BuyGachaTicketRsp{MaterialId=req.MaterialId,Num=req.Num};
            BuyGachaTicketRsp Fail(BuyGachaTicketRsp.Retcode code){rsp.retcode=code;return rsp;}
            if(req.Num==0 || req.Num>int.MaxValue)return Fail(BuyGachaTicketRsp.Retcode.Fail);
            var prices=GachaConfig.ActivePools.Where(p=>GachaService.TicketFor(p)==req.MaterialId).Select(p=>p.TicketHcoinCost).Distinct().ToArray();
            if(prices.Length!=1 || prices[0]==0 || BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(req.MaterialId)==null)
                return Fail(BuyGachaTicketRsp.Retcode.MaterialIdError);
            long cost=(long)prices[0]*req.Num;
            if(cost>int.MaxValue || player.User.Hcoin<cost)return Fail(BuyGachaTicketRsp.Retcode.HcoinLack);
            long owned=player.Equipment.MaterialList.Where(x=>x.Id==req.MaterialId).Sum(x=>(long)x.Num);
            if(owned+req.Num>uint.MaxValue)return Fail(BuyGachaTicketRsp.Retcode.EquipmentMaterialLimit);
            _=Common.Database.User.collection;
            string snapshot=JsonConvert.SerializeObject(player.Equipment);int crystals=player.User.Hcoin;
            try
            {
                Global.Store.InTransaction(()=>
                {
                    player.User.Hcoin-=(int)cost;
                    player.Equipment.AddMaterial((int)req.MaterialId,(int)req.Num);
                    player.Equipment.Save();player.User.Save();
                });
            }
            catch(Exception error)
            {
                player.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(snapshot);player.User.Hcoin=crystals;
                Packet.c.Warn("[GachaTicket] Transaction rolled back: "+error.Message);
                return Fail(BuyGachaTicketRsp.Retcode.Fail);
            }
            rsp.retcode=BuyGachaTicketRsp.Retcode.Succ;rsp.HcoinCost=(uint)cost;return rsp;
        }
    }

    private static StigmataDataExcel[] WishItems(GachaPool pool)
    {
        if(pool==null || !pool.WishEnabled)return Array.Empty<StigmataDataExcel>();
        var ids=pool.Drops.Where(x=>x.Featured && string.Equals(x.Kind,"stigmata",StringComparison.OrdinalIgnoreCase)).Select(x=>x.Id).Distinct().ToArray();
        if(ids.Length!=3)return Array.Empty<StigmataDataExcel>();
        var table=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance();
        var rows=ids.Select(x=>table.FromId((int)x)).ToArray();
        if(rows.Any(x=>x==null || x.Rarity!=4 || x.MaxRarity!=5) || rows.Select(x=>x.SetId).Distinct().Count()!=1 || !rows.Select(x=>x.BaseType).OrderBy(x=>x).SequenceEqual(new[]{1,2,3}))return Array.Empty<StigmataDataExcel>();
        return rows;
    }
    private static string WishKey(GachaPool pool,int set)=>pool.GachaType+"/"+(string.IsNullOrWhiteSpace(pool.WishPeriodKey)?GachaService.Banner(pool):pool.WishPeriodKey)+"/"+set;

    internal static WishWellData WishDisplay(EquipmentScheme equipment,GachaPool pool)
    {
        var rows=WishItems(pool);if(rows.Length==0)return null;
        var data=new WishWellData{LeftTimes=(equipment.UsedGachaWishes?.Contains(WishKey(pool,rows[0].SetId))??false)?0u:1u,MaxWishTimes=1,SetId=(uint)rows[0].SetId,MaterialId=8004,MaterialNum=2000,EquipNeedNum=2};
        foreach(var row in rows.OrderBy(x=>x.BaseType))data.EquipLists.Add(new WishWellData.Equip{EquipId=(uint)row.Id,EquipLevel=pool.WishEquipmentLevel});
        return data;
    }

    internal static WishWellRsp Wish(Player player,WishWellReq req)
    {
        lock(player.Equipment)
        {
            var rsp=new WishWellRsp{WishId=req.WishId,GachaType=req.GachaType};
            WishWellRsp Fail(WishWellRsp.Retcode code){rsp.retcode=code;return rsp;}
            var pool=GachaConfig.Pool((uint)req.GachaType);var rows=WishItems(pool);
            if(rows.Length!=3 || !rows.Any(x=>x.Id==req.WishId) || pool.WishEquipmentLevel==0 || pool.WishEquipmentLevel>rows.First(x=>x.Id==req.WishId).MaxLv)
                return Fail(WishWellRsp.Retcode.MaterialError);
            string key=WishKey(pool,rows[0].SetId);
            if(player.Equipment.UsedGachaWishes?.Contains(key)??false)return Fail(WishWellRsp.Retcode.WishTimesLimit);
            if(req.CostEquipLists.Count!=2 || req.CostEquipLists.Any(x=>x.CostEquipNum!=1) || req.CostEquipLists.Select(x=>x.CostEquipId).Distinct().Count()!=2)
                return Fail(WishWellRsp.Retcode.EquipNumError);
            var equipment=player.Equipment;
            if(req.CostEquipLists.Any(x=>equipment.StigmataList.Count(s=>s.UniqueId==x.CostEquipId)!=1))return Fail(WishWellRsp.Retcode.EquipNumError);
            var selected=req.CostEquipLists.Select(x=>equipment.StigmataList.First(s=>s.UniqueId==x.CostEquipId)).ToArray();
            if(selected.Any(x=>x==null))return Fail(WishWellRsp.Retcode.EquipNumError);
            var table=BaseExcelReader<StigmataData,StigmataDataExcel>.GetInstance();
            var selectedRows=selected.Select(x=>table.FromId((int)x.Id)).ToArray();
            if(selectedRows.Any(x=>x==null || x.SetId!=rows[0].SetId || !rows.Any(r=>r.BaseType==x.BaseType)))return Fail(WishWellRsp.Retcode.MaterialError);
            var usedIds=new HashSet<uint>(selected.Select(x=>x.UniqueId));
            long now=Global.GetUnixInSeconds();
            if(selected.Any(x=>x.IsProtected || x.CancelProtectTime>now || x.WaitSelectRuneLists.Count>0 || x.WaitSelectRuneGroupLists.Count>0) ||
                (player.AvatarList??Array.Empty<AvatarScheme>()).Any(x=>usedIds.Contains(x.StigmataUniqueId1)||usedIds.Contains(x.StigmataUniqueId2)||usedIds.Contains(x.StigmataUniqueId3)))
                return Fail(WishWellRsp.Retcode.DeviceError);
            var heldRows=equipment.StigmataList.Select(x=>table.FromId((int)x.Id)).Where(x=>x!=null && x.SetId==rows[0].SetId).ToArray();
            int heldTypes=heldRows.Select(x=>x.BaseType).Distinct().Count();
            int targetPart=rows.First(x=>x.Id==req.WishId).BaseType;
            bool ownsTarget=heldRows.Any(x=>x.BaseType==targetPart);
            // Native IsSetConditionMatch: restricted requires2 types and a missing target, or all3 types;
            // unrestricted requires a missing target only when just one type is owned.
            if(req.IsForceSet ? heldTypes<2 || (heldTypes==2 && ownsTarget) : heldTypes<1 || (heldTypes==1 && ownsTarget))
                return Fail(WishWellRsp.Retcode.EquipNumError);
            // Native7.0 passes the preserve-one UI switch directly as is_force_set.
            if(req.IsForceSet)
            {
                var remaining=equipment.StigmataList.Where(x=>!usedIds.Contains(x.UniqueId)).Select(x=>table.FromId((int)x.Id)).Where(x=>x!=null && x.SetId==rows[0].SetId).ToArray();
                if(selectedRows.Any(x=>!remaining.Any(y=>y.BaseType==x.BaseType)))return Fail(WishWellRsp.Retcode.EquipNumError);
                var held=equipment.StigmataList.Select(x=>table.FromId((int)x.Id)).Where(x=>x!=null && x.SetId==rows[0].SetId).Select(x=>x.BaseType).Distinct().Count();
                if(held<2)return Fail(WishWellRsp.Retcode.EquipNumError);
            }
            var resonance=equipment.MaterialList.Where(x=>x.Id==8004).ToArray();
            if(resonance.Sum(x=>(long)x.Num)<2000)return Fail(WishWellRsp.Retcode.MaterialNotEnough);
            _=AutoIncrement.collection;
            string snapshot=JsonConvert.SerializeObject(equipment);
            try
            {
                Global.Store.InTransaction(()=>
                {
                    uint remaining=2000;foreach(var material in resonance){uint debit=Math.Min(remaining,material.Num);material.Num-=debit;remaining-=debit;}
                    equipment.StigmataList=equipment.StigmataList.Where(x=>!usedIds.Contains(x.UniqueId)).ToArray();
                    var item=equipment.AddStigmata((int)req.WishId);item.Level=pool.WishEquipmentLevel;
                    equipment.UsedGachaWishes??=new();equipment.UsedGachaWishes.Add(key);
                    equipment.Save();
                });
            }
            catch(Exception error)
            {
                player.Equipment=JsonConvert.DeserializeObject<EquipmentScheme>(snapshot);
                Packet.c.Warn("[WishWell] Transaction rolled back: "+error.Message);
                return Fail(WishWellRsp.Retcode.Fail);
            }
            rsp.retcode=WishWellRsp.Retcode.Succ;
            var reward=new Common.Resources.Proto.RewardData();reward.ItemLists.Add(new RewardItemData{Id=req.WishId,Level=pool.WishEquipmentLevel,Num=1});rsp.RewardLists.Add(reward);
            // Wishing is not a supply draw: do not modify its pity or collected-UP set.
            return rsp;
        }
    }
}
