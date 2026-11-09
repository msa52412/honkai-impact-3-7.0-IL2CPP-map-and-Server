using System;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Newtonsoft.Json.Linq;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetStageDropDisplayReq)]
internal class GetStageDropDisplayReqHandler : IPacketHandler
{
    private static readonly Lazy<ILookup<int, uint>> DropItems = new(() =>
        JArray.Parse(File.ReadAllText("Resources/ExcelOutputAsset/StageDropItemData.json"))
            .ToLookup(row => (int)row["DropID"], row => (uint)row["ItemID"]));

    internal static GetStageDropDisplayRsp Build(uint[] requested)
    {
        var rsp = new GetStageDropDisplayRsp();
        if (requested?.Length > 256)
        {
            rsp.retcode = GetStageDropDisplayRsp.Retcode.StageTooMuch;
            return rsp;
        }

        foreach (uint id in (requested ?? Array.Empty<uint>()).Distinct())
        {
            var stage = WeekdayGameplay.Stage(id);
            if (stage == null)
                continue;

            rsp.StageDropLists.Add(new StageDropDisplayInfo
            {
                StageId = id,
                DropItemIdLists = (stage.DropList ?? Array.Empty<int>())
                    .SelectMany(drop => DropItems.Value[drop]).Where(item => item != 0).Distinct().ToArray()
            });
        }
        if (requested?.Length > 0 && rsp.StageDropLists.Count == 0)
            rsp.retcode = GetStageDropDisplayRsp.Retcode.StageNotExist;
        return rsp;
    }

    public void Handle(Session session, Packet packet)
    {
        var req = packet.GetDecodedBody<GetStageDropDisplayReq>();
        session.Send(Packet.FromProto(Build(req.StageIdLists), CmdId.GetStageDropDisplayRsp));
    }
}
