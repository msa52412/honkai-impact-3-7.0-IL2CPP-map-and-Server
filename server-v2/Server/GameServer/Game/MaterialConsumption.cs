using System;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
namespace PemukulPaku.GameServer.Game;
internal static class MaterialConsumption
{
    internal static UseMaterialRsp Use(Player player, UseMaterialReq req)
    {
        lock(player.Equipment)
        {
            var rsp=new UseMaterialRsp{MaterialId=req.MaterialId,Num=req.Num};
            UseMaterialRsp Fail(UseMaterialRsp.Retcode code){rsp.retcode=code;return rsp;}
            var meta=BaseExcelReader<MaterialData,MaterialDataExcel>.GetInstance().FromId(req.MaterialId);
            var config=BaseExcelReader<MaterialUseData,MaterialUseDataExcel>.GetInstance().FromId(meta?.UseId??0);
            if(req.Num==0 || req.Num>int.MaxValue || (config!=null && config.MultiUse>0 && req.Num>config.MultiUse))return Fail(UseMaterialRsp.Retcode.InvalidNum);
            var materials=player.Equipment.MaterialList.Where(x=>x.Id==req.MaterialId).ToArray();
            if(materials.Sum(x=>(long)x.Num)<req.Num)return Fail(UseMaterialRsp.Retcode.MaterialLack);
            if(config?.UseType==2 && req.MaterialId==8402)return MiniWaferTreasure.Use(player,req,config);
            if(config?.UseType==3)return WeightedMaterialGifts.Use(player,req,config);
            if(config!=null && (config.UseType==1 || config.UseType==11))return MaterialGiftRewards.Use(player,req,config);
            if(config==null || config.UseType!=25)return Fail(UseMaterialRsp.Retcode.FeatureClosed);
            var options=config.ParaStr??Array.Empty<string>();
            var text=req.Parameter==0?options.FirstOrDefault():options.FirstOrDefault(x=>x.Split(':')[0]==req.Parameter.ToString());
            var parts=text?.Split(':');uint rewardId=0,multiplier=1;
            if(parts==null || parts.Length>2 || !uint.TryParse(parts[0],out rewardId) || (parts.Length==2 && !uint.TryParse(parts[1],out multiplier)) || multiplier==0)return Fail(UseMaterialRsp.Retcode.Fail);
            var avatarMeta=BaseExcelReader<AvatarData,AvatarDataExcel>.GetInstance().All.FirstOrDefault(x=>x.AvatarFragmentId==rewardId);
            if(avatarMeta==null)return Fail(UseMaterialRsp.Retcode.Fail);
            var previousAvatars=player.AvatarList??Array.Empty<AvatarScheme>();
            var avatar=previousAvatars.FirstOrDefault(x=>x.AvatarId==avatarMeta.AvatarId);
            bool create=avatar==null;
            if(create)avatar=new AvatarScheme{OwnerUid=player.Equipment.OwnerUid,AvatarId=(uint)avatarMeta.AvatarId,Star=0,Level=1,SkillLists=new(),DressLists=Array.Empty<uint>()};
            ulong amount=(ulong)multiplier*req.Num;
            if(amount+avatar.Fragment>uint.MaxValue)return Fail(UseMaterialRsp.Retcode.EquipmentFragmentLimit);
            // Initialize the table before the shared transaction begins.
            _=Common.Database.Avatar.collection;
            var counts=materials.Select(x=>x.Num).ToArray();uint previousFragments=avatar.Fragment;
            try
            {
                Global.Store.InTransaction(()=>
                {
                    uint remaining=req.Num;
                    foreach(var material in materials){uint debit=Math.Min(material.Num,remaining);material.Num-=debit;remaining-=debit;}
                    avatar.Fragment+=(uint)amount;
                    if(create)Common.Database.Avatar.collection.InsertOne(avatar);else avatar.Save();
                    player.Equipment.Save();
                });
            }
            catch
            {
                for(int i=0;i<counts.Length;i++)materials[i].Num=counts[i];
                avatar.Fragment=previousFragments;
                return Fail(UseMaterialRsp.Retcode.Fail);
            }
            if(create)player.AvatarList=previousAvatars.Append(avatar).ToArray();
            rsp.retcode=UseMaterialRsp.Retcode.Succ;
            var reward=new Common.Resources.Proto.RewardData();reward.ItemLists.Add(new RewardItemData{Id=rewardId,Num=(uint)amount});rsp.GiftRewardLists.Add(reward);
            return rsp;
        }
    }
}
