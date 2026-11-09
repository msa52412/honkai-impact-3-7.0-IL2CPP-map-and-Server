using System;using System.Collections.Generic;using Common;using Newtonsoft.Json;
namespace PemukulPaku.GameServer.Game;
internal static partial class GodWarProgression
{
 // Begin/leave/portal transitions modify the challenge only. Do not rewrite
 // hundreds of unrelated weapons and stigmata before answering each door.
 private static bool CommitRunState(State state,Action action)
 {
  _=Store;var before=JsonConvert.SerializeObject(state.Runs);
  try{Global.Store.InTransaction(()=>{action();Store.ReplaceOne(state);});return true;}
  catch(Exception ex){state.Runs=JsonConvert.DeserializeObject<Dictionary<uint,ChallengeRun>>(before);Packet.c.Warn("[GodWar] run transition rolled back: "+ex.Message);return false;}
 }
}
