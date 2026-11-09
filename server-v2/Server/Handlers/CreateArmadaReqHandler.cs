using Common.Resources.Proto;
using PemukulPaku.GameServer.MPModule;

namespace PemukulPaku.GameServer.Handlers.One;

/// <summary>
/// 建团（创建舰团）。客户端「舰队」页里的建团按钮发这个。
///
/// 校验顺序照着客户端枚举来：已在团内 -> IN_ARMADA；名字为空 -> NAME_FORMAT_ERROR；
/// 重名 -> NAME_REPEAT。成功后回 armada_id / armada_name，
/// 客户端随后会再发一次 GetArmadaDataReq 拿完整数据。
/// </summary>
[PacketCmdId(CmdId.CreateArmadaReq)]
internal class CreateArmadaReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		CreateArmadaReq req = packet.GetDecodedBody<CreateArmadaReq>();
		ArmadaModule module = ArmadaModule.GetInstance();
		uint uid = session.Player.User.Uid;
		string name = (req.Name ?? "").Trim();

		if (module.ByUid(uid) != null)
		{
			session.Send(Packet.FromProto(new CreateArmadaRsp
			{
				retcode = CreateArmadaRsp.Retcode.InArmada
			}, CmdId.CreateArmadaRsp));
			return;
		}

		if (name.Length == 0)
		{
			session.Send(Packet.FromProto(new CreateArmadaRsp
			{
				retcode = CreateArmadaRsp.Retcode.NameFormatError
			}, CmdId.CreateArmadaRsp));
			return;
		}

		if (module.NameExists(name))
		{
			session.Send(Packet.FromProto(new CreateArmadaRsp
			{
				retcode = CreateArmadaRsp.Retcode.NameRepeat
			}, CmdId.CreateArmadaRsp));
			return;
		}

		Armada armada = module.Create(uid, session.Player.User.Nick ?? "", name, req.Label);
		session.Send(Packet.FromProto(new CreateArmadaRsp
		{
			retcode = CreateArmadaRsp.Retcode.Succ,
			ArmadaId = armada.Id,
			ArmadaName = armada.Name,
			AuditArmadaName = armada.Name
		}, CmdId.CreateArmadaRsp));
	}
}
