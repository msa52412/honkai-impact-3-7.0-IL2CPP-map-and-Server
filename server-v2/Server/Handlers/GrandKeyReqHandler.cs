using Common;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 神之键主数据（GetGrandKeyReq / 506）。
///
/// 老实现是「一律回 Fail + 空列表」，所以客户端 FPLBEFMIALC 一把都收不到，
/// 界面永远停在全部未解锁。现在改成按 GrandKey.json + 存档回真实数据。
/// </summary>
[PacketCmdId(CmdId.GetGrandKeyReq)]
internal class GrandKeyReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetGrandKeyReq decodedBody = packet.GetDecodedBody<GetGrandKeyReq>();
		GetGrandKeyRsp rsp = GrandKeyService.BuildRsp(session.Player);
		Packet.c.Log("[GrandKey] GetGrandKeyReq -> 回 " + rsp.KeyLists.Count + " 把："
			+ GrandKeyService.Describe(rsp)
			+ "（unlock_all=" + GrandKeyTuning.UnlockAll + "）");
		session.Send(Packet.FromProto(rsp, CmdId.GetGrandKeyRsp));
	}
}
