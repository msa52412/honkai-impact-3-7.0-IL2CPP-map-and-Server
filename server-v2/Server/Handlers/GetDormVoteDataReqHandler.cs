using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 家园装扮投票活动（GetDormVoteDataReq 1138）。
/// 私服没有投票活动排期，固定回 NOT_START，让客户端安心走空数据分支。
/// </summary>
[PacketCmdId(CmdId.GetDormVoteDataReq)]
internal class GetDormVoteDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetDormVoteDataRsp rsp = new GetDormVoteDataRsp
		{
			retcode = GetDormVoteDataRsp.Retcode.Succ,
			ActivityId = 1u,
			status = GetDormVoteDataRsp.Status.NotStart,
			EndTime = 0u,
			Url = ""
		};

		session.Send(Packet.FromProto(rsp, CmdId.GetDormVoteDataRsp));
	}
}
