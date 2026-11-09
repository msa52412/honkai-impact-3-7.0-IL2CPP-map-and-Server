using System;
namespace PemukulPaku.GameServer.Game;
internal static class AbyssCalendar
{
 internal sealed record Window(long Begin,long End,long Close,long NextBegin,long Ordinal)
 {
  internal bool Open(long now)=>now>=Begin&&now<End;
 }
 internal static Window At(DateTimeOffset now)
 {
  var local=now.ToOffset(TimeSpan.FromHours(8));
  var begin=new DateTimeOffset(local.Date.AddHours(15),TimeSpan.FromHours(8));
  while(begin>local||(begin.DayOfWeek!=DayOfWeek.Monday&&begin.DayOfWeek!=DayOfWeek.Friday))begin=begin.AddDays(-1);
  var end=begin.AddDays(2).AddHours(7);
  var next=begin.AddDays(begin.DayOfWeek==DayOfWeek.Monday?4:3);
  long week=(long)Math.Floor((begin.Date-new DateTime(1970,1,5)).TotalDays/7);
  return new(begin.ToUnixTimeSeconds(),end.ToUnixTimeSeconds(),end.AddMinutes(30).ToUnixTimeSeconds(),next.ToUnixTimeSeconds(),week*2+(begin.DayOfWeek==DayOfWeek.Friday?1:0));
 }
}
