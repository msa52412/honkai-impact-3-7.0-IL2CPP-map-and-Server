using System;
using Common;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers.Dlc;

/// <summary>
/// 3550 / 3551 —— 后崩坏书（DLC）基础数据，界面左上角的「调查等级」就是这里的 level。
///
/// 服务端原来没有 handler（日志里每次登录都是 `GetDLCReq not handled!`），
/// 客户端拿不到 level/exp，等级就一直是空的。
///
/// 客户端 OnGetDLCRsp（0x1827B44B0）直接把 rsp.level 存成当前等级，
/// 不会按经验反算，所以这里直接下发目标等级即可。
/// </summary>
[PacketCmdId(CmdId.GetDLCReq)]
internal class GetDLCReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		uint level = DlcService.TargetLevel;

		GetDLCRsp rsp = new GetDLCRsp
		{
			retcode = GetDLCRsp.Retcode.Succ,
			Level = level,
			// 满级时该级没有「升到下一级」的经验需求，回 0 避免进度条显示成可升级。
			Exp = 0u,
			// 奖励领到当前级，界面不会一直挂红点。
			HasTakeRewardLevel = level,
			FinishedDialogIdLists = Array.Empty<uint>()
		};

		Packet.c.Log($"[DLC] GetDLCReq -> Succ level={level}/{DlcService.MaxLevel} exp=0 rewardTaken={level}");
		session.Send(Packet.FromProto(rsp, CmdId.GetDLCRsp));
	}
}
