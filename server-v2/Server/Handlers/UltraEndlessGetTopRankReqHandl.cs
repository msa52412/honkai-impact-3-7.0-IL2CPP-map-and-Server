using Common.Utils.ExcelReader;
using System.Collections.Generic;
using Common;
using Common.Resources.Proto;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 超弦空间「排行榜」页（5200 请求 / 5201 回包）。
///
/// 原来的实现是写死 <c>IsFeatureClosed = true, MyRank = 1, MyScore = 0</c>，
/// rank_list 一条都不给 —— 于是：
///   · 页面认为排行榜功能关闭，整页不画
///   · UltraEndlessRankPageContext.RefreshView 里
///     <c>GetTopRank(scheduleID)</c> 拿到的是空 rank_data
///   · 客户端每 5 秒重发一次 5200（日志里 18:54:20/25/30/35/40 就是这个）
///
/// 现在按主界面（5203）同一份数据下发：假玩家阶梯 + 自己，
/// 分数口径同样是「本期总分」，名次也和主界面一致。
///
/// ★ schedule_id 必须回 5203 下发的那个期号（见下面 Handle 里的注释），
///   否则客户端字典 key 对不上，页面还是空白。
/// </summary>
[PacketCmdId(CmdId.UltraEndlessGetTopRankReq)]
internal class UltraEndlessGetTopRankReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
        session.Send(Packet.FromProto(Build(session,packet.GetDecodedBody<UltraEndlessGetTopRankReq>()),CmdId.UltraEndlessGetTopRankRsp));
    }
    internal static UltraEndlessGetTopRankRsp Build(Session session,UltraEndlessGetTopRankReq decodedBody)
    {

		// ── 期号：必须和 5203 一致 ──
		// UltraEndlessModule.OnGetTopRankRsp @ 0x183866F60：
		//     0x183866F97  cmp dword [rax+0x10], 0    ; rsp.retcode != 0 → return
		//     0x183866FB4  mov edx, [rax+0x14]        ; key = rsp.schedule_id
		//     0x183866FB0  mov r8,  [rax+0x18]        ; value = rsp.rank_data
		//     0x183866FB7  call Dictionary.set_Item
		// 而 UltraEndlessRankPageContext.RefreshView @ 0x182B154D0 是用
		// module.ScheduleID（来自 5203 的 rsp.schedule_id）去 GetTopRank 查这张表。
		// 请求里的 schedule_id 是客户端 RequestUltraEndlessTopRank @ 0x1815BAA3E
		// 写进 [req+0x10] 的，正常情况下就是 module.ScheduleID；
		// 万一是 0（未设置），回 0 会让字典 key 变成 0 → 查不到 → 页面永远空白。
		uint scheduleId = UltraEndlessCycle.Enabled ? UltraEndlessState.EffectiveScheduleId : decodedBody.ScheduleId != 0u
			? decodedBody.ScheduleId
			: UltraEndlessState.EffectiveScheduleId;

		Common.Database.UserScheme user = session.Player.User;

		// 分数口径和 5203 完全一致：Σ 本期所有站点 Σ 各层最高分。
		uint selfScore = UltraEndlessRank.TotalScore(UltraEndlessState.Get(user.Uid));
		uint groupLevel = UltraEndlessGetMainDataReqHandler.ResolveGroupLevel(session, out _);

		List<UltraEndlessRank.Row> rows = UltraEndlessRank.BuildRows(user.Uid, selfScore);

		RankShowData rankData = new RankShowData
		{
			// 写死 true 会让客户端认为功能关闭，这里必须是 false。
			IsFeatureClosed = false,
			// 1 = 个人榜（客户端只用它区分展示文案）
			MyRankType = 1u,
			MyScore = selfScore
		};

		uint avatarId = (uint)((user.AssistantAvatarId != 0) ? user.AssistantAvatarId : 101);
		string nick = string.IsNullOrEmpty(user.Nick) ? user.Name : user.Nick;

		for (int i = 0; i < rows.Count; i++)
		{
			uint rank = (uint)i + 1u;
			UltraEndlessRank.Row row = rows[i];
			var bot = !row.IsSelf && UltraEndlessCycle.Enabled ? UltraEndlessCycle.Bot(row.Uid) : null;
			if (row.IsSelf)
			{
				rankData.MyRank = rank;
			}

			// 排行榜行的每一列（UltraEndlessRankPageContext.RefreshRankRow）：
			//   名次 → 参数 rank；分数 → data.score；昵称 → data.nick_name
			//   头像/头像框/自定义头像 → data.avatar_id / frame_id / custom_head_id
			//   组别列 → data.ultra_endless_group_level
			rankData.RankLists.Add(new UserRankData
			{
				Uid = row.Uid,
				Score = checked(row.Score + (uint)BaseExcelReader<UltraEndlessGroup,UltraEndlessGroupExcel>.GetInstance().ByLevel((int)groupLevel).GroupId * 1000000u),
				Rank = rank,
				NickName = row.IsSelf ? nick : bot?.Nickname ?? $"挑战者{rank:00}",
				AvatarId = row.IsSelf ? avatarId : bot?.Avatar ?? 101u,
				FrameId = row.IsSelf ? (uint)user.FrameId : bot?.Frame ?? 200001u,
				CustomHeadId = row.IsSelf ? (uint)user.CustomHeadId : bot?.Head ?? 161001u,
				UltraEndlessGroupLevel = groupLevel,
				IsLogoff = false
			});
		}

		Packet.c.Log($"[Ultra] 5201 GetTopRank: req={decodedBody.ScheduleId} -> schedule={scheduleId} "
			+ $"level={groupLevel} myScore={selfScore}/{UltraEndlessRank.MaxPossibleScore()} "
			+ $"myRank={rankData.MyRank} rows={rankData.RankLists.Count} "
			+ $"top={rankData.RankLists[0].Score}");

		return new UltraEndlessGetTopRankRsp
		{
			retcode = UltraEndlessGetTopRankRsp.Retcode.Succ,
			ScheduleId = scheduleId,
			RankData = rankData
		};
	}
}
