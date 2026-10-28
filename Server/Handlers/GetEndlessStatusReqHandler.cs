using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetEndlessStatusReq)]
internal class GetEndlessStatusReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		// 每次下发都记一行：这是判断「客户端被哪套深渊牵着走」的观测点。
		Packet.c.Log("[Abyss] GetEndlessStatus -> " + AbyssConfig.Describe());
		// 本期时间窗：原来三处都写 BeginTime = 0u，客户端排期界面就显示 1970-…。
		// 现在统一由 OwTuning.Period 给（abyss_ow.json 的 period_* 键可调）。
		OwTuning.Period(out uint beginTime, out uint endTime, out uint closeTime);

		GetEndlessStatusRsp proto = new GetEndlessStatusRsp
		{
			retcode = GetEndlessStatusRsp.Retcode.Succ,
			CurStatus = new EndlessStatus
			{
				BeginTime = beginTime,
				EndTime = endTime,
				CloseTime = closeTime,
				// 原来硬编码 Ultra，但 SelectedEndlessType 又写 5（贪婪），两者打架。
				// 现在统一由 AbyssConfig 决定，默认值和改之前一致。
				EndlessType = AbyssConfig.CurStatusType,
				// 2026-10-22 11:40：这里原来我加过 GroupLevel = 存档值(8)，
				// 但基线（17:26 唯一成功那次）这个字段是不下发的。
				// 为了做受控实验，先还原成不下发；等确认它跟跳转有没有关系再决定。
				CanJoinIn = true
			},
			SelectedEndlessType = AbyssConfig.SelectedEndlessType
		};

		// 把其它深渊也挂上去。原来这里是空的，客户端「挑战」面板就只有一张卡。
		if (AbyssConfig.ShowOtherEndless)
		{
			foreach (EndlessType type in AbyssConfig.OtherEndless)
			{
				if (type == AbyssConfig.CurStatusType)
				{
					continue;
				}
				proto.NextStatusLists.Add(new EndlessStatus
				{
					BeginTime = beginTime,
					EndTime = endTime,
					CloseTime = closeTime,
					EndlessType = type,
					CanJoinIn = true,
					// 这里必须用「旧深渊（迪拉克/量子）」的分组等级，不能用 AbyssGroupLevel。
					// AbyssGroupLevel 是超弦空间那套 1..9 杯数档位，落在客户端
					// OWEndlessGroup 表（有效集 21/22/23/24）之外，
					// 会让 RefreshPlayerGroup+0x404 越界崩掉。
					GroupLevel = AbyssConfig.OwGroupLevel,
					ProgressOrScore = 0u
				});
			}
			Packet.c.Log($"[Abyss] NextStatusLists = {proto.NextStatusLists.Count} 条");
		}

		session.Send(Packet.FromProto(proto, CmdId.GetEndlessStatusRsp));
	}
}
