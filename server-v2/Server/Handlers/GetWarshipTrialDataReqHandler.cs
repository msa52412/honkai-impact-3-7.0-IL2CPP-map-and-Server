using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetWarshipTrialDataReq)]
internal class GetWarshipTrialDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetWarshipTrialDataRsp getWarshipTrialDataRsp = new GetWarshipTrialDataRsp
		{
			retcode = GetWarshipTrialDataRsp.Retcode.Succ,
			IsAll = true
		};
		getWarshipTrialDataRsp.TrialWarshipLists.Add(new TrialWarship
		{
			SampleId = 410002u,
			EndTime = (uint)((int)Global.GetUnixInSeconds() + 400000)
		});
		session.Send(Packet.FromProto(getWarshipTrialDataRsp, CmdId.GetWarshipTrialDataRsp));
	}
}
