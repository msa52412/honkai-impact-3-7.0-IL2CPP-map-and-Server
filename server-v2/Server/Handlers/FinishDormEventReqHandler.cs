using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 完成宿舍互动事件（FinishDormEventReq 617）：事件必须是当天给该角色随机出来的，
/// 完成后从当日事件列表里移除，并按 DormitoryInteractEvent.TouchExp 加好感。
/// </summary>
[PacketCmdId(CmdId.FinishDormEventReq)]
internal class FinishDormEventReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		FinishDormEventReq req = packet.GetDecodedBody<FinishDormEventReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		FinishDormEventRsp rsp = new FinishDormEventRsp
		{
			retcode = FinishDormEventRsp.Retcode.Succ
		};

		DormEvent dormEvent = dorms.EventLists.FirstOrDefault(
			(DormEvent x) => x.AvatarId == req.AvatarId && (x.EventIdLists ?? new uint[0]).Contains(req.EventId));
		DormitoryInteractEventExcel cfg = DormService.InteractConfig(req.EventId);
		if (dormEvent == null || cfg == null)
		{
			rsp.retcode = FinishDormEventRsp.Retcode.ConditionLimit;
		}
		else
		{
			uint[] rest = (dormEvent.EventIdLists ?? new uint[0]).Where((uint id) => id != req.EventId).ToArray();
			if (rest.Length == 0)
			{
				dorms.EventLists.Remove(dormEvent);
			}
			else
			{
				dormEvent.EventIdLists = rest;
			}
			dorms.Save();

			DormService.AddGoodfeel(session.Player, req.AvatarId, cfg.TouchExp);
		}

		session.Send(Packet.FromProto(rsp, CmdId.FinishDormEventRsp));
	}
}
