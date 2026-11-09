using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

/// <summary>
/// 推荐舰团。舰队页「加入」页签会拉这个。
///
/// 没有真实推荐算法，直接把内存里已有的舰团都返回（最多 10 个），
/// 这样自己建的团能在列表里看到，也方便测试加入流程。
/// </summary>
[PacketCmdId(CmdId.GetRecommendArmadaReq)]
internal class GetRecommendArmadaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetRecommendArmadaRsp proto = new GetRecommendArmadaRsp
		{
			retcode = GetRecommendArmadaRsp.Retcode.Succ
		};
		proto.ArmadaLists.AddRange(ArmadaModule.GetInstance().Armadas.Values
			.Take(10)
			.Select(a => a.ToBrief()));
		session.Send(Packet.FromProto(proto, CmdId.GetRecommendArmadaRsp));
	}
}
