using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetLoginActivityReq)]
internal class GetLoginActivityReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetLoginActivityRsp getLoginActivityRsp = new GetLoginActivityRsp
		{
			retcode = GetLoginActivityRsp.Retcode.Succ
		};
		getLoginActivityRsp.LoginLists.Add(new LoginActivityData
		{
			Id = 581u,
			LoginDays = Login.GetUserLoginDays(session.Player.User.Uid),
			AcceptTime = session.Player.User.GetCreationTime(),
			DurationEndTime = session.Player.User.GetCreationTime() + 1209600
		});
		session.Send(Packet.FromProto(getLoginActivityRsp, CmdId.GetLoginActivityRsp));
	}
}
