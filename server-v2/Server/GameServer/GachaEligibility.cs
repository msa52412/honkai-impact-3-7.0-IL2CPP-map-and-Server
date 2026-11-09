using System;
namespace PemukulPaku.GameServer;
internal static class GachaEligibility
{
    // These five AvatarData rows belong to APHO; they are not regular gacha valkyries.
    internal static bool IsRegularAvatar(int id) => id!=2001 && id!=9011 && id!=9021 && id!=9031 && id!=9041 && id!=9051;
    // Confirmed test-icon/zero-image weapon families, including all evolutions.
    internal static bool IsAllowedWeapon(int id)
    {
        // Temporary event EX weapons, not normal inventory rewards.
        if(id==20486||id==26038||id==26040||id==26042)return false;
        if(id==26026||id==26028||id==26030||id==26032||id==26034||id==26036)return false;
        int family=id/10,step=id%10;
        return !(step>=1 && step<=4 && (family==2080 || family==2120 || family==2300 || family==2301 || family==2302 || family==2303 || family==2305 || family==2306 || family==2400 || family==2424));
    }
    internal static bool Allowed(GachaDrop drop)
    {
        if(drop==null)return false;
        if(string.Equals(drop.Kind,"weapon",StringComparison.OrdinalIgnoreCase) && (!IsAllowedWeapon((int)drop.Id)||(drop.Id>=28101&&drop.Id<=28162)))return false;
        uint id=drop.Id;
        return id!=2001 && id!=62001 && id!=12001 && id!=9011 && id!=9021 && id!=9031 && id!=9041 && id!=9051 &&
            id!=69011 && id!=69021 && id!=69031 && id!=69041 && id!=69051 &&
            id!=19011 && id!=19021 && id!=19031 && id!=19041 && id!=19051;
    }
}
