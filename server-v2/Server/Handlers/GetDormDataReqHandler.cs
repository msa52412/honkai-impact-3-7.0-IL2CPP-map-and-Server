using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 拉家园数据（GetDormDataReq 601）。data_type 决定回哪些区块：
/// ALL(0) 全量、DECORATION(1) 宿舍/仓库/设施、EVENT(3) 剧情+随机事件、SNS(4) 展示信息。
/// </summary>
[PacketCmdId(CmdId.GetDormDataReq)]
internal class GetDormDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormDataReq req = packet.GetDecodedBody<GetDormDataReq>();
		DormsScheme dorms = session.Player.Dorms;
		DormService.Prepare(dorms);

		GetDormDataRsp rsp = new GetDormDataRsp
		{
			retcode = GetDormDataRsp.Retcode.Succ,
			DataType = req.DataType
		};

		DormService.FillDormData(rsp, dorms, req.DataType);
		rsp.LastTakeDormDailyStaminaTime = dorms.LastTakeDormDailyStaminaTime;

		session.Send(Packet.FromProto(rsp, CmdId.GetDormDataRsp));

		AdventureService.Push(session);

	}
}