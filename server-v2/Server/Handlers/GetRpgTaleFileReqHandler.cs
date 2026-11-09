using System;
using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 2378 / 2379 —— 「RPG 故事」的档案列表（角色挑战页面 `MonoGeneralAvatarActLevelPage.SetupFiles`）。
///
/// 原来没 handler（日志 `GetRpgTaleFileReq not handled!` 一次刷 12 条），
/// 客户端拿不到 file_id_list，档案/章节数据就一直是空的。
/// </summary>
[PacketCmdId(CmdId.GetRpgTaleFileReq)]
internal class GetRpgTaleFileReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetRpgTaleFileReq req = packet.GetDecodedBody<GetRpgTaleFileReq>();

		GetRpgTaleFileRsp rsp = new GetRpgTaleFileRsp
		{
			retcode = GetRpgTaleFileRsp.Retcode.Succ,
			TaleId = req.TaleId,
			FileIdLists = RpgFilesCfg.GetInstance().FileIdsOf(req.TaleId).ToArray()
		};

		Packet.c.Log($"[RpgTale] GetRpgTaleFile tale={req.TaleId} -> Succ files={rsp.FileIdLists.Length}");
		session.Send(Packet.FromProto(rsp, CmdId.GetRpgTaleFileRsp));
	}
}
