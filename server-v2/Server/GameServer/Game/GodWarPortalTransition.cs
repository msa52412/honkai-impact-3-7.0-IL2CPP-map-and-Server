using System;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;

namespace PemukulPaku.GameServer.Game;

internal static partial class GodWarProgression
{
    internal static GodWarEventInfo[] DeepPortalTransitionEvents(Player player, uint tale, uint portal)
    {
        lock (Gate)
        {
            if (!Load(player.User).Runs.TryGetValue(tale, out var run) || run.Deep?.Phase != "site")
                return Array.Empty<GodWarEventInfo>();

            var teleport = Table("GodWarTeleport").FirstOrDefault(row => (uint)row["TeleportID"] == portal);
            var eventId = (uint?)teleport?["TeleportEvent"] ?? 0;
            var boss = DeepBosses.FirstOrDefault(choice => choice.Door == portal);
            if (boss == null && portal != RegularBossDoor && (eventId is < 300100 or > 300113))
                return Array.Empty<GodWarEventInfo>();

            var root = Table("GodWarEvent").Single(row => (uint)row["EventID"] == eventId);
            var children = root["ParamsVar"].Values<uint>().ToArray();
            var markerId = boss?.ChoiceEvent ?? children.FirstOrDefault();
            var marker = Table("GodWarEvent").FirstOrDefault(row =>
                (uint)row["EventID"] == markerId && (uint)row["EventType"] == (boss == null ? 13u : 30u));
            if (marker == null || !children.Contains(markerId) || !children.Contains(300000u))
                return Array.Empty<GodWarEventInfo>();

            var markerArgs = marker["ParamsVar"].Values<uint>().ToArray();
            var expected = boss == null ? new[] { 101u, run.Deep.NextSuit } : new[] { 611u, run.Deep.SelectedBoss };
            if (!markerArgs.SequenceEqual(expected))
                return Array.Empty<GodWarEventInfo>();


            var enter = Enter(run, tale);
            enter.SourceEventId = 300000;
            enter.SourceType = 1;
            var room = Event(300000, enter.EventId);
            room.SourceEventId = eventId;
            room.SourceType = 1;
            var suit = Event(markerId, markerArgs);
            suit.SourceEventId = eventId;
            suit.SourceType = 1;
            var door = Event(eventId, markerId, room.EventId);
            door.SourceType = 1;
            return new[] { suit, enter, room, door };
        }
    }
}

