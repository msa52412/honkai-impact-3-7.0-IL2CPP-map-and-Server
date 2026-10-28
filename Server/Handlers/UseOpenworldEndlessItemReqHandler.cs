using System.Collections.Generic;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3422/3423 —— 使用旧深渊（迪拉克之海 / 量子奇点）的游戏内道具。
///
/// 客户端口径（反汇编证据）
/// ------------------------
///   NetworkManager.RequestUseOpenworldEndlessItemReq(0x181AD4F60)
///       只写 item_id / target_uid / item_num，**battle_id 根本没发**（invasion 固定 null）。
///       所以陷阱落在哪个 battle 由服务端决定，回包里的 battle_id 只用来提示玩家。
///   OWEndlessModule.OnUseOpenworldEndlessItemRsp(0x18189B7F0)
///       retcode != 0 -> ExcuteUseItemCallback(false, ...) 弹错误
///       retcode == 0 -> 只调 RefreshEffectItemList(rsp.effect_item_list)，**不刷新背包**
///   OWEndlessModule.RefreshEffectItemList(0x18189BB30)
///       清空 _effectItemList(+0x88)，按每项 num 次重复加入 item_id
///
/// 因此用完之后必须再推一包 3421（OpenworldEndlessItemUpdateNotify），
/// 否则客户端道具栏上的数量不会变。
/// </summary>
[PacketCmdId(CmdId.UseOpenworldEndlessItemReq)]
internal class UseOpenworldEndlessItemReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UseOpenworldEndlessItemReq req = packet.GetDecodedBody<UseOpenworldEndlessItemReq>();
		uint uid = session.Player.User.Uid;
		// 请求里没有模式字段（反汇编确认只发 item_id/target_uid/item_num），
		// 用「当前选中的深渊类型」兜底：2 = 迪拉克之海，3 = 量子奇点。
		uint type = AbyssConfig.SelectedEndlessType == 3u ? 3u : 2u;

		// 干涉道具（150010）会带 invasion{invasion_id, avatar_list, buff_list}：
		// 这是「召唤干涉怪物」界面里选的 boss/buff 组合，服务端必须原样记账，
		// 否则 3420 的 invasion_list 里没有这条入侵，地图上看不到干涉 boss。
		string invDump = req.Invasion == null
			? "null"
			: $"id={req.Invasion.InvasionId} avatars=[{string.Join(",", req.Invasion.AvatarLists ?? System.Array.Empty<uint>())}] "
				+ $"buffs=[{string.Join(",", req.Invasion.BuffLists ?? System.Array.Empty<uint>())}]";
		Packet.c.Log($"[Abyss] UseItem uid={uid} item={req.ItemId} num={req.ItemNum} target={req.TargetUid} invasion={invDump}");

		OwEndlessItems.UseResult result = OwEndlessItems.Use(uid, type, req.ItemId, req.ItemNum, req.TargetUid);

		UseOpenworldEndlessItemRsp rsp = new UseOpenworldEndlessItemRsp
		{
			retcode = result.Code,
			ItemId = result.ItemId,
			BattleId = result.BattleId,
			TargetId = result.TargetId,
			NewInvasionId = result.NewInvasionId
		};

		OwEndlessItemState state = OwEndlessItems.Get(uid, type);
		List<string> effectDump = new List<string>();
		lock (state.Gate)
		{
			state.PurgeEffects();
			foreach (var kv in state.Effects)
			{
				if (kv.Value != 0u)
				{
					rsp.EffectItemLists.Add(new OpenworldEndlessItem { ItemId = kv.Key, Num = kv.Value });
					effectDump.Add(kv.Key + "x" + kv.Value);
				}
			}
		}

		session.Send(Packet.FromProto(rsp, CmdId.UseOpenworldEndlessItemRsp));

		// 背包唯一刷新通道：3421。无论成功失败都推一份，保证界面和库存一致。
		session.Send(Packet.FromProto(OwEndlessItems.BuildNotify(state), CmdId.OpenworldEndlessItemUpdateNotify));

		OwEndlessItemState.Persist();

		// effect 列表原样打出来：客户端「没有 buff 图标 / 技能不生效」时，
		// 先确认这一行里有没有对应的 type-6 id（陷阱 150026 -> buff 150036）。
		Packet.c.Log($"[Abyss] UseItemRsp item={req.ItemId} retcode={result.Code} battle={result.BattleId} "
			+ $"effect=[{string.Join(",", effectDump)}]");
	}
}
