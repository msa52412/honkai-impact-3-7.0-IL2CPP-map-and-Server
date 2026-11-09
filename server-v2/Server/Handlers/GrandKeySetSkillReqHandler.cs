using System.Text;
using Common;
using Common.Database;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 神之键设置技能（GrandKeySetSkillReq / 763）。
/// 老实现是空函数（不回包）。这里把 key_list 里的 skill_id 写进存档并回 SUCC。
///
/// 和 759「激活技能」的区别：763 只是换技能（技能列表里点一个），
/// 不带到期时间；到期时间跟着钥匙的 EndTime 走，见 SetGrandKeySkill。
/// </summary>
[PacketCmdId(CmdId.GrandKeySetSkillReq)]
internal class GrandKeySetSkillReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GrandKeySetSkillReq req = packet.GetDecodedBody<GrandKeySetSkillReq>();
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
					continue;
				}
				bool found = scheme.Find(skill.KeyId) != null;
				detail.Append($"[key={skill.KeyId} skill={skill.SkillId}{(found ? "" : " 未解锁!")}] ");
				if (!found)
				{
					continue;
				}
				scheme.SetGrandKeySkill(skill.KeyId, skill.SkillId);
				n++;
			}
		}
		if (n > 0)
		{
			scheme.Save();
		}
		Packet.c.Log($"[GrandKey] 设置技能 收到 {total} 条，写入 {n} 条：{detail}");
		Packet.c.Log("[GrandKey] 写入后：" + GrandKeyService.DescribeScheme(scheme));

		session.Send(Packet.FromProto(new GrandKeySetSkillRsp
		{
			retcode = GrandKeySetSkillRsp.Retcode.Succ
		}, CmdId.GrandKeySetSkillRsp));

		if (n > 0)
		{
			GrandKeyService.PushMainInfo(session);
		}
	}
}
