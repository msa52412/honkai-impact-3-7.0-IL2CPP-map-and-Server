using System;
using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.SetArmadaBulletinReq)] internal class SetArmadaBulletinReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var q=p.GetDecodedBody<SetArmadaBulletinReq>();var rsp=new SetArmadaBulletinRsp{retcode=SetArmadaBulletinRsp.Retcode.Fail};
  try{var module=ArmadaModule.GetInstance();string old="";string bulletin=q.Bulletin??"",contact=q.Contact??"";
   if(bulletin.Any(char.IsControl)||contact.Any(char.IsControl)){rsp.retcode=SetArmadaBulletinRsp.Retcode.FormatError;}
   else{int result=module.Update(s.Player.User.Uid,a=>{old=a.Bulletin;a.Bulletin=bulletin;a.Contact=contact;});rsp.retcode=result==1?SetArmadaBulletinRsp.Retcode.NotInArmada:result==2?SetArmadaBulletinRsp.Retcode.NotGranted:0;if(result==0){rsp.Bulletin=bulletin;rsp.AuditBulletin=bulletin;rsp.OldBulletin=old;}}
  }catch(Exception e){Packet.c.Warn("[Armada] bulletin persistence failed: "+e.Message);}s.Send(Packet.FromProto(rsp,CmdId.SetArmadaBulletinRsp));}
}
[PacketCmdId(CmdId.SetArmadaApplyReq)] internal class SetArmadaApplyReqHandler:IPacketHandler
{
 public void Handle(Session s,Packet p){var q=p.GetDecodedBody<SetArmadaApplyReq>();var rsp=new SetArmadaApplyRsp{retcode=SetArmadaApplyRsp.Retcode.Fail};
  try{if(q.ApplyLevel>100||q.AutoPassLevel>100)rsp.retcode=SetArmadaApplyRsp.Retcode.LevelError;else{int r=ArmadaModule.GetInstance().Update(s.Player.User.Uid,a=>{a.ApplyLevel=q.ApplyLevel;a.AutoPass=q.AutoPass;a.AutoPassLevel=q.AutoPassLevel;});rsp.retcode=r==1?SetArmadaApplyRsp.Retcode.NotInArmada:r==2?SetArmadaApplyRsp.Retcode.NotGranted:0;}}catch(Exception e){Packet.c.Warn("[Armada] apply settings persistence failed: "+e.Message);}s.Send(Packet.FromProto(rsp,CmdId.SetArmadaApplyRsp));}
}
