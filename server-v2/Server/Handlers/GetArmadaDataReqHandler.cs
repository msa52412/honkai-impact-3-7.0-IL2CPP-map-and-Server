using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

/// <summary>
/// 舰团主数据。客户端点主页「舰队」按钮就会发这个请求。
///
/// 以前服务端没实现 → 回 `not handled!` → 舰队页面一直转圈，「建团」点了没反应。
/// 现在：没加入舰团就回 ArmadaPlayerNotJoin（页面正常显示「创建 / 搜索」入口），
/// 已加入就把内存里的舰团数据带上。
/// </summary>
[PacketCmdId(CmdId.GetArmadaDataReq)]
internal class GetArmadaDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		Armada? armada = ArmadaModule.GetInstance().ByUid(uid);

		GetArmadaDataRsp proto = new GetArmadaDataRsp
		{
			retcode = GetArmadaDataRsp.Retcode.Succ,
			Status = armada == null ? ArmadaPlayerStatus.ArmadaPlayerNotJoin : ArmadaPlayerStatus.ArmadaPlayerJoined,
			IsNew = false,
			IsNeedRecommend = false
		};
		if (armada != null)
		{
			proto.Armada = ArmadaModule.GetInstance().ToData(armada, uid);
		}

		session.Send(Packet.FromProto(proto, CmdId.GetArmadaDataRsp));
	}
}
