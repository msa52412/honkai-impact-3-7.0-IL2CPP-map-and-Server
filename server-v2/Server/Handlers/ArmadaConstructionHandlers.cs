using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
namespace PemukulPaku.GameServer.Handlers;
[PacketCmdId(CmdId.GetArmadaManageDataReq)] internal class GetArmadaManageDataReqHandler:IPacketHandler{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(ArmadaConstructionGameplay.Manage(s.Player),CmdId.GetArmadaManageDataRsp));}
[PacketCmdId(CmdId.GetArmadaBuildOwnershipReq)] internal class GetArmadaBuildOwnershipReqHandler:IPacketHandler{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(ArmadaConstructionGameplay.Own(s.Player),CmdId.GetArmadaBuildOwnershipRsp));}
[PacketCmdId(CmdId.ReleaseArmadaBuildOwnershipReq)] internal class ReleaseArmadaBuildOwnershipReqHandler:IPacketHandler{public void Handle(Session s,Packet p)=>s.Send(Packet.FromProto(ArmadaConstructionGameplay.Release(s.Player),CmdId.ReleaseArmadaBuildOwnershipRsp));}
[PacketCmdId(CmdId.LevelUpArmadaCabinReq)] internal class LevelUpArmadaCabinReqHandler:IPacketHandler{public void Handle(Session s,Packet p){var rsp=ArmadaConstructionGameplay.Start(s.Player,p.GetDecodedBody<LevelUpArmadaCabinReq>());s.Send(Packet.FromProto(rsp,CmdId.LevelUpArmadaCabinRsp));if(rsp.retcode==0)s.ProcessPacket(Packet.FromProto(new GetArmadaDataReq(),CmdId.GetArmadaDataReq));}}
