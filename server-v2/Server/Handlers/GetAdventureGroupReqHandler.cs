using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;
using ProtoBuf;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3900/3901 —— 家园小队主页数据（舰桥 → 右下角家园 → 左下角家园小队）。
///
/// 之前服务端**没有这个处理器**，客户端发 3900 无人应答，就保留自己的默认等级，
/// 表现是「家园小队一直是 1 级」，而改 Sanctuary 存档/配置完全没用
/// （Sanctuary 是旧活动界面，和这个页面不是一条链路）。
///
/// 7.0 字段口径（反汇编实证，别按旧模型猜）
/// --------------------------------------
///   生成模型里 tag 2 叫 level，但 7.0 客户端把 tag 2 当 **old_level**；
///   真正的当前等级在 **tag 18**，徽章 tag 19，额外徽章消耗 tag 20。
/// 生成模型只到 tag 17，所以 18/19/20 用 IExtensible 补写，
/// 不动 Common\Common.Resources.Proto\GetAdventureGroupRsp.cs（重新生成会丢）。
/// </summary>
[PacketCmdId(CmdId.GetAdventureGroupReq)]
internal class GetAdventureGroupReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint uid = session.Player.User.Uid;
		uint level = AdventureGroupService.LevelFor(uid);

		GetAdventureGroupRsp rsp = new GetAdventureGroupRsp
		{
			retcode = GetAdventureGroupRsp.Retcode.Succ,

			// tag 2：旧模型叫 level，7.0 客户端读作 old_level。
			// 保留同一个值，升级弹窗/经验那套才不会错位。
			Level = level,

			// tag 3：旧模型叫 badge，7.0 客户端读作 old_badge。
			Badge = 0u,

			LastExamination = 0u,
			Grain = 0u,
			HasTakeRewardLevel = 0u,
			NextScoinAddTime = 0u,
			StorageScoin = 0u,
			StorageStamina = 0u,
			TodayResetQuestNum = 0u
		};

		// tag 18/19/20：7.0 才有的字段，生成模型里没有，走扩展写入。
		Extensible.AppendValue(rsp, 18, level);   // level            当前等级
		Extensible.AppendValue(rsp, 19, 0u);      // badge            徽章
		Extensible.AppendValue(rsp, 20, 0u);      // extra_badge_cost_num

		Packet.c.Log($"[Adventure] GetAdventureGroupReq uid={uid} -> level={level}"
			+ " (tag2=old_level, tag18=level)");

		session.Send(Packet.FromProto(rsp, CmdId.GetAdventureGroupRsp));
	}
}
