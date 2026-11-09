using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 拜访别人宿舍（GetOtherDormDataReq 633）：返回对方的展示宿舍、点赞 / 访问数据，
/// 同时给对方记一条访问记录（自己访问自己不记）。
/// </summary>
[PacketCmdId(CmdId.GetOtherDormDataReq)]
internal class GetOtherDormDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOtherDormDataReq req = packet.GetDecodedBody<GetOtherDormDataReq>();
		uint targetUid = req.TargetUid != 0u ? req.TargetUid : session.Player.User.Uid;

		DormsScheme own = session.Player.Dorms;
		DormService.Prepare(own);

		DormsScheme target = targetUid == session.Player.User.Uid ? own : Dorms.TryFromUid(targetUid);
		GetOtherDormDataRsp rsp = new GetOtherDormDataRsp
		{
			retcode = GetOtherDormDataRsp.Retcode.Succ,
			TargetUid = targetUid
		};

		if (target == null)
		{
			rsp.retcode = GetOtherDormDataRsp.Retcode.TargetError;
			session.Send(Packet.FromProto(rsp, CmdId.GetOtherDormDataRsp));
			return;
		}

		DormService.Prepare(target);

		if (!target.IsAllowVisit && targetUid != session.Player.User.Uid)
		{
			rsp.retcode = GetOtherDormDataRsp.Retcode.VisitForbid;
			session.Send(Packet.FromProto(rsp, CmdId.GetOtherDormDataRsp));
			return;
		}

		uint showHouse = DormService.CurrentShowHouse(target);
		DormsScheme.DormHouse house = target.GetHouse(showHouse);
		if (house != null)
		{
			rsp.House = DormService.ToProto(house);
		}

		rsp.ShowRoom = target.ShowRoom;
		rsp.VisitAvatar = target.VisitAvatar;
		rsp.BeLikedNum = target.SnsData.BeLikedNum;
		rsp.BeVisitNum = target.SnsData.BeVisitNum;
		rsp.IsReceiveLikeLimit = DormService.IsReceiveLikeLimit(target);
		rsp.IsSendLikeLimit = DormService.IsSendLikeLimit(own);
		rsp.IsHasLiked = DormService.HasLiked(own, targetUid);
		rsp.ComfortValue = (uint)Math.Max(0, target.ComfortValue(showHouse));

		foreach (DormVisitRecord record in target.SnsData.RecordLists)
		{
			rsp.RecordLists.Add(new DormVisitRecord
			{
				VisitTime = record.VisitTime,
				VisitUid = record.VisitUid,
				VisitAvatar = record.VisitAvatar,
				Level = record.Level,
				Name = record.Name
			});
		}

		DormService.AddVisitRecord(session.Player, target, req.VisitAvatar, req.Level, req.Name);

		session.Send(Packet.FromProto(rsp, CmdId.GetOtherDormDataRsp));
	}
}
