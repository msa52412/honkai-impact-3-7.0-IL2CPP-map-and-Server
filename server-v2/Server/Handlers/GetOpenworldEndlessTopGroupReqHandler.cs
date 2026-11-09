using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3446/3447 —— 旧深渊「顶层分组」排行。
///
/// 客户端在分组界面点排行/升降级说明时请求。这里只回自己一条 +
/// 自己的 brief，保证名次区域能算出来、昵称不显示成裸 uid。
/// </summary>
[PacketCmdId(CmdId.GetOpenworldEndlessTopGroupReq)]
internal class GetOpenworldEndlessTopGroupReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetOpenworldEndlessTopGroupReq req = packet.GetDecodedBody<GetOpenworldEndlessTopGroupReq>();
		uint type = (uint)req.Type;
		if (type == 0u)
		{
			type = (uint)AbyssConfig.CurStatusType;
		}

		OwEndlessState state = OwEndlessState.Get(session.Player.User.Uid, type);
		OWEndlessGroupExcel group = OwEndlessConfig.ResolveGroup((int)type, (int)OwTuning.GroupLevel);
		uint score;
		uint maxScore;
		uint groupLevel;
		lock (state.Gate)
		{
			score = state.Score;
			maxScore = state.MaxScore;
			groupLevel = state.GroupLevel != 0u ? state.GroupLevel : (uint)group.GroupLevel;
		}

		GetOpenworldEndlessTopGroupRsp rsp = new GetOpenworldEndlessTopGroupRsp
		{
			retcode = GetOpenworldEndlessTopGroupRsp.Retcode.Succ,
			Type = (EndlessType)type,
			HardLevel = (uint)group.GroupHardLevel
		};
		rsp.EndlessDataLists.Add(new OpenworldEndlessPlayer
		{
			Uid = session.Player.User.Uid,
			Score = score,
			MaxScore = System.Math.Max(score, maxScore),
			ScoreTime = (uint)Global.GetUnixInSeconds(),
			GroupLevel = groupLevel
		});
		rsp.BriefDataLists.Add(new PlayerFriendBriefData
		{
			Uid = session.Player.User.Uid,
			Nickname = string.IsNullOrEmpty(session.Player.User.Nick)
				? session.Player.User.Name
				: session.Player.User.Nick,
			AvatarId = (uint)(session.Player.User.AssistantAvatarId != 0
				? session.Player.User.AssistantAvatarId
				: 101),
			Level = (uint)BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>
				.GetInstance().CalculateLevel(session.Player.User.Exp).Level,
			OnlineStatus = 1u
		});

		Packet.c.Log($"[Abyss] TopGroup type={(int)type} score={score} groupLevel={groupLevel} hard={group.GroupHardLevel}");

		session.Send(Packet.FromProto(rsp, CmdId.GetOpenworldEndlessTopGroupRsp));
	}
}
