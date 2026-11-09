using Common;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetClientDataReq)]
internal class GetClientDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetClientDataReq decodedBody = packet.GetDecodedBody<GetClientDataReq>();
		ClientDataScheme clientData = Common.Database.ClientData.GetClientData(session.Player.User.Uid, decodedBody.Type, decodedBody.Id);

		// 2026-10-22：这条之前每次都会抛
		//   Error reading bytes. Unexpected token: StartObject. Path 'Data.$binary'
		// 原因是 v1(Mongo) 迁移过来的存档把 byte[] 存成了 {"$binary":"...","$type":"00"}，
		// 而 v2 用 Newtonsoft 直接读 byte[] 只认纯 base64 字符串 → 整张表反序列化失败 →
		// 一个字节都不回给客户端。已把 7 行 ClientDatas 全部改成纯 base64。
		// 这行日志用来确认它真的通了。
		Packet.c.Log($"[ClientData] GetClientDataReq type={(int)decodedBody.Type}({decodedBody.Type}) id={decodedBody.Id} -> "
			+ (clientData == null ? "未命中" : $"{clientData.Data?.Length ?? 0} 字节"));

		GetClientDataRsp getClientDataRsp = new GetClientDataRsp
		{
			retcode = GetClientDataRsp.Retcode.Succ,
			Id = decodedBody.Id,
			Type = decodedBody.Type
		};
		if (clientData != null)
		{
			getClientDataRsp.ClientDataLists.Add(new Common.Resources.Proto.ClientData
			{
				Id = clientData.ClientDataId,
				Type = clientData.Type,
				Data = clientData.Data
			});
		}
		else
		{
			getClientDataRsp.retcode = GetClientDataRsp.Retcode.NotFound;
		}
		session.Send(Packet.FromProto(getClientDataRsp, CmdId.GetClientDataRsp));
	}
}
