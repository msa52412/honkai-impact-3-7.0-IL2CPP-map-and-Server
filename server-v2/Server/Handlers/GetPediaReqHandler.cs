using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 图鉴的「百科」黑名单：black_list 里的 pedia id 会被客户端标记为未解锁。
/// 之前没有 handler，客户端拿不到回包，百科条目全部处于锁定态。
/// 这里回空列表 = 全部解锁。
/// </summary>
[PacketCmdId(CmdId.GetPediaReq)]
internal class GetPediaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		session.Send(Packet.FromProto(new GetPediaRsp
		{
			retcode = GetPediaRsp.Retcode.Succ,
			BlackLists = new uint[0]
		}, CmdId.GetPediaRsp));
	}
}
