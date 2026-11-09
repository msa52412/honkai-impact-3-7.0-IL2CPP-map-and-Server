using System.Linq;
using System.Collections.Generic;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 4700 / 4701 —— 主页「补给」抽卡。
///
/// 老实现只回一件写死的物品（10106 / 80026），这两个 id 在 7.0 的
/// MaterialData / WeaponData / StigmataData 里都不存在，客户端画不出图标（问号），
/// 而且什么都没写进存档。
///
/// 现在卡池内容、UP 角色、掉落权重、概率全在 run\gacha.json 里，
/// 真正的掉落在 <see cref="GachaService.Draw"/> 里按 <c>req.type</c> 选池执行。
/// </summary>
[PacketCmdId(CmdId.GachaReq)]
internal class GachaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GachaReq decodedBody = packet.GetDecodedBody<GachaReq>();

		uint gachaType = (uint)decodedBody.Type;
		GachaPool? pool = GachaConfig.Pool(gachaType);
		var avatarsBefore=string.Join(";",session.Player.AvatarList.Select(a=>$"{a.AvatarId}:{a.Star}:{a.Fragment}"));
        uint coinsBefore=(uint)session.Player.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num);
        List<GachaItem> items;
        try { items=GachaService.Purchase(session,decodedBody); }
        catch(System.Exception error)
        {
            Packet.c.Warn("[Gacha] Draw failed without committed rewards: "+error.Message);
            var refreshed=error is GachaService.PurchaseError pe&&pe.Code==GachaRsp.Retcode.GachaClosed?GetGachaDisplayReqHandler.Build(session.Player.Equipment):null;if(refreshed!=null)GachaRotation.Remember(session,refreshed);
            session.Send(Packet.FromProto(new GachaRsp{retcode=error is GachaService.PurchaseError purchaseError?purchaseError.Code:GachaRsp.Retcode.Fail,GachaRandom=decodedBody.GachaRandom,Display=refreshed},CmdId.GachaRsp));
            return;
        }
        session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(),CmdId.GetEquipmentDataReq));
        uint coinsAfter=(uint)session.Player.Equipment.MaterialList.Where(x=>x.Id==100).Sum(x=>(long)x.Num);
        if(decodedBody.IsUseHcoin || coinsAfter!=coinsBefore)session.Send(Packet.FromProto(new GetMainDataRsp{retcode=GetMainDataRsp.Retcode.Succ,Hcoin=(uint)session.Player.User.Hcoin,Scoin=coinsAfter},CmdId.GetMainDataRsp));
        var avatarsAfter=string.Join(";",session.Player.AvatarList.Select(a=>$"{a.AvatarId}:{a.Star}:{a.Fragment}"));
        if(avatarsBefore!=avatarsAfter)
            session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq(),CmdId.GetAvatarDataReq));

		GachaRsp gachaRsp = new GachaRsp
		{
			retcode = GachaRsp.Retcode.Succ,
			GachaRandom = decodedBody.GachaRandom,
			IsUseFreeGacha = decodedBody.IsUseFreeGacha,
			// 抽完要让客户端刷新补给页（剩余次数 / 保底进度 / 水晶和补给卡数量 / 碎片进度）。
			Display = GetGachaDisplayReqHandler.Build(session.Player.Equipment),
			// 碎片掉落。不填这个字段的话，结算页不会出现「获得碎片」，
			// 补给页的 0/210 也永远不动。
			ExFragment = GachaService.ExFragment(session.Player.Equipment, (uint)items.Count, pool)
		};
		gachaRsp.ItemLists.AddRange(items);

        GachaRotation.Remember(session,gachaRsp.Display);
		session.Send(Packet.FromProto(gachaRsp, CmdId.GachaRsp));
	}
}
