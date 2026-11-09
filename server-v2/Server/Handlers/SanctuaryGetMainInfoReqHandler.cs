using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 拉家园小队主信息（SanctuaryGetMainInfoReq 4305）。
///
/// 这个包以前压根没人接，服务端日志里是
///     `SanctuaryGetMainInfoReq not handled!`
/// 于是客户端拿不到 SanctuaryActivityData，家园主界面底部一直显示
/// 「家园小队 等级.1 / 经验值 0/2147483647」——等级卡 1 的真正原因。
///
/// 回包有两个硬性要求（客户端 OnSanctuaryGetMainInfoRsp 0x182454610 反汇编）：
///   * retcode 必须为 SUCC(0)，非 0 直接 return，什么都不更新；
///   * activity_data 不能为 null，为 null 会走 0x1804fe400 抛异常（客户端崩）。
/// </summary>
[PacketCmdId(CmdId.SanctuaryGetMainInfoReq)]
internal class SanctuaryGetMainInfoReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SanctuaryScheme sanctuary = session.Player.Sanctuary;

		if (SanctuaryService.Prepare(sanctuary, session.Player))
		{
			sanctuary.Save();
		}

		SanctuaryGetMainInfoRsp rsp = new SanctuaryGetMainInfoRsp();
		SanctuaryService.FillMainInfo(rsp, sanctuary);

		session.Send(Packet.FromProto(rsp, CmdId.SanctuaryGetMainInfoRsp));
	}
}
