using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 3462/3463 —— 客户端切换当前深渊类型。
///
/// 客户端 EndlessSelectTypeRsp 到达后调用 TwoEndless.Refresh(rsp)，
/// 后续的 GetEndlessStatus / GoToOWEndless 都会跟着这个选择走。
/// 服务端必须同步 AbyssConfig.SelectedEndlessType / CurStatusType，
/// 否则会出现「界面上选的是量子，3419 却仍按迪拉克应答」。
/// </summary>
[PacketCmdId(CmdId.EndlessSelectTypeReq)]
internal class EndlessSelectTypeReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		EndlessSelectTypeReq req = packet.GetDecodedBody<EndlessSelectTypeReq>();
		uint selected = SelectedOf(req.EndlessType);
		if (selected != 0u)
		{
			AbyssConfig.SetSelected(selected);
		}
		Packet.c.Log($"[Abyss] SelectType type={(int)req.EndlessType} level={req.Level} -> {AbyssConfig.Describe()}");

		session.Send(Packet.FromProto(new EndlessSelectTypeRsp
		{
			retcode = EndlessSelectTypeRsp.Retcode.Succ,
			EndlessType = req.EndlessType
		}, CmdId.EndlessSelectTypeRsp));
	}

	/// <summary>EndlessType → AbyssConfig.SetSelected 使用的 1..6 序号。</summary>
	private static uint SelectedOf(EndlessType type)
	{
		return type switch
		{
			EndlessType.EndlessTypeNormal => 1u,
			EndlessType.EndlessTypeOpenworld => 2u,
			EndlessType.EndlessTypeQuantum => 3u,
			EndlessType.EndlessTypeUltra => 4u,
			EndlessType.EndlessTypeGreedy => 5u,
			EndlessType.EndlessTypeFront => 6u,
			_ => 0u
		};
	}
}
