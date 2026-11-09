using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
namespace PemukulPaku.GameServer.MPModule;
// SQLite is the authority. No day reset or process-local membership cache.
public class Armada
{
	public uint Id;

	public string Name;

	public uint LeaderUid;

	public string LeaderName;

	public uint Level = 1u;

	public string Bulletin = "";

	public ArmadaLabel Label;
 public string Contact="";
 public uint Fund;
 public uint ApplyLevel=1;
 public bool AutoPass;
 public uint AutoPassLevel=1;

	public List<uint> MemberUids {get;set;} = new List<uint>();

	public ArmadaBriefData ToBrief()
	{
		return new ArmadaBriefData
		{
			Id = Id,
			Name = Name,
			Level = Level,
			Bulletin = Bulletin,
 Fund=Fund,Contact=Contact,ApplyLevel=ApplyLevel,AutoPass=AutoPass,AutoPassLevel=AutoPassLevel,
			MemberNum = (uint)MemberUids.Count,
			MemberCap = 30u,
			LeaderId = LeaderUid,
			LeaderName = LeaderName,
			Label = Label
		};
	}

	public ArmadaMember ToMember(uint uid, ArmadaPosition position)
	{
		return PemukulPaku.GameServer.Game.ArmadaContributionGameplay.Member(this,uid,position);
	}
}


public class ArmadaModule
{
 public sealed class Record:IRow {public long Id{get;set;}public long CreatedAt{get;set;}public Armada Value{get;set;}=new();}
 private static readonly Lazy<ArmadaModule> Instance=new(()=>new ArmadaModule());
 private static readonly object Gate=new();
 internal static object SyncGate=>Gate;
 private static SqlTable<Record> Store=>Global.Table<Record>("Armadas",("armada_id","INTEGER",r=>r.Value.Id,true),("leader_uid","INTEGER",r=>r.Value.LeaderUid,true),("name","TEXT",r=>r.Value.Name,true));
 public static ArmadaModule GetInstance()=>Instance.Value;
 public Dictionary<uint,Armada> Armadas {get{lock(Gate)return Store.All().ToDictionary(r=>r.Value.Id,r=>r.Value);}}
 public Armada? ByUid(uint uid){lock(Gate)return Store.All().Select(r=>r.Value).FirstOrDefault(a=>a.MemberUids.Contains(uid));}
 public bool NameExists(string name){lock(Gate)return Store.All().Any(r=>r.Value.Name==name);}
 public sealed class CreateError:Exception {public CreateArmadaRsp.Retcode Code;public CreateError(CreateArmadaRsp.Retcode code){Code=code;}}
 public Armada Create(uint uid,string leaderName,string name,ArmadaLabel label)
 {
  lock(Gate){
   var all=Store.All();
   if(all.Any(r=>r.Value.MemberUids.Contains(uid)))throw new CreateError(CreateArmadaRsp.Retcode.InArmada);
   if(string.IsNullOrWhiteSpace(name)||name.Any(char.IsControl))throw new CreateError(CreateArmadaRsp.Retcode.NameFormatError);
   if(all.Any(r=>r.Value.Name==name))throw new CreateError(CreateArmadaRsp.Retcode.NameRepeat);
   uint id=checked(Math.Max(10000u,all.Select(r=>r.Value.Id).DefaultIfEmpty(10000u).Max())+1);
   var a=new Armada{Id=id,Name=name,LeaderUid=uid,LeaderName=leaderName,Label=label};a.MemberUids.Add(uid);
   Global.Store.InTransaction(()=>Store.InsertOne(new Record{Value=a}));return a;
  }
 }
 // 0 saved,1 no membership,2 not leader. Mutation becomes visible only after DB commit.
 public int Update(uint uid,Action<Armada> edit)
 {
  lock(Gate){var r=Store.All().FirstOrDefault(x=>x.Value.MemberUids.Contains(uid));if(r==null)return 1;if(r.Value.LeaderUid!=uid)return 2;edit(r.Value);Global.Store.InTransaction(()=>Store.ReplaceOne(r));return 0;}
 }
 internal void AddFund(uint armada,long amount)
 {
  lock(Gate){var r=Store.All().First(x=>x.Value.Id==armada);r.Value.Fund=checked((uint)(r.Value.Fund+amount));Store.ReplaceOne(r);}
 }
 public ArmadaData ToData(Armada armada,uint viewerUid)
 {
  var data=new ArmadaData{Brief=armada.ToBrief(),Position=armada.LeaderUid==viewerUid?ArmadaPosition.ArmadaPositionLeader:ArmadaPosition.ArmadaPositionMember,ArmadaPlayerData=new ArmadaPlayerData{Uid=viewerUid}};
  foreach(uint uid in armada.MemberUids)data.MemberLists.Add(armada.ToMember(uid,uid==armada.LeaderUid?ArmadaPosition.ArmadaPositionLeader:ArmadaPosition.ArmadaPositionMember));PemukulPaku.GameServer.Game.ArmadaConstructionGameplay.Fill(armada,data);PemukulPaku.GameServer.Game.ArmadaContributionGameplay.Fill(armada,data,viewerUid);return data;
 }
}
