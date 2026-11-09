using System.Text;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 神之键激活技能（GrandKeyActivateSkillReq / 759）。
/// 老实现是空函数（不回包）。这里把 key_list 里的 key_id/skill_id/last_time
/// 写进存档并回 SUCC，再推一份完整列表让界面刷新。
/// 技能数值本身由客户端按 GrandKeyBuff.json 自己算，服务端只负责存状态。
///
/// 注意：客户端发来的 last_time 是**时长**（秒），不是绝对时间戳，
/// 换算在 GrandKeysScheme.ActivateGrandKeySkill 里做。
/// </summary>
[PacketCmdId(CmdId.GrandKeyActivateSkillReq)]
internal class GrandKeyActivateSkillReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GrandKeyActivateSkillReq req = packet.GetDecodedBody<GrandKeyActivateSkillReq>();
		GrandKeys.GrandKeysScheme scheme = GrandKeyService.Scheme(session.Player);
		GrandKeyService.Sync(session.Player, scheme);

		int n = 0;
		int total = req.KeyLists?.Count ?? 0;
		StringBuilder detail = new StringBuilder();
		if (req.KeyLists != null)
		{
			foreach (GrandKeySkill skill in req.KeyLists)
			{
				if (skill == null)
				{
					detail.Append("(null) ");
					continue;
				}
				bool found = scheme.Find(skill.KeyId) != null;
				detail.Append($"[key={skill.KeyId} skill={skill.SkillId} last={skill.LastTime}{(found ? "" : " 未解锁!")}] ");
				if (!found)
				{
					continue;
				}
				scheme.ActivateGrandKeySkill(skill.KeyId, skill.SkillId, skill.LastTime);
				n++;
			}
		}
		if (n > 0)
		{
			scheme.Save();
		}
		Packet.c.Log($"[GrandKey] 激活技能 收到 {total} 条，写入 {n} 条：{detail}");
		Packet.c.Log("[GrandKey] 写入后：" + GrandKeyService.DescribeScheme(scheme));

		session.Send(Packet.FromProto(new GrandKeyActivateSkillRsp
		{
			retcode = GrandKeyActivateSkillRsp.Retcode.Succ
		}, CmdId.GrandKeyActivateSkillRsp));

		if (n > 0)
		{
			GrandKeyService.PushMainInfo(session);
		}
	}
}
