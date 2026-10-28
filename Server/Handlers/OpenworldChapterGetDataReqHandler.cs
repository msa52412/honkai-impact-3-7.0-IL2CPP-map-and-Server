using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 开放世界章节数据。
///
/// 为什么它跟深渊有关
/// ------------------
/// 迪拉克之海在旧版是「开放世界」章节里的一个玩法（无尽挑战），
/// 不是挑战面板里的独立卡片。对应关系：
///
///   ChapterOWEndlessChallenge 表（15 条，MapID 全是 801）
///     └─ Group 1..5 × ChallengeID 1..15
///         └─ OpenworldChapter.EndlessChallengeLists[]
///
/// 客户端每次进世界地图都会发这个请求（实测 33 次），v2 之前没实现，
/// 于是开放世界章节打不开，迪拉克之海的入口自然出不来。
///
/// 这里先返回最小可用结构 + 打日志，确认客户端要的 MapId 是不是 801。
/// </summary>
[PacketCmdId(CmdId.OpenworldChapterGetDataReq)]
internal class OpenworldChapterGetDataReqHandler : IPacketHandler
{
	/// <summary>ChapterOWEndlessChallenge 表里的 ChallengeID 全量（1..15）。</summary>
	private static readonly uint[] EndlessChallengeIds = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

	public void Handle(Session session, Packet packet)
	{
		OpenworldChapterGetDataReq req = packet.GetDecodedBody<OpenworldChapterGetDataReq>();
		Packet.c.Log($"[Abyss] OpenworldChapterGetDataReq mapId={req.MapId}");

		OpenworldChapter chapter = new OpenworldChapter
		{
			MapId = req.MapId,
			Fame = 0u,
			FameLevel = 1u,
			MaxEquipCost = 0u,
			MaxRewardTakeNum = 0u,
			Challenge = new OpenworldChapterChallenge
			{
				TodayChallengeTimes = 0u,
				LastChallengeTime = 0u
			}
		};

		// 801 = ChapterOWEndlessChallenge 表里的 MapID，也就是「章节开放世界无尽挑战」。
		if (req.MapId == 801u)
		{
			foreach (uint id in EndlessChallengeIds)
			{
				chapter.EndlessChallengeLists.Add(new OpenworldChapterEndlessChallenge
				{
					ChallengeId = id,
					IsPassed = false,
					MaxScore = 0u
				});
			}
			Packet.c.Log($"[Abyss] 801 章节无尽挑战 {chapter.EndlessChallengeLists.Count} 条");
		}

		session.Send(Packet.FromProto(new OpenworldChapterGetDataRsp
		{
			retcode = OpenworldChapterGetDataRsp.Retcode.Succ,
			ChapterData = chapter
		}, CmdId.OpenworldChapterGetDataRsp));
	}
}
