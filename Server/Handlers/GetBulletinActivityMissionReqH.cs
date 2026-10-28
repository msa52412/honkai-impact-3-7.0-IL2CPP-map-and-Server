using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetBulletinActivityMissionReq)]
internal class GetBulletinActivityMissionReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetBulletinActivityMissionReq decodedBody = packet.GetDecodedBody<GetBulletinActivityMissionReq>();
		GetBulletinActivityMissionRsp getBulletinActivityMissionRsp = new GetBulletinActivityMissionRsp
		{
			retcode = GetBulletinActivityMissionRsp.Retcode.Succ
		};
		getBulletinActivityMissionRsp.MissionGroupLists.AddRange(decodedBody.ActivityIdLists.Select((uint activityId) => new BulletinMissionGroup
		{
			ActivityId = activityId
		}).ToList());
		session.Send(Packet.FromProto(getBulletinActivityMissionRsp, CmdId.GetBulletinActivityMissionRsp));
	}
}
