using Common.Resources.Proto;
using System.Linq;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SelectNewStigmataRuneReq)]
internal class SelectNewStigmataRuneReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var request = packet.GetDecodedBody<SelectNewStigmataRuneReq>();
        var equipment = session.Player.Equipment;
        lock (equipment)
        {
            var before = equipment.StigmataList.FirstOrDefault(x => x.UniqueId == request.UniqueId);
            Packet.c.Warn("[Affix][Select] stg=" + request.UniqueId + " isSelect=" + request.IsSelect
                + " sel=" + request.SelectUniqueId
                + " pending=" + (before == null ? "null" : string.Join(",", before.WaitSelectRuneGroupLists.Select(g =>
                    g.UniqueId + "[" + string.Join("/", g.RuneLists.Select(r => r.RuneId + "@" + r.StrengthPercent)) + "]")))
                + " now=" + (before == null ? "null" : string.Join("/", before.RuneLists.Select(r => r.RuneId + "@" + r.StrengthPercent))));
            var proto = new SelectNewStigmataRuneRsp
            {
                SelectUniqueId = request.SelectUniqueId,
                IsSelect = request.IsSelect,
                retcode = StigmataRuneSelection.Apply(equipment.StigmataList, request, equipment.Save)
            };
            Packet.c.Warn("[Affix][Select] rc=" + proto.retcode);
            session.Send(Packet.FromProto(proto, CmdId.SelectNewStigmataRuneRsp));
            if(proto.retcode==SelectNewStigmataRuneRsp.Retcode.Succ)
            {
                // Partial update: never clear the rest of the client's inventory.
                var refresh=new GetEquipmentDataRsp{retcode=GetEquipmentDataRsp.Retcode.Succ,IsAll=false};
                refresh.StigmataLists.Add(equipment.StigmataList.Single(x=>x.UniqueId==request.UniqueId));
                session.Send(Packet.FromProto(refresh,CmdId.GetEquipmentDataRsp));
            }
        }
    }
}
