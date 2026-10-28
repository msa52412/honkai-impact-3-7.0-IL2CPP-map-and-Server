using System.Collections.Generic;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 开放世界章节商店数据（CmdId 4575 / 4576）。
///
/// 为什么它排在第一位
/// ------------------
/// 实测日志里这是**未实现协议里的头号大户**：一次登录就发 120 次，
/// 而且是在 OpenworldChapterGetDataReq 之后紧接着发的。
///
/// 客户端拿不到应答就会一直重试（120 次 = 十几轮重试），
/// 章节界面等不到商店数据 → 内容判定为空 → OpenWorldMainStubPageContext 2 秒内自动退出。
/// 这和文档 §9.3 记录的「页面秒退」现象完全吻合。
///
/// 现在先返回一个空货架的最小可用结构：商店界面画得出来（只是没货），
/// 至少不会再让章节页面卡在等数据这一步。货架内容等入口打通后再接表。
/// </summary>
[PacketCmdId(CmdId.OpenworldChapterGetShopDataReq)]
internal class OpenworldChapterGetShopDataReqHandler : IPacketHandler
{
	/// <summary>已经打过日志的 shop_id，避免 120 次请求刷屏。</summary>
	private static readonly HashSet<uint> Logged = new HashSet<uint>();

	public void Handle(Session session, Packet packet)
	{
		OpenworldChapterGetShopDataReq req = packet.GetDecodedBody<OpenworldChapterGetShopDataReq>();

		bool first;
		lock (Logged)
		{
			first = Logged.Add(req.ShopId);
		}
		if (first)
		{
			Packet.c.Log($"[Abyss] OpenworldChapterGetShopDataReq shopId={req.ShopId} (首次，返回空货架)");
		}

		session.Send(Packet.FromProto(new OpenworldChapterGetShopDataRsp
		{
			retcode = OpenworldChapterGetShopDataRsp.Retcode.Succ,
			Shop = new OpenworldChapterShop
			{
				ShopId = req.ShopId
			}
		}, CmdId.OpenworldChapterGetShopDataRsp));
	}
}
