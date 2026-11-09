using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 徽章佩戴 / 替换 / 卸下（451 → 452）。
///
/// 之前这个包完全没有 handler，日志里一条条 `MedalOpReq not handled!`，
/// 所以「收藏 → 勋章」里点装备没有任何反应。
///
/// 账号一律取会话里的 uid，不信任客户端传来的任何账号信息。
/// 成功后会补一包只带 on_medal_id 的 GetMainDataRsp，让界面立刻跟上；
/// 失败则原样回 retcode，不动已保存的选择。
/// </summary>
[PacketCmdId(CmdId.MedalOpReq)]
internal class MedalOpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		MedalOpReq req = packet.GetDecodedBody<MedalOpReq>();
		uint uid = session.Player.User.Uid;

		MedalOpRsp rsp = new MedalOpRsp
		{
			retcode = MedalOpRsp.Retcode.Fail,
			Op = req.Op
		};

		uint onId = 0u;
		uint offId = 0u;

		switch (req.Op)
		{
			case MedalOpType.MedalWear:
				rsp.retcode = MedalService.Wear(uid, req.OnId, req.OffId, out onId, out offId);
				break;

			case MedalOpType.MedalOff:
				rsp.retcode = MedalService.Off(uid, req.OffId, out onId, out offId);
				break;

			default:
				Packet.c.Log(string.Format("[Medal] 未知操作 uid={0} op={1}", uid, (int)req.Op));
				rsp.retcode = MedalOpRsp.Retcode.Fail;
				break;
		}

		rsp.OnId = onId;
		rsp.OffId = offId;

		Packet.c.Log(string.Format("[Medal] uid={0} op={1} req(on={2},off={3}) -> ret={4} on={5} off={6}",
			uid, (int)req.Op, req.OnId, req.OffId, (int)rsp.retcode, onId, offId));

		session.Send(Packet.FromProto(rsp, CmdId.MedalOpRsp));

		if (rsp.retcode == MedalOpRsp.Retcode.Succ)
		{
			// 只带勋章相关字段的部分主数据：客户端按字段合并，和 SetWarshipReq 的做法一致。
			// 不这么做的话界面要等下一次完整 GetMainData 才会变。
			// 注意 field 43 on_medal 是客户端真正读的那个，24 on_medal_id 只是顺带。
			Medal onMedal = MedalService.CurrentMedal(uid);
			session.Send(Packet.FromProto(new GetMainDataRsp
			{
				retcode = GetMainDataRsp.Retcode.Succ,
				OnMedalId = onMedal != null ? onMedal.Id : 0u,
				OnMedal = onMedal
			}, CmdId.GetMainDataRsp));
		}
	}
}
