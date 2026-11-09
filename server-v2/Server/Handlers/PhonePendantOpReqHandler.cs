using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 个性装饰的「更换 / 卸下」（PhoneStrapDialogContext 底部按钮）。
///
/// 客户端 PlayerModule.OnPhonePendantOpRsp（0x182C82AE0）拿到回包后：
///     retcode != 0            → 直接返回，什么都不改
///     op == WEAR(1)           → MainData.OnPhonePendantId = rsp.on_id
///     op == OFF(2) && 当前 id == rsp.off_id → MainData.OnPhonePendantId = 0
/// 所以服务端把 op / on_id / off_id 原样回显即可，同时把玩家数据落库，
/// 重登后 GetMainDataRsp.OnPhonePendantId 才不会退回默认值。
/// </summary>
[PacketCmdId(CmdId.PhonePendantOpReq)]
internal class PhonePendantOpReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		PhonePendantOpReq req = packet.GetDecodedBody<PhonePendantOpReq>();
		PhonePendantOpRsp rsp = new PhonePendantOpRsp
		{
			retcode = PhonePendantOpRsp.Retcode.Succ,
			Op = req.Op,
			OnId = req.OnId,
			OffId = req.OffId
		};

		if (req.Op == PhonePendantOpType.PhonePendantWear)
		{
			PhonePendantDataExcel? config = PhonePendantData.GetInstance().FromId(req.OnId);
			if (config == null)
			{
				Packet.c.Warn(string.Format("[PhonePendant] 穿戴失败：{0} 不在 PhonePendantData.json", req.OnId));
				rsp.retcode = PhonePendantOpRsp.Retcode.Fail;
			}
			else
			{
				session.Player.User.OnPhonePendantId = (int)req.OnId;
				session.Player.User.Save();
			}
		}
		else if (req.Op == PhonePendantOpType.PhonePendantOff)
		{
			session.Player.User.OnPhonePendantId = 0;
			session.Player.User.Save();
		}

		Packet.c.Log(string.Format("[PhonePendant] op={0} on={1} off={2} retcode={3}",
			req.Op, req.OnId, req.OffId, rsp.retcode));
		session.Send(Packet.FromProto(rsp, CmdId.PhonePendantOpRsp));
	}
}
