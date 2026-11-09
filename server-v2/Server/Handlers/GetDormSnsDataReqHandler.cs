using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 家园社交数据（GetDormSnsDataReq 637）：获赞数、访问数、访问记录。
/// </summary>
[PacketCmdId(CmdId.GetDormSnsDataReq)]
internal class GetDormSnsDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		GetDormSnsDataRsp rsp = new GetDormSnsDataRsp
		{
			retcode = GetDormSnsDataRsp.Retcode.Succ,
			BeLikedNum = dorms.SnsData.BeLikedNum,
			BeVisitNum = dorms.SnsData.BeVisitNum
		};

		foreach (DormVisitRecord record in dorms.SnsData.RecordLists)
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

		session.Send(Packet.FromProto(rsp, CmdId.GetDormSnsDataRsp));
	}
}
