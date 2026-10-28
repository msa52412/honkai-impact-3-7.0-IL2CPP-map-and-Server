using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetDormSnsDataReq)]
internal class GetDormSnsDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormSnsDataReq decodedBody = packet.GetDecodedBody<GetDormSnsDataReq>();
		GetDormSnsDataRsp getDormSnsDataRsp = new GetDormSnsDataRsp
		{
			retcode = GetDormSnsDataRsp.Retcode.Succ
		};
		DormsScheme.DormSnsData snsData = session.Player.Dorms.SnsData;
		getDormSnsDataRsp.BeLikedNum = snsData.BeLikedNum;
		getDormSnsDataRsp.BeVisitNum = snsData.BeVisitNum;
		IEnumerable<DormVisitRecord> collection = session.Player.Dorms.SnsData.RecordLists.Select((DormVisitRecord record) => new DormVisitRecord
		{
			VisitTime = record.VisitTime,
			VisitUid = record.VisitUid,
			VisitAvatar = record.VisitAvatar,
			Level = record.Level,
			Name = record.Name
		});
		getDormSnsDataRsp.RecordLists.AddRange(collection);
		session.Send(Packet.FromProto(getDormSnsDataRsp, CmdId.GetDormSnsDataRsp));
	}
}
