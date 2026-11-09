using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AddTechExpReq)]
internal class AddTechExpReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var request = packet.GetDecodedBody<AddTechExpReq>();
        var result = SakuraServant.Feed(session.Player, request);
        if (result == AddTechExpRsp.Retcode.Succ)
        {
            PushTech(session, request.MapId);
            DailyGameplay.PushInventory(session);
        }
        session.Send(Packet.FromProto(new AddTechExpRsp { retcode = result }, CmdId.AddTechExpRsp));
    }

    internal static void PushTech(Session session, uint map)
    {
        var state = session.Player.OpenWorlds.First(row => row.MapId == map);
        var update = new GetNewOpenworldRsp
        {
            retcode = GetNewOpenworldRsp.Retcode.Succ,
            DataType = OpenworldDataType.OpenworldDataTech
        };
        update.Techs.Add(SakuraServant.Snapshot(session.Player.User, map, state));
        session.Send(Packet.FromProto(update, CmdId.GetNewOpenworldRsp));
    }
}

[PacketCmdId(CmdId.TakeOpenworldEventRewardReq)]
internal class TakeOpenworldEventRewardReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var request = packet.GetDecodedBody<TakeOpenworldEventRewardReq>();
        var reply = SakuraServant.Gather(session.Player, request, out bool changed);
        if (changed)
            AddTechExpReqHandler.PushTech(session, request.MapId);
        session.Send(Packet.FromProto(reply, CmdId.TakeOpenworldEventRewardRsp));
    }
}

[PacketCmdId(CmdId.UnlockOpenworldTechSkillReq)]
internal class UnlockOpenworldTechSkillReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var request = packet.GetDecodedBody<UnlockOpenworldTechSkillReq>();
        var result = SakuraServant.CheckSkill(session.Player, request);
        session.Send(Packet.FromProto(new UnlockOpenworldTechSkillRsp { retcode = result }, CmdId.UnlockOpenworldTechSkillRsp));
    }
}

[PacketCmdId(CmdId.TakeOpenworldKeyQuestRewardReq)]
internal class TakeOpenworldKeyQuestRewardReqHandler : IPacketHandler
{
    public void Handle(Session session, Packet packet)
    {
        var request = packet.GetDecodedBody<TakeOpenworldKeyQuestRewardReq>();
        var result = SakuraServant.ClaimQuest(session.Player, request.MapId);
        session.Send(Packet.FromProto(result, CmdId.TakeOpenworldKeyQuestRewardRsp));
        if (result.retcode == TakeOpenworldKeyQuestRewardRsp.Retcode.Succ)
            DailyGameplay.PushInventory(session);
    }
}
