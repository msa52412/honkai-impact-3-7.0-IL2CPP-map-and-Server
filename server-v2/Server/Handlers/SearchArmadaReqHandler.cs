using System.Linq;
using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

/// <summary>
/// 搜索舰团。按名字做子串匹配；名字留空就返回全部（最多 20 个）。
/// </summary>
[PacketCmdId(CmdId.SearchArmadaReq)]
internal class SearchArmadaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SearchArmadaReq req = packet.GetDecodedBody<SearchArmadaReq>();
		string keyword = (req.Name ?? "").Trim();

		SearchArmadaRsp proto = new SearchArmadaRsp
		{
			retcode = SearchArmadaRsp.Retcode.Succ
		};
		proto.ArmadaLists.AddRange(ArmadaModule.GetInstance().Armadas.Values
			.Where(a => keyword.Length == 0 || a.Name.Contains(keyword))
			.Take(20)
			.Select(a => a.ToBrief()));
		session.Send(Packet.FromProto(proto, CmdId.SearchArmadaRsp));
	}
}
