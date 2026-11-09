using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3450/3451 —— 关卡内换人（旧深渊）。
///
/// 服务端没有「本局锁定队伍」的概念，回 Succ 即可；
/// 队伍数据由客户端自己维护并在 3424 的 lineup_info 里带上。
/// </summary>
[PacketCmdId(CmdId.OpenworldEndlessChangeAvatarReq)]
internal class OpenworldEndlessChangeAvatarReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		OpenworldEndlessChangeAvatarReq req = packet.GetDecodedBody<OpenworldEndlessChangeAvatarReq>();
		Packet.c.Log($"[Abyss] ChangeAvatar avatars=[{string.Join(",", req.AvatarIdLists ?? System.Array.Empty<uint>())}]");

		session.Send(Packet.FromProto(new OpenworldEndlessChangeAvatarRsp
		{
			retcode = OpenworldEndlessChangeAvatarRsp.Retcode.Succ
		}, CmdId.OpenworldEndlessChangeAvatarRsp));
	}
}
