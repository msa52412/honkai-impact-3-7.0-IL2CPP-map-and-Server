using System;
using System.Linq;
using Newtonsoft.Json.Linq;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
    // Original AvatarAbility descriptions explicitly grant one extra Ego
    // selection. Type 7 alone also includes unrelated weapon adjustments.
    // Restrict this to the actual weekly preparation room, never trials or
    // later Ego doors. The follow-event transaction consumes this grant once.
    private static readonly uint[] OpeningEgoAbilities={1021204,1060204,1220104,1220205,1230103,1260103,1270104,1280103,1290104,1300103};
    private static uint DeepOpeningRewardCount(ChallengeRun run)
    {
        if(!IsDeepOpening(run))return 1;
        uint main=run.Challenge.AvatarLists.Single().AvatarId;
        var avatar=Table("GodWarMainAvatar").SingleOrDefault(x=>(uint)x["MainAvatarID"]==main);
        return avatar!=null&&avatar["AvatarAbilityID"].Values<uint>().Any(OpeningEgoAbilities.Contains)?2u:1u;
    }
    private static uint PortalSigilRefreshBonus(ChallengeRun run)
    {
        var item=Table("GodWarExtraItem").SingleOrDefault(x=>(uint)x["ExtraItemID"]==run.Challenge.ExtraItemId);
        return item!=null&&(uint)item["ExtraItemType"]==1&&(uint)item["ExtraItemSkillType"]==3&&
            item["ParamList"].Count()==1&&uint.TryParse((string)item["ParamList"][0],out uint bonus)?bonus:0;
    }
    private static void CapturePortalSigilRefresh(ChallengeRun run)
    {
        var d=run.Deep;if(d==null||d.PortalRefreshSigilBonus!=0)return;
        uint bonus=PortalSigilRefreshBonus(run);if(bonus==0)return;
        // Capture once: loading, reconnecting and re-sending setup must not
        // mint extra refreshes or reset the already-spent refresh counter.
        d.PortalRefreshLimit=checked(d.PortalRefreshLimit+bonus);
        d.PortalRefreshSigilBonus=bonus;
        run.Challenge.RefreshTeleportTimesLimit=d.PortalRefreshLimit;
    }
    private static bool DeepCanChangeLineup(ChallengeRun run)=>run.Deep==null||
        (run.Deep.Phase=="portal"&&run.Deep.MerchantOpen&&run.Deep.PortalRefreshSigilBonus==0&&PortalSigilRefreshBonus(run)==0);
}
