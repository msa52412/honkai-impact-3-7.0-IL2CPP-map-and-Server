using System;
using System.IO;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static class GodWarService
{
	private static JArray Read(string name) => JArray.Parse(File.ReadAllText($"Resources/ExcelOutputAsset/{name}.json"));
	private static readonly Lazy<JArray> Tales = new(() => Read("GodWarTale"));
	private static readonly Lazy<JArray> Schedules = new(() => Read("GodWarTaleSchedule"));
	private static readonly Lazy<JArray> AvatarSchedules = new(() => Read("GodWarAvatarTaleSchedule"));

	internal static GodWarRoleInfo RoleInfo(Player player) => GodWarProgression.RoleInfo(player);

    internal static GetGodWarRsp Build(Player player)
    {
        var response=new GetGodWarRsp{retcode=0};
        var realm=new GodWar{GodWarId=1,BeginTime=0,EndTime=uint.MaxValue,RoleInfo=RoleInfo(player)};
        foreach(uint chapter in new uint[]{1,2,3})realm.ChapterLists.Add(new GodWarChapter{ChapterId=chapter});
        AddTales(realm);GodWarProgression.Apply(player,realm);response.GodWarLists.Add(realm);return response;
    }

	internal static void AddTales(GodWar realm)
	{
		long now = GodWarProgression.Clock().ToUnixTimeSeconds();
		foreach (JObject config in Tales.Value)
		{
			uint id = (uint)config["TaleID"];
			var schedule = GodWarCalendar.TaleSchedule(id,Schedules.Value,now);
			if(schedule == null)continue;
			var avatarSchedule = GodWarCalendar.AvatarSchedule(id,AvatarSchedules.Value,now);
			var window = GodWarProgression.EntryWindow(id,now);
			var tale = new GodWarTale
			{
				TaleId = id, ScheduleId = schedule == null ? 0u : (uint)schedule["TaleScheduleID"],
				BeginTime = window.Begin, EndTime = window.End, IsLocked = false, CurSiteId = 0,
				AvatarScheduleId = avatarSchedule == null ? 0u : (uint)avatarSchedule["AvatarScheduleID"],
				Challenge = new GodWarChallenge
				{
					IsChallengeFail = false, StepLevel = 0, ChallengeScore = 0,
					SupportAvatarIdLists = Array.Empty<uint>(), PunishBuffIdLists = Array.Empty<uint>(), BoonBuffIdLists = Array.Empty<uint>()
				}
			};
			realm.TaleLists.Add(tale);
		}
	}

	internal static LevelUpGodWarRoleRelationRsp.Retcode LevelUp(Session session,uint roleId)
        => GodWarProgression.LevelUp(session.Player,roleId);
}
