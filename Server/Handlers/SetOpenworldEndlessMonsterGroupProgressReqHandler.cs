using System;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3448/3449 —— 组内进度同步。
///
/// 客户端 OWEndLessBattleLevelInfo.OnSetMonsterGroupProgressRsp 只做一件事：
///     SetGroupProgressDirty(rsp.monster_group_list)
/// 所以回包必须把请求里的 progress 原样带回来，否则界面上的进度条会停在旧值。
///
/// progress 与 GetEndlessProgress 同尺度：0..10000（10000 = 本组完成）。
/// </summary>
[PacketCmdId(CmdId.SetOpenworldEndlessMonsterGroupProgressReq)]
internal class SetOpenworldEndlessMonsterGroupProgressReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SetOpenworldEndlessMonsterGroupProgressReq req = packet.GetDecodedBody<SetOpenworldEndlessMonsterGroupProgressReq>();
		uint type = (uint)req.Type;
		if (type == 0u)
		{
			type = (uint)EndlessType.EndlessTypeOpenworld;
		}

		(OwEndlessState state, _) = OwEndlessState.Ensure(session.Player.User.Uid, (int)type);

		uint progress = Math.Min(req.Progress, 10000u);
		uint oldProgress;
		lock (state.Gate)
		{
			oldProgress = state.MonsterProgress;
			state.MonsterProgress = progress;
		}

		SetOpenworldEndlessMonsterGroupProgressRsp rsp = new SetOpenworldEndlessMonsterGroupProgressRsp
		{
			retcode = SetOpenworldEndlessMonsterGroupProgressRsp.Retcode.Succ,
			Type = (EndlessType)type
		};
		rsp.MonsterGroupLists.Add(new OpenworldEndlessMonsterGroup
		{
			GroupId = req.GroupId,
			Progress = progress,
			OldProgress = oldProgress
		});

		Packet.c.Log($"[Abyss] SetGroupProgress type={(int)type} group={req.GroupId} "
			+ $"progress={oldProgress}->{progress}");

		session.Send(Packet.FromProto(rsp, CmdId.SetOpenworldEndlessMonsterGroupProgressRsp));

		// 组内进度也落盘：否则服务端一重启，客户端回来看到的进度条会跳回去。
		OwEndlessState.Persist();
	}
}
