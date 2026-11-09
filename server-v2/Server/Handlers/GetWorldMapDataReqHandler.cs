using System.IO;
using Common.Resources.Proto;
using Common.Utils;
using ProtoBuf;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetWorldMapDataReq)]
internal class GetWorldMapDataReqHandler : IPacketHandler
{
	// 2026-10-21：曾试验把 ID 385（WorldMapId 2267，已过期限时地图）的 EndTime 改长，
	// 实测导致客户端世界地图渲染失败 → 全部玩法入口消失，已回退。
	// 此 blob 是 v1 原始数据，一字未改 —— 哪怕只改一个时间字段也会让地图渲染挂掉，
	// 推测客户端对这条响应做过强校验（条目数/字段布局敏感）。
	// 复盘留档：_ext\tmp\worldmap_blob.bin（原）、worldmap_blob_new.bin（试验版）。
	public void Handle(Session session, Packet packet)
	{
		MemoryStream source = new MemoryStream(Misc.StringToByteArray("0800121c080110c0c4f4eb0418bffbaad60720002800300038c0c4f4eb044001121c080210c0c4f4eb0418bffbaad60720002800300038c0c4f4eb044002121c080310c0c4f4eb0418bffbaad6072019281e300038c0c4f4eb044003121c080410c0c4f4eb0418bffbaad60720002800300138c0c4f4eb044004121c080510c0c4f4eb0418bffbaad60720002800300138c0c4f4eb044005121c080610c0c4f4eb0418bffbaad607200f2858300138c0c4f4eb044006121c080710c0c4f4eb0418bffbaad607201e2828300138c0c4f4eb044007121c080810c0c4f4eb0418bffbaad60720002800300138c0c4f4eb044008121c080910c0c4f4eb0418bffbaad60720002800300138c0c4f4eb044009121c080a10c0c4f4eb0418bffbaad60720002800300138c0c4f4eb04400a121c080b10c0c4f4eb0418bffbaad60720002800300138c0c4f4eb04400b121c080c10c0c4f4eb0418bffbaad60720002800300138c0c4f4eb044031121e08bb1010a099aae90518bffbaad6072014286330cd0138a099aae9054079121d081210c0c4f4eb0418bffbaad60720322863300138c0c4f4eb04409e02121e08ad1110a092c3800618bffbaad607200f2858300138a092c3800640b302121e08db1110c0e494a30618bfa2f2a40620002800307a38c0e494a306408103"));
		GetWorldMapDataRsp proto = Serializer.Deserialize<GetWorldMapDataRsp>(source);
		session.Send(Packet.FromProto(proto, CmdId.GetWorldMapDataRsp));
	}
}
