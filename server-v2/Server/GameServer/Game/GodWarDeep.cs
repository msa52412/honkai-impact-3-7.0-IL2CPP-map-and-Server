using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

// Data-backed offline reconstruction. Route selection, random weights, shop
// pricing and free reroll limits are compatibility rules, not official tables.
internal static partial class GodWarProgression
{
    private static readonly Lazy<JArray> DeepRoutes=new(()=>File.Exists("godwar-offline-routes.json")?JArray.Parse(File.ReadAllText("godwar-offline-routes.json")):new JArray());
    public sealed class DeepState
    {
        public string Phase {get;set;}="site";
        public uint RoomRulesVersion {get;set;}
        public uint ExclusiveArmorLevel {get;set;}
        public bool OpeningSignetDialoguePending {get;set;}
        public uint RewardRulesVersion {get;set;}
        public bool EliteTimedOut {get;set;}
        public uint SelectedBoss {get;set;}
        public HashSet<uint> WeeklyShopVisits {get;set;}=new();
        public bool RegularBossCleared {get;set;}
        public uint RoomCurrencyVersion {get;set;}
        public uint RoomCoinBase {get;set;}
        public uint RewardChoicesRemaining {get;set;}
        public bool ElysiaPortalUsed {get;set;}
        public bool SlotPaid {get;set;}
        public bool SlotJackpotPending {get;set;}
        public bool SlotExploded {get;set;}
        public uint SlotResetMask {get;set;}
        public uint SlotSpinsInRoom {get;set;}
        public long SlotLastCostAt {get;set;}
        public Dictionary<uint,uint> CombatOverall {get;set;}=new();
        public Dictionary<uint,uint> EnhancedExclusives {get;set;}=new();
        public uint[] Sites {get;set;}=Array.Empty<uint>();
        public uint[] Stages {get;set;}=Array.Empty<uint>();
        public int Floor {get;set;}
        public uint PendingStage {get;set;}
        public long PendingSince {get;set;}
        public uint TotalTime {get;set;}
        public uint Coins {get;set;}
        public uint NextSuit {get;set;}=8;
        public uint CoreSuit {get;set;}
        public bool CoreFromShop {get;set;}
        public uint Revision {get;set;}
        public Dictionary<uint,uint> Buffs {get;set;}=new();
        public uint[] Candidates {get;set;}=Array.Empty<uint>();
        public uint[] Portals {get;set;}=Array.Empty<uint>();
        public bool BuffRefreshed {get;set;}
        public uint ShopRulesVersion {get;set;}
        public uint ShopRefreshCount {get;set;}
        public uint AvatarShopPercent {get;set;}=100;
        public bool ShopRefreshed {get;set;}
        public HashSet<uint> ShopRequests {get;set;}=new();
        public List<DeepGoods> Goods {get;set;}=new();
        public uint LastScore {get;set;}
        public uint PendingVictoryCoins {get;set;}
        public bool MerchantOpen {get;set;}
        public bool ShopRecoverUsed {get;set;}
        public uint StockVersion {get;set;}
        public uint PurchaseSlotsBonus {get;set;}
        public uint UpgradeSlotsBonus {get;set;}
        public uint EffectsVersion {get;set;}
        public uint YieldRulesVersion {get;set;}
        public uint StoneYieldBonus {get;set;}
        public uint PurchasePercent {get;set;}=100;
        public uint UpgradePercent {get;set;}=100;
        public uint PortalRefreshLimit {get;set;}
        public uint PortalRefreshSigilBonus {get;set;}
        public uint SignetDialoguePlot {get;set;}
        public uint SignetDialogueFollow {get;set;}
        public bool StartCoinHandled {get;set;}
        public uint CoinTalentRulesVersion {get;set;}
        public uint NormalStartCoinBonus {get;set;}
        public uint EliteStartCoinBonus {get;set;}
        public uint BossStartCoinBonus {get;set;}
        public bool RoomTalentCoinHandled {get;set;}
    }
    public sealed class DeepGoods {public uint Index {get;set;}public uint Buff {get;set;}public uint BaseCost {get;set;}public uint Cost {get;set;}public bool Upgrade {get;set;}public bool Sold {get;set;}}
    internal sealed class DeepEvents
    {
        internal TriggerGodWarEventRsp Reply;
        internal List<GodWarEventInfo> Events=new();
        internal bool Changed;
        internal bool InventoryChanged;
    }
    private static JToken DeepStep(ChallengeRun run)=>Table("GodWarPunishStep").Single(x=>(uint)x["PunishStepConfigID"]==(uint)Table("GodWarAvatarTaleSchedule").Single(a=>(uint)a["AvatarScheduleID"]==run.AvatarScheduleId)["PunishStepID"]&&(uint)x["PunishStep"]==run.Challenge.StepLevel);
    private static uint Punish(ChallengeRun run)=>checked((uint)(run.Challenge.PunishBuffIdLists??Array.Empty<uint>()).Sum(id=>(long)(uint)Table("GodWarPunishBuff").Single(x=>(uint)x["PunishBuffID"]==id)["PunishLevelCost"]));
    private static GodWarEventInfo Event(uint id,params uint[] args)
    {
        var row=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==id);
        return new GodWarEventInfo{EventId=id,EventType=(uint)row["EventType"],EventParamLists=args,ExtraParamLists=Array.Empty<uint>()};
    }
    private static uint Leaf(uint root,uint type)
    {
        var seen=new HashSet<uint>();var q=new Queue<uint>();q.Enqueue(root);
        while(q.Count>0&&seen.Count<1024){uint id=q.Dequeue();if(!seen.Add(id))continue;var row=Table("GodWarEvent").FirstOrDefault(x=>(uint)x["EventID"]==id);if(row==null)continue;if((uint)row["EventType"]==type)return id;if((uint)row["EventType"] is 99 or 100)foreach(uint next in row["ParamsVar"].Values<uint>())q.Enqueue(next);}
        throw new InvalidOperationException("No authored GodWar event leaf "+root+"/"+type);
    }
    // Versioned new routes only: existing in-progress routes keep their issued stages.
    private static bool IsDeepRest(ChallengeRun run)=>run.Deep is {} d&&(d.RoomRulesVersion>=4?CurrentWeeklyShop(d)!=null:d.Sites[d.Floor]==6018&&((d.RoomRulesVersion is 1 or 2&&d.Stages[d.Floor]==175653)||(d.RoomRulesVersion==3&&d.Stages[d.Floor]==175650)));
    // The actual weekly Lua deliberately skips BattleWaves when CurFloor==1.
    // Its preparation room has a legitimate zero battle timer on gate exit.
    private static bool IsDeepOpening(ChallengeRun run)=>run.Deep is {} d&&d.Floor==0&&
        (string)Table("StageData_Main").Single(x=>(uint)x["levelId"]==d.Stages[0])["luaFile"]=="Lua/Levels/GodWar/GodWar_WeeklyChallenge.lua";
    private static JToken Site(DeepState d)=>Table("GodWarSite").Single(x=>(uint)x["SiteID"]==d.Sites[d.Floor]);
    private static JToken Stage(DeepState d)=>Table("GodWarEventStage").Single(x=>(uint)x["StageID"]==d.Stages[d.Floor]);
    private static GodWarEventInfo Enter(ChallengeRun run,uint tale)=>Event(tale==30?5600050u:5600040u+run.Challenge.StepLevel,run.Deep.Sites[run.Deep.Floor]);
    private static GodWarEventInfo Fight(DeepState d)=>Event(CurrentWeeklyShop(d)?.StageEvent??(d.RoomRulesVersion>=2&&d.Sites[d.Floor]==6017&&d.SelectedBoss>0?DeepBosses.Single(x=>x.Selector==d.SelectedBoss).StageEvent:Leaf((uint)Site(d)["SiteEvent"],1)),d.Stages[d.Floor]);
    private static IEnumerable<uint> EligibleBuffs(ChallengeRun run,uint suit,bool allowUpgrade=false)=>EligibleSignets(run,suit,allowUpgrade);
    private static uint[] DrawUniform(IEnumerable<uint> source, int count)
    {
        var pool = source.Distinct().ToList();
        var selected = new List<uint>(Math.Min(count, pool.Count));
        while (selected.Count < count && pool.Count > 0)
        {
            int index = Random.Shared.Next(pool.Count);
            selected.Add(pool[index]);
            pool.RemoveAt(index);
        }
        selected.Sort();
        return selected.ToArray();
    }

    private static void Candidates(ChallengeRun run,uint[] previous=null)
    {
        var d=run.Deep;d.Revision++;
        if(OfferCore(run))return;
        var eligible = EligibleBuffs(run, d.NextSuit).ToArray();
        if (d.NextSuit != 0 && d.NextSuit != 8)
        {
            var rows = PlayableBuffRows();
            var ownedCore = rows.FirstOrDefault(row => (uint)row["BuffSuit"] == d.NextSuit &&
                (uint)row["BuffQuality"] == 3 && d.Buffs.ContainsKey((uint)row["BuffID"]));
            uint group = (uint?)ownedCore?["BuffGroup"] ?? 0;
            var pool = rows.Where(row => (uint)row["BuffSuit"] == d.NextSuit &&
                (uint)row["BuffGroup"] == group && (uint)row["BuffID"] != 100001)
                .Select(row => (uint)row["BuffID"]).Intersect(eligible).ToArray();
            if (pool.Length > 0) eligible = pool;
        }
        if(eligible.Length==0)eligible=EligibleBuffs(run,0).ToArray();
        if(previous==null)
            d.Candidates=DrawUniform(eligible,3);
        else
        {
            var fresh=DrawUniform(eligible.Except(previous),3);
            d.Candidates=fresh.Concat(DrawUniform(eligible.Intersect(previous),3-fresh.Length)).OrderBy(id=>id).ToArray();
        }
        d.Phase=d.Candidates.Length==0?"portal":"buff";
        if(d.Phase=="portal")Portals(run);else PrepareSignetDialogue(run);
    }
    private static uint PortalSuit(uint id)
    {
        // Ego doors use the special icon, not the ordinary suit icon.
        if(id is 108 or 1008 or 2008 or RegularBossDoor)return 8;
        var icon=(string)Table("GodWarTeleport").Single(x=>(uint)x["TeleportID"]==id)["TeleportIcon"];
        return (uint?)Table("GodWarBuffSuit").FirstOrDefault(x=>(string)x["SuitIcon"]==icon)?["SuitID"]??0;
    }

    private static void ShopGoods(ChallengeRun run)=>BuildDeepShopGoods(run,false);
    private static void BuildDeepShopGoods(ChallengeRun run,bool refresh)
    {
        var d=run.Deep;d.Revision++;d.MerchantOpen=true;
        int purchaseCount=d.Goods.Count(g=>!g.Upgrade);
        var retained=refresh&&d.ShopRulesVersion>=1?d.Goods.Where(g=>g.Upgrade).ToArray():Array.Empty<DeepGoods>();d.Goods.Clear();d.Goods.AddRange(retained);
        void Add(uint id){bool upgrade=d.Buffs.ContainsKey(id);uint cost=DeepGoodsBaseCost(d,id,upgrade);d.Goods.Add(new DeepGoods{Index=checked(d.Revision*64+(uint)d.Goods.Count+1),Buff=id,BaseCost=cost,Cost=DeepPrice(d,cost,upgrade),Upgrade=upgrade});}
        foreach(uint id in DrawUniform(EligibleShopSignets(run).Where(id=>!d.Goods.Any(g=>g.Buff==id)&&(!refresh||d.ShopRulesVersion==0||!d.Buffs.ContainsKey(id))),refresh&&d.ShopRulesVersion>=1?purchaseCount:6))Add(id);
        if(!refresh||d.ShopRulesVersion==0)foreach(uint id in DrawUniform(EligibleShopSignets(run).Where(id=>!d.Buffs.ContainsKey(id)&&!d.Goods.Any(g=>g.Buff==id)),checked((int)d.PurchaseSlotsBonus)))Add(id);
        if(!refresh||d.ShopRulesVersion==0)foreach(uint id in DrawUniform(EligibleShopSignets(run).Where(id=>d.Buffs.ContainsKey(id)&&!d.Goods.Any(g=>g.Buff==id)),checked((int)d.UpgradeSlotsBonus)))Add(id);
    }
    private static List<GodWarEventInfo> AfterEvents(ChallengeRun run)
    {
        var d=run.Deep;
        if(d.Phase=="buff"&&d.OpeningSignetDialoguePending)return new(){Event(8602,54541)};
        if(d.Phase=="buff")return new(){d.SignetDialoguePlot!=0?SignetDialogueEvent(run):SelectionEvent(run)};
        if(d.Phase=="portal")return new(){Event(10011,DeepCurrentPortals(run))};
        if(d.Phase is "complete" or "failed")return new(){Event(99999999)};
        return new();
    }
    internal static DeepEvents TriggerDeep(Player p,TriggerGodWarEventReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var result=new DeepEvents{Reply=new TriggerGodWarEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,EventIdLists=Array.Empty<uint>(),retcode=TriggerGodWarEventRsp.Retcode.Fail}};
            if(req.GodWarId!=1){result.Reply.retcode=TriggerGodWarEventRsp.Retcode.FeatureClose;return result;}
            var ids=req.EventIdLists??Array.Empty<uint>();if(ids.Length!=1)return result;
            var s=Load(p.User);if(!s.Runs.TryGetValue(req.TaleId,out var run)){result.Reply.retcode=TriggerGodWarEventRsp.Retcode.FeatureClose;return result;}
            if(!RepairDeepOffers(p,s,run,out bool offersChanged))return result;result.Changed=offersChanged;
            uint id=ids[0],root=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==req.TaleId)["StartEvent"];
            if(id==root&&req.TriggerType==0)
            {
                if(run.Deep==null)
                {
                    var route=DeepRoutes.Value.SingleOrDefault(x=>(uint)x["TaleId"]==req.TaleId&&(uint)x["Step"]==run.Challenge.StepLevel);
                    if(route==null){result.Reply.retcode=TriggerGodWarEventRsp.Retcode.FeatureClose;return result;}
                    var d=new DeepState{ShopRulesVersion=(uint?)route["ShopRulesVersion"]??0,RoomRulesVersion=(uint?)route["RoomRulesVersion"]??0,RewardRulesVersion=(uint?)route["RewardRulesVersion"]??0,Sites=route["Sites"].Values<uint>().ToArray(),Stages=route["Stages"].Values<uint>().ToArray()};
                    if(d.Sites.Length==0||d.Sites.Length!=d.Stages.Length||d.Sites.Any(x=>!Table("GodWarSite").Any(y=>(uint)y["SiteID"]==x))||d.Stages.Any(x=>!Table("GodWarEventStage").Any(y=>(uint)y["StageID"]==x)))return result;
                    if(!Commit(p,s,()=>{run.Deep=d;PrepareDeepEffects(s,run,req.TaleId,true);}))return result;result.Changed=true;
                }
                if(run.Deep.Phase is "site" or "ready")result.Events.Add(Enter(run,req.TaleId));
                else if(run.Deep.Phase is "fighting" or "buff" or "portal")
                {
                    // A start request from the lobby enters a new native actor.
                    // Its local coin delta starts at zero, unlike an in-room sync.
                    if(!RebaseDeepCurrency(p,s,run))return result;
                    result.Changed=true;result.Events.Add(Fight(run.Deep));
                }
                else if(run.Deep.Phase is "complete" or "failed"){if(run.Deep.PendingStage!=0)result.Events.Add(Fight(run.Deep));else result.Events.AddRange(AfterEvents(run));}
                else return result;
            }
            else if(run.Deep is { } d)
            {
                if(req.TriggerType==3&&id is 551003 or 551004)
                {
                    // These are Lua combat checkpoints, never economy or route events.
                    if(!SaveDeepCombatOverall(p,s,run,id,req.TriggerParamLists,out bool changed))return result;
                    result.Changed|=changed;
                }
                else if(req.TriggerType==0&&id is 551001 or 551002)
                {
                    if((req.TriggerParamLists?.Length??0)!=0||!TriggerDeepPassiveCoin(p,s,run,req.TaleId,id,result))return result;
                }
                else if(req.TriggerType==0&&id==5600070)
                {
                    if(d.Phase!="portal"||d.PendingStage!=d.Stages[d.Floor]||!IsDeepBossChoice(run))return result;
                    // Authored completion root -> stage exit. Only the subsequent
                    // StageEnd confirms the ordinary clear, never this request.
                    result.Events.Add(Event(99999999));
                }
                else if(req.TriggerType==0&&IsDeepSupportEvent(id))
                {
                    if((req.TriggerParamLists?.Length??0)!=0||!TriggerDeepSupport(p,s,run,id,result))return result;
                }
                else if(req.TriggerType==0&&IsDeepShopInteraction(id))
                {
                    // The native counter requests GodWarEventProp.PropEvent before
                    // it can send GetGodWarTaleShopReq. Resolve that interaction to
                    // the authored shop UI event, without charging or rebuilding stock.
                    if((req.TriggerParamLists?.Length??0)!=0||d.Phase!="portal"||!d.MerchantOpen||d.PendingStage!=d.Stages[d.Floor])return result;
                    result.Events.Add(Event(80000101,1));
                }
                else if(req.TriggerType==0&&id is 800002 or 800003)
                {
                    if(d.Phase!="portal"||!d.MerchantOpen)return result;
                    if(id==800003)
                    {
                        var actor=run.Challenge.AvatarLists.Single();
                        if(d.ShopRecoverUsed||actor.HpPercent==0)return result;
                        var heal=Table("GodWarEvent").Single(x=>(uint)x["EventID"]==80000301);
                        uint amount=(uint)heal["ParamsVar"][0];
                        if((uint)heal["EventType"]!=34||amount==0||amount>100)return result;
                        if(!Commit(p,s,()=>{actor.HpPercent=Math.Min(100,actor.HpPercent+amount);d.ShopRecoverUsed=true;}))return result;
                        result.Changed=true;result.Events.Add(Event(80000301,amount));
                    }
                    else {if(!DeepCanChangeLineup(run))return result;result.Events.Add(Event(80000201));}
                }
                else if(req.TriggerType==0&&id==(uint)Site(d)["SiteEvent"])
                {
                    if(d.Phase=="site"){if(!CommitRunState(s,()=>d.Phase="ready"))return result;result.Changed=true;}
                    if(d.Phase is not ("ready" or "fighting" or "buff" or "portal"))return result;
                    // Native lobby resume sends the current SiteEvent, not
                    // necessarily the tale's StartEvent. Rebase its fresh actor
                    // exactly as the root resume path does.
                    if(d.PendingStage!=0)
                    {
                        if(!RebaseDeepCurrency(p,s,run))return result;
                        result.Changed=true;
                    }
                    result.Events.Add(Fight(d));
                }
                else if(req.TriggerType==0&&d.PendingStage==d.Stages[d.Floor]&&id==(uint)Stage(d)["StartCoinEventID"]&&id!=0&&d.Phase is "fighting" or "buff" or "portal")
                {
                    // The client requests the authored start-drop group. The
                    // visual coin notification uses [currency ID, amount].
                    // Actual run balance is credited once on the victory report.
                    if(!d.StartCoinHandled)
                    {
                        if(!TryDeepStartTalentCoins(run,req.TaleId,id,out var bonuses))return result;
                        uint extra=checked((uint)bonuses.Sum(x=>(long)x.EventParamLists[1]));
                        if(!Commit(p,s,()=>{d.StartCoinHandled=true;d.RoomTalentCoinHandled=true;d.Coins=checked(d.Coins+extra);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+extra);}))return result;
                        result.Changed=true;uint coin=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==req.TaleId)["RogueCoin"];
                        // Existing base40 income remains an explicit compatibility
                        // allowance; authored talent gifts below are independent.
                        result.Events.Add(Event(Leaf(id,44),coin,40));result.Events.AddRange(bonuses);
                    }
                }
                else if(req.TriggerType==1&&d.PendingStage==d.Stages[d.Floor]&&(id==(uint)Stage(d)["FollowEventID"]||IsDeepEliteTimeout(run,id)))
                {
                    if(IsDeepRest(run))
                    {
                        if(d.Phase!="portal")return result;
                    }
                    else if(d.Phase=="fighting")
                    {
                        long elapsed=Clock().ToUnixTimeSeconds()-d.PendingSince;if(elapsed<0||elapsed>21600)return result;
                        uint baseScore=(uint)Stage(d)["Score"],punish=Punish(run);
                        uint ratio=(uint?)Table("GodWarPunishScore").SingleOrDefault(x=>(uint)x["PunishLevel"]==punish)?["PunishExtraScore"]??100;
                        uint score=checked((uint)((ulong)baseScore*ratio/100));
                        uint roomCoins=d.StartCoinHandled||(d.RoomTalentCoinHandled&&(uint)Stage(d)["StartCoinEventID"]!=0)?40u:0u;
                        uint merchantBonus = DeepMerchantPhilosophyBonus(d, roomCoins);
                        uint victoryCoins = checked(roomCoins + merchantBonus);
                        if(!Commit(p,s,()=>{d.EliteTimedOut=IsDeepEliteTimeout(run,id);d.LastScore=score;d.PendingVictoryCoins=victoryCoins;run.Challenge.ChallengeScore=checked(run.Challenge.ChallengeScore+score);d.Coins=checked(d.Coins+victoryCoins);run.Challenge.GainCoinNum=checked(run.Challenge.GainCoinNum+victoryCoins);
                            if(d.Floor==d.Sites.Length-1)d.Phase="complete";else if(IsDeepBossChoice(run))Portals(run);else{d.RewardChoicesRemaining=IsDeepOpening(run)?DeepOpeningRewardCount(run):((d.RoomRulesVersion>=3||d.RewardRulesVersion>=1)&&IsDeepTimedElite(run)&&!d.EliteTimedOut?2u:1u);Candidates(run);d.OpeningSignetDialoguePending=IsDeepOpening(run)&&d.Phase=="buff";}} ))return result;
                        result.Changed=true;
                        if (merchantBonus > 0 && d.RoomCurrencyVersion == 1)
                            result.Events.Add(DeepCoinEvent(req.TaleId, 551002, merchantBonus));
                    }
                    else if(d.Phase is not ("buff" or "portal" or "complete"))return result;
                    result.Events.AddRange(AfterEvents(run));
                }
                else if(req.TriggerType==1&&id==(uint)Stage(d)["FailEventID"]&&id!=(uint)Stage(d)["FollowEventID"]&&d.Phase=="fighting")
                {
                    if(!Commit(p,s,()=>{d.Phase="failed";run.Challenge.IsChallengeFail=true;}))return result;result.Changed=true;result.Events.AddRange(AfterEvents(run));
                }
                else if(req.TriggerType==0&&id==500108&&d.Phase=="buff"&&IsDeepOpening(run))
                {
                    if(d.OpeningSignetDialoguePending&&!CommitRunState(s,()=>{d.OpeningSignetDialoguePending=false;ClearSignetDialogue(d);}))return result;
                    result.Events.Add(SelectionEvent(run));
                }
                else if(req.TriggerType==0&&d.Phase=="buff"&&d.SignetDialoguePlot!=0&&id==d.SignetDialogueFollow)
                {
                    if(!CommitRunState(s,()=>ClearSignetDialogue(d)))return result;
                    result.Events.Add(SelectionEvent(run));
                }
                else if(req.TriggerType==0&&id==(uint)Site(d)["BuffEvent"]&&d.Phase=="buff")result.Events.AddRange(AfterEvents(run));
                else return result;
            }
            else return result;
            result.Reply.retcode=0;result.Reply.EventIdLists=ids;return result;
        }
    }
    // The exported Tale.StageList is stale and omits modern 175xxx stages.
    // Route the complete event-stage catalogue away from generic drop granting.
    internal static bool HandlesDeep(uint stage)=>Table("GodWarEventStage").Any(x=>(uint)x["StageID"]==stage)&&!HandlesShallow(stage)&&!HandlesPlotBattle(stage);
    internal static StageBeginRsp BeginDeep(Player p,StageBeginReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageBeginRsp{StageId=req.StageId,retcode=StageBeginRsp.Retcode.Fail};var s=Load(p.User);
            var run=s.Runs.Values.SingleOrDefault(x=>x.Deep!=null&&x.Deep.Stages[x.Deep.Floor]==req.StageId&&(x.Deep.Phase is "ready" or "fighting" or "buff" or "portal" || (x.Deep.Phase is "complete" or "failed" && x.Deep.PendingStage==req.StageId)));
            if(run==null)return rsp;var d=run.Deep;var ids=Ids(req.AvatarIdLists);uint main=run.Challenge.AvatarLists.Single().AvatarId;
            var allowed=(run.Challenge.SupportAvatarIdLists??Array.Empty<uint>()).Append(main).ToHashSet();
            if(ids.Distinct().Count()!=ids.Length||ids.Length>3||ids.Any(x=>!allowed.Contains(x))||(ids.Length>0&&!ids.Contains(main))){rsp.retcode=StageBeginRsp.Retcode.AvatarError;return rsp;}
            if(d.PendingStage==req.StageId)
            {
                long elapsed=Clock().ToUnixTimeSeconds()-d.PendingSince;if(elapsed<0)return rsp;
                if(elapsed>21600&&!CommitRunState(s,()=>d.PendingSince=Clock().ToUnixTimeSeconds()))return rsp;
                rsp.retcode=0;return rsp;
            }
            if(!CommitRunState(s,()=>{d.RoomCoinBase=d.Coins;d.RoomCurrencyVersion=1;d.PendingStage=req.StageId;d.PendingSince=Clock().ToUnixTimeSeconds();if(d.Phase=="ready"){if(IsDeepRest(run)){Portals(run);ShopGoods(run);}else d.Phase="fighting";}}))return rsp;rsp.retcode=0;return rsp;
        }
    }
    internal static StageEndRsp EndDeep(Player p,StageEndReqBody req,out ResetGodWarChallengeRsp settlement)
    {
        settlement=null;
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new StageEndRsp{StageId=req.StageId,EndStatus=req.EndStatus,retcode=StageEndRsp.Retcode.Fail};var s=Load(p.User);
            var active=s.Runs.SingleOrDefault(x=>x.Value.Deep?.PendingStage==req.StageId);
            var run=active.Value;if(run==null)return rsp;var d=run.Deep;
            long elapsed=Clock().ToUnixTimeSeconds()-d.PendingSince;if(elapsed<0||elapsed>21600||req.StagePassTime>21600){rsp.retcode=StageEndRsp.Retcode.StageTimeError;return rsp;}
            bool abandon=req.GodWarStatus?.IsAbandon??false;uint portal=req.GodWarStatus?.TeleportId??0;
            var hp=req.AvatarLists.FirstOrDefault(x=>x.AvatarId==run.Challenge.AvatarLists.Single().AvatarId);
            if(hp!=null&&hp.HpPercent>100)return rsp;
            // The pause settlement action reports a win with IsAbandon.
            // A plain exit can carry IsAbandon too, but uses StageExit.
            if(req.EndStatus==StageEndStatus.StageWin&&abandon&&portal==0)
            {
                var previousTime=d.TotalTime;
                d.TotalTime=checked(previousTime+req.StagePassTime);
                var result=SettleDeep(p,s,active.Key,run);
                if(result.retcode!=ResetGodWarChallengeRsp.Retcode.Succ)
                {
                    d.TotalTime=previousTime;
                    return rsp;
                }
                settlement=result;
                rsp.GodWarChallengeScore=result.ChallengeScore;
                rsp.retcode=0;
                return rsp;
            }
            if(req.EndStatus==StageEndStatus.StageWin&&!abandon)
            {
                if(req.StagePassTime==0&&!IsDeepRest(run)&&!IsDeepOpening(run)){rsp.retcode=StageEndRsp.Retcode.StageTimeError;return rsp;}
                if(d.Phase=="complete"){}
                else if(d.Phase!="portal"||((portal!=0||!IsDeepBossChoice(run))&&!DeepCurrentPortals(run).Contains(portal)))return rsp;
            }
            if(req.EndStatus==StageEndStatus.StageWin&&!abandon&&portal==100&&d.RoomRulesVersion>=4&&NextWeeklyShop(d)==null)return rsp;
            if(!CommitRunState(s,()=>{
                d.TotalTime=checked(d.TotalTime+req.StagePassTime);d.PendingStage=0;
                // ChallengeModeAvatar is also used for sparse battle summaries.
                // An omitted hp_percent reads as zero; it is not a death report.
                // Native GodWarUtil.SetLevelEndData clamps living HP to 1..100.
                // Preserve confirmed health for absent/zero living summaries.
                if(hp?.IsDie==true)run.Challenge.AvatarLists.Single().HpPercent=0;
                else if(hp!=null&&hp.ShouldSerializeHpPercent()&&hp.HpPercent>0)
                    run.Challenge.AvatarLists.Single().HpPercent=hp.HpPercent;
                // Native pause/leave reports StageExit+IsAbandon too. The explicit
                // ResetGodWarChallenge request is the authoritative whole-run settle.
                if(req.EndStatus==StageEndStatus.StageExit&&hp?.IsDie!=true)
                {
                    if(d.Phase=="fighting")d.Phase="ready";
                    run.Challenge.IsChallengeFail=false;return;
                }
                // An explicit actor death is terminal even when StageExit omits
                // IsAbandon. Sparse living pause summaries still resume above.
                if(hp?.IsDie==true||abandon||req.EndStatus==StageEndStatus.STAGEAllDEAD)
                {
                    run.Challenge.ChallengeScore-=Math.Min(run.Challenge.ChallengeScore,d.LastScore);d.LastScore=0;
                    d.Coins -= Math.Min(d.Coins, d.PendingVictoryCoins);
                    run.Challenge.GainCoinNum -= Math.Min(run.Challenge.GainCoinNum, d.PendingVictoryCoins);
                    d.PendingVictoryCoins = 0;
                    if(req.EndStatus==StageEndStatus.STAGEAllDEAD)run.Challenge.AvatarLists.Single().HpPercent=0;
                    d.Phase="failed";run.Challenge.IsChallengeFail=true;return;
                }
                if(req.EndStatus==StageEndStatus.StageWin&&!IsDeepRest(run)&&!IsDeepOpening(run)&&
                    (run.Challenge.SupportAvatarIdLists??Array.Empty<uint>()).Contains(204u))
                    CountLightningSupportWin(s);
                if(d.Phase=="portal"&&IsDeepBossChoice(run)&&!abandon&&req.EndStatus is StageEndStatus.StageWin or StageEndStatus.StageExit)
                {
                    // Follow alone is provisional. Only this authorized StageEnd
                    // confirms the ordinary boss, even when the optional room is skipped.
                    d.RegularBossCleared=true;d.LastScore=0;d.PendingVictoryCoins=0;
                    if(req.EndStatus==StageEndStatus.StageWin&&portal==0){d.Phase="complete";d.LastScore=0;return;}
                }
                if(req.EndStatus!=StageEndStatus.StageWin){if(d.Phase=="fighting")d.Phase="ready";return;}
                if(d.Phase=="complete"){d.PendingVictoryCoins=0;return;}
                if(IsDeepBossChoice(run)){var boss=DeepBosses.Single(x=>x.Door==portal);d.Stages[d.Floor+1]=boss.Stage;d.SelectedBoss=boss.Selector;}
                if(portal==100&&d.RoomRulesVersion>=4){var shop=NextWeeklyShop(d)??throw new InvalidOperationException("Shop exit no longer eligible");d.Stages[d.Floor+1]=shop.Stage;d.WeeklyShopVisits.Add(shop.Period);}
                d.OpeningSignetDialoguePending=false;ClearSignetDialogue(d);d.NextSuit=PortalSuit(portal);d.Floor++;d.Phase="site";d.Candidates=Array.Empty<uint>();d.Portals=Array.Empty<uint>();d.Goods.Clear();d.MerchantOpen=false;d.ShopRecoverUsed=false;d.BuffRefreshed=false;d.ShopRefreshed=false;d.ShopRefreshCount=0;d.LastScore=0;d.PendingVictoryCoins=0;d.StartCoinHandled=false;d.RoomTalentCoinHandled=false;d.RewardChoicesRemaining=0;d.EliteTimedOut=false;d.ElysiaPortalUsed=false;d.SlotPaid=false;d.SlotResetMask=0;d.SlotSpinsInRoom=0;d.SlotLastCostAt=0;
            }))return rsp;
            Packet.c.Log($"[GodWar] end stage={req.StageId} status={req.EndStatus} abandon={abandon} hpPresent={hp?.ShouldSerializeHpPercent()??false} hp={hp?.HpPercent} dead={hp?.IsDie} savedHp={run.Challenge.AvatarLists.Single().HpPercent} phase={d.Phase} floor={d.Floor}");
            rsp.GodWarChallengeScore=run.Challenge.ChallengeScore;
            rsp.retcode=0;return rsp;
        }
    }
    private static void ApplyDeep(ChallengeRun run,GodWarTale tale)
    {
        if(run.Deep is not {} d)return;
        tale.CurSiteId=d.Sites[d.Floor];tale.SiteLists.Clear();tale.SiteLists.AddRange(d.Sites.Select(x=>new GodWarSite{SiteId=x}));
        tale.BuffLists.Clear();tale.BuffLists.AddRange(d.Buffs.OrderBy(x=>x.Key).Select(x=>new GodWarBuff{BuffId=x.Key,BuffLevel=x.Value}));
        foreach(var pair in d.CombatOverall.OrderBy(x=>x.Key))
        {
            if(!IsDeepCombatOverall(pair.Key,pair.Value))continue;
            tale.OverallLists.RemoveAll(x=>x.OverallId==pair.Key);
            tale.OverallLists.Add(new GodWarOverall{OverallId=pair.Key,OverallVal=pair.Value});
        }
        // 100 is the native NPC/support hard-level code, never the wallet.
        tale.OverallLists.RemoveAll(x=>x.OverallId is 100 or 101 or 102);
        tale.OverallLists.Add(new GodWarOverall{OverallId=100,OverallVal=DeepHardLevel(run,tale.TaleId)});
        tale.OverallLists.Add(new GodWarOverall{OverallId=101,OverallVal=d.NextSuit});
        tale.OverallLists.Add(new GodWarOverall{OverallId=102,OverallVal=d.EliteTimedOut?1u:0u});
        if(d.SelectedBoss>0){tale.OverallLists.RemoveAll(x=>x.OverallId==611);tale.OverallLists.Add(new GodWarOverall{OverallId=611,OverallVal=d.SelectedBoss});}
    }
    internal static GetEquipmentDataRsp DeepCurrencySnapshot(Player p)
    {
        var response=new GetEquipmentDataRsp{retcode=GetEquipmentDataRsp.Retcode.Succ,IsAll=false};
        ApplyDeepCurrency(p,response);return response;
    }
    internal static void ApplyDeepCurrency(Player p,GetEquipmentDataRsp response)
    {
        lock(Gate)
        {
            var state=Load(p.User);
            foreach(uint tale in new uint[]{20,30})
            {
                uint currency=(uint)Table("GodWarTale").Single(x=>(uint)x["TaleID"]==tale)["RogueCoin"];
                response.MaterialLists.RemoveAll(x=>x.Id==currency);
                uint coins=state.Runs.TryGetValue(tale,out var run)&&run.Deep!=null?DeepCurrencyBase(run):0;
                response.MaterialLists.Add(new Material{Id=currency,Num=coins});
            }
        }
    }
    internal static SelectGodWarBuffRsp SelectDeepBuff(Player p,SelectGodWarBuffReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new SelectGodWarBuffRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=SelectGodWarBuffRsp.Retcode.NotOpen};
            if(req.GodWarId!=1){rsp.retcode=SelectGodWarBuffRsp.Retcode.FeatureClose;return rsp;}var s=Load(p.User);
            if(!s.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="buff")return rsp;
            if(!RepairDeepOffers(p,s,run,out _)){rsp.retcode=SelectGodWarBuffRsp.Retcode.Fail;return rsp;}
            if(!d.Candidates.Contains(req.BuffId)||!EligibleBuffs(run,0).Contains(req.BuffId)){rsp.retcode=SelectGodWarBuffRsp.Retcode.NotCandidate;return rsp;}
            uint selectedLevel=DeepSelectedBuffLevel(run,req.BuffId);
            if(!Commit(p,s,()=>{bool core=d.CoreSuit!=0;ClearSignetDialogue(d);d.Buffs[req.BuffId]=selectedLevel;RepriceDeepShop(d);if(!core&&d.RewardChoicesRemaining>0)d.RewardChoicesRemaining--;RememberCollections(p,s,false);if(!OfferCore(run)){d.Candidates=Array.Empty<uint>();if(d.CoreFromShop){d.CoreFromShop=false;d.Phase="portal";}else if(d.RewardChoicesRemaining>0)Candidates(run);else{Portals(run);if(d.RoomRulesVersion<4&&(d.Floor+1)%4==0)ShopGoods(run);}}})){rsp.retcode=SelectGodWarBuffRsp.Retcode.Fail;return rsp;}
            rsp.retcode=0;rsp.Buff=new GodWarBuff{BuffId=req.BuffId,BuffLevel=selectedLevel};return rsp;
        }
    }
    internal static GetGodWarTeleportEventRsp DeepTeleports(Player p,GetGodWarTeleportEventReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new GetGodWarTeleportEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=GetGodWarTeleportEventRsp.Retcode.NotOpen};if(req.GodWarId!=1){rsp.retcode=GetGodWarTeleportEventRsp.Retcode.FeatureClose;return rsp;}
            if(Load(p.User).Runs.TryGetValue(req.TaleId,out var run)&&run.Deep is {} d&&d.Phase=="portal"){rsp.retcode=0;rsp.TeleportIdLists=DeepCurrentPortals(run);}return rsp;
        }
    }
    internal static RefreshGodWarBuffEventRsp RefreshDeepBuff(Player p,RefreshGodWarBuffEventReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new RefreshGodWarBuffEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=RefreshGodWarBuffEventRsp.Retcode.Fail};if(req.GodWarId!=1){rsp.retcode=RefreshGodWarBuffEventRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);if(!s.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="buff"||d.BuffRefreshed||d.CoreSuit!=0)return rsp;
            if(Commit(p,s,()=>{d.BuffRefreshed=true;Candidates(run,d.Candidates);ClearSignetDialogue(d);} ))rsp.retcode=0;return rsp;
        }
    }
    internal static RefreshGodWarTeleportEventRsp RefreshDeepTeleport(Player p,RefreshGodWarTeleportEventReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new RefreshGodWarTeleportEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,Random=req.Random,retcode=RefreshGodWarTeleportEventRsp.Retcode.Fail};if(req.GodWarId!=1){rsp.retcode=RefreshGodWarTeleportEventRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);if(!s.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="portal")return rsp;
            if(IsDeepBossChoice(run)||LeadsToRegularBoss(d)||LeadsToFinalWeeklyShop(d)||run.Challenge.RefreshTeleportTimes>=d.PortalRefreshLimit){rsp.retcode=RefreshGodWarTeleportEventRsp.Retcode.RandomInvalid;return rsp;}
            if(!DeepPortalPool(run).Any() && NextWeeklyShop(d)==null)return rsp;
            if(Commit(p,s,()=>{run.Challenge.RefreshTeleportTimes++;run.Challenge.RefreshTeleportTimesLimit=d.PortalRefreshLimit;Portals(run);}))rsp.retcode=0;return rsp;
        }
    }
    private static bool IsDeepShopInteraction(uint id)=>id==800001||Table("GodWarEventProp")
        .Any(x=>((uint)x["PropID"] is 1 or 3)&&(uint)x["PropEvent"]==id);
    internal static GetGodWarTaleShopRsp DeepShop(Player p,GetGodWarTaleShopReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new GetGodWarTaleShopRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=GetGodWarTaleShopRsp.Retcode.NoShop};if(req.GodWarId!=1){rsp.retcode=GetGodWarTaleShopRsp.Retcode.FeatureClose;return rsp;}
            var state=Load(p.User);if(!state.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="portal"||!d.MerchantOpen)return rsp;
            if(!RepairDeepOffers(p,state,run,out _)){rsp.retcode=GetGodWarTaleShopRsp.Retcode.Fail;return rsp;}
            var shop=new GodWarShop{SpecialDiscount=DeepSpecialDiscount(d),RefreshShopCost=DeepRefreshCost(d),RefreshShopRandom=d.Revision,ShoppingCost=0,IsCanChangeState=DeepCanChangeLineup(run),ChangeStateEventId=800002,IsCanRecover=!d.ShopRecoverUsed&&run.Challenge.AvatarLists.Any(a=>a.HpPercent>0),RecoverEventId=800003};
            foreach(var g in d.Goods){var row=new GodWarGoods{goods_type=g.Upgrade?GodWarGoods.GoodsType.GoodsTypeLevelUpBuff:GodWarGoods.GoodsType.GoodsTypeBuff,GoodsIdx=g.Index,GoodsNum=g.Sold?0u:1u,CoinCost=g.Cost};if(g.Upgrade)row.LevelupBuffGoods=new GodWarGoods.LevelUpBuffGoods{BuffId=g.Buff,BuffCurLevel=d.Buffs[g.Buff]};else row.buff_goods=new GodWarGoods.BuffGoods{BuffId=g.Buff,BuffInitLevel=DeepSelectedBuffLevel(run,g.Buff)};shop.GoodsLists.Add(row);}
            rsp.retcode=0;rsp.Shop=shop;return rsp;
        }
    }
    internal static BuyGodWarShopGoodsRsp BuyDeepGoods(Player p,BuyGodWarShopGoodsReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new BuyGodWarShopGoodsRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,GoodsIdx=req.GoodsIdx,retcode=BuyGodWarShopGoodsRsp.Retcode.NotOpen};if(req.GodWarId!=1){rsp.retcode=BuyGodWarShopGoodsRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);if(!s.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="portal")return rsp;
            var g=d.Goods.SingleOrDefault(x=>x.Index==req.GoodsIdx);if(g==null||g.Sold||!EligibleShopSignets(run).Contains(g.Buff)||g.Upgrade!=d.Buffs.ContainsKey(g.Buff)){rsp.retcode=BuyGodWarShopGoodsRsp.Retcode.GoodsError;return rsp;}
            if(d.Coins<g.Cost){rsp.retcode=BuyGodWarShopGoodsRsp.Retcode.MaterialLack;return rsp;}uint level=g.Upgrade?d.Buffs[g.Buff]+1:DeepSelectedBuffLevel(run,g.Buff);
            if(!Table("GodWarBuff").Any(x=>(uint)x["BuffID"]==g.Buff&&(uint)x["BuffLevel"]==level)){rsp.retcode=BuyGodWarShopGoodsRsp.Retcode.GoodsError;return rsp;}
            rsp.retcode=Commit(p,s,()=>{d.Coins-=g.Cost;d.RoomCoinBase=d.Coins;g.Sold=true;d.Buffs[g.Buff]=level;RenewDeepUpgrade(d,g);RepriceDeepShop(d);RememberCollections(p,s,false);if(OfferCore(run))d.CoreFromShop=true;})?0:BuyGodWarShopGoodsRsp.Retcode.Fail;return rsp;
        }
    }
    internal static RefreshGodWarShopGoodsRsp RefreshDeepShop(Player p,RefreshGodWarShopGoodsReq req)
    {
        lock(p.Equipment)lock(Gate)
        {
            var rsp=new RefreshGodWarShopGoodsRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,Random=req.Random,retcode=RefreshGodWarShopGoodsRsp.Retcode.NotOpen};if(req.GodWarId!=1){rsp.retcode=RefreshGodWarShopGoodsRsp.Retcode.FeatureClose;return rsp;}
            var s=Load(p.User);if(!s.Runs.TryGetValue(req.TaleId,out var run)||run.Deep is not {} d||d.Phase!="portal"||!d.MerchantOpen)return rsp;
            if((d.ShopRulesVersion==0&&d.ShopRefreshed)||d.ShopRequests.Contains(req.Random)||req.Random!=d.Revision){rsp.retcode=RefreshGodWarShopGoodsRsp.Retcode.RandomInvalid;return rsp;}uint refreshCost=DeepRefreshCost(d);if(d.Coins<refreshCost){rsp.retcode=RefreshGodWarShopGoodsRsp.Retcode.MaterialLack;return rsp;}
            rsp.retcode=Commit(p,s,()=>{d.ShopRefreshed=true;d.ShopRequests.Add(req.Random);d.Coins-=refreshCost;d.RoomCoinBase=d.Coins;d.ShopRefreshCount++;BuildDeepShopGoods(run,true);})?0:RefreshGodWarShopGoodsRsp.Retcode.Fail;return rsp;
        }
    }
    internal static void PushDeepEvents(Session session,uint tale,IEnumerable<GodWarEventInfo> events,uint trigger=0,uint[] parameters=null)
    {
        var list=events.ToArray();if(list.Length==0)return;var notify=new GodWarEventNotify{GodWarId=1,TaleId=tale,TriggerType=trigger,TriggerParamLists=parameters??Array.Empty<uint>()};notify.EventLists.AddRange(list);session.Send(Packet.FromProto(notify,CmdId.GodWarEventNotify));
    }
    internal static void PushDeepAfter(Session session,uint tale,bool shop=false)
    {
        lock(Gate){var s=Load(session.Player.User);if(s.Runs.TryGetValue(tale,out var run)&&run.Deep!=null){var events=AfterEvents(run);if((shop||IsDeepRest(run))&&run.Deep.MerchantOpen)events.Add(Event(80000101,1));PushDeepEvents(session,tale,events);}}
    }
    internal static uint ActiveDeepTale(Player p,uint stage)
    {
        lock(Gate){var s=Load(p.User);return s.Runs.FirstOrDefault(x=>x.Value.Deep!=null&&x.Value.Deep.Stages[x.Value.Deep.Floor]==stage).Key;}
    }
    internal static void PushDeepResume(Session session,uint tale)
    {
        lock(Gate){var s=Load(session.Player.User);if(s.Runs.TryGetValue(tale,out var run)&&run.Deep?.Phase=="site")PushDeepEvents(session,tale,new[]{Enter(run,tale)});}
    }
    internal static PredictGodWarEventRsp PredictDeep(Player p,PredictGodWarEventReq req)
    {
        lock(Gate)
        {
            var rsp=new PredictGodWarEventRsp{GodWarId=req.GodWarId,TaleId=req.TaleId,retcode=PredictGodWarEventRsp.Retcode.Fail,EventIdLists=Array.Empty<uint>()};
            if(req.GodWarId!=1){rsp.retcode=PredictGodWarEventRsp.Retcode.FeatureClose;return rsp;}
            var ids=req.EventIdLists??Array.Empty<uint>();if(ids.Length>256)return rsp;var s=Load(p.User);
            if(!s.Runs.TryGetValue(req.TaleId,out var run))return rsp;
            rsp.retcode=0;rsp.EventIdLists=ids;return rsp;
        }
    }
}








