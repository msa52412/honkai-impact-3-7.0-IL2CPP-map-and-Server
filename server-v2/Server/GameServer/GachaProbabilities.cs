using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Game;

internal static class GachaProbabilities
{
    private static string Percent(double value)=>(value*100).ToString("0.######",CultureInfo.InvariantCulture)+"%";
    private static string Key(GachaDrop drop)=>drop.Kind.Trim().ToLowerInvariant()+":"+drop.Id+":"+Math.Max(1u,drop.Num);
    private static string KindName(string kind)=>kind switch{"material"=>"材料","weapon"=>"武器","stigmata"=>"圣痕","avatarcard"=>"角色卡","avatarfragment"=>"角色碎片",_=>kind};
    private static string Label(GachaDrop drop)=>string.IsNullOrWhiteSpace(drop.Note)?drop.Kind+" #"+drop.Id:drop.Note;
    internal static GetGachaProbRsp Build(EquipmentScheme equipment,uint type)
    {
        var rsp=new GetGachaProbRsp{GachaType=type};var pool=GachaConfig.Pool(type);
        if(pool==null || pool.Drops.Any(x=>x.Id==0)) {rsp.retcode=GetGachaProbRsp.Retcode.Fail;return rsp;}
        double total=pool.Drops.Sum(x=>(double)Math.Max(0,x.Weight));
        if(total<=0 || total>int.MaxValue){rsp.retcode=GetGachaProbRsp.Retcode.Fail;return rsp;}
        uint order=0;
        foreach(var group in pool.Drops.GroupBy(x=>x.Kind.Trim().ToLowerInvariant()))
            rsp.TotalProbLists.Add(new GachaTotalProb{Name=KindName(group.Key)+"基础概率（不含保底）",Prob=Percent(group.Sum(x=>(double)Math.Max(0,x.Weight))/total),OrderValue=order++});
        order=0;
        foreach(var group in pool.Drops.GroupBy(Key))
        {
            var first=group.First();
            rsp.DetailProbLists.Add(new GachaDetailProb{Name=Label(first),Star=first.Rarity.ToString(CultureInfo.InvariantCulture),Content="当前配置的普通抽取概率；非官服概率认证，保护触发时另算。",Prob=Percent(group.Sum(x=>(double)Math.Max(0,x.Weight))/total),IsUp=group.Any(x=>x.Featured),OrderValue=order++});
        }
        var progress=GachaService.PoolProgress(equipment,pool);
        var high=pool.Drops.Where(x=>x.Featured && (!pool.CollectUpEquipment || !progress.ObtainedUpItems.Contains(x.Id))).ToList();
        var low=pool.Drops.Where(x=>x.Pity).ToList();if(low.Count==0)low=pool.Drops.Where(x=>x.Featured).ToList();
        // Show the actual NEXT draw, including a currently due guarantee, not an invented long-run rate.
        var next=pool.Drops.ToDictionary(x=>x,x=>Math.Max(0,x.Weight)/total);
        void Guarantee(List<GachaDrop> choices,double mass)
        {
            if(choices.Count==0)return;
            double sum=choices.Sum(x=>(double)Math.Max(0,x.Weight));
            foreach(var drop in choices)next[drop]+=mass*(sum>0?Math.Max(0,drop.Weight)/sum:ReferenceEquals(drop,choices[0])?1:0);
        }
        if(pool.GuaranteeUpCount>0 && high.Count>0 && progress.Up+1>=pool.GuaranteeUpCount)
        {
            foreach(var drop in pool.Drops)next[drop]=0;Guarantee(high,1);
        }
        else if(pool.GuaranteeCount>0 && low.Count>0 && progress.Low+1>=pool.GuaranteeCount)
        {
            double mass=0;foreach(var drop in pool.Drops.Where(x=>!x.Pity && !x.Featured)){mass+=next[drop];next[drop]=0;}
            Guarantee(low,mass);
        }
        if(type==20){next=DormSupply.Distribution(pool,progress,DormSupply.Load(equipment.OwnerUid),Common.Database.Avatar.AvatarsFromUid(equipment.OwnerUid));foreach(var item in rsp.DetailProbLists)item.Content="历史近似基础权重，非官服精确概率；S卡内部另按未拥有保护分配，实际下一抽见保护概率。";}
        order=0;
        foreach(var group in pool.Drops.GroupBy(Key))
        {
            var first=group.First();
            rsp.BaodiProbLists.Add(new GachaDetailProb{Name=Label(first),Star=first.Rarity.ToString(CultureInfo.InvariantCulture),Content="按当前已保存进度计算的下一抽概率；不是综合概率。",Prob=Percent(group.Sum(x=>next[x])),IsUp=group.Any(x=>x.Featured),OrderValue=order++});
        }
        rsp.retcode=GetGachaProbRsp.Retcode.Succ;return rsp;
    }
}
