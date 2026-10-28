using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetPlayerTokenReq)]
internal class GetPlayerTokenReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet _packet)
	{
		GetPlayerTokenReq decodedBody = _packet.GetDecodedBody<GetPlayerTokenReq>();
		GetPlayerTokenRsp getPlayerTokenRsp = new GetPlayerTokenRsp();
		UserScheme userScheme = User.FromToken(decodedBody.AccountToken);
		if (userScheme == null || userScheme.Uid != uint.Parse(decodedBody.AccountUid))
		{
			getPlayerTokenRsp.retcode = GetPlayerTokenRsp.Retcode.AccountVerifyError;
			getPlayerTokenRsp.Msg = "Account verification failed, please re-login!";
		}
		else
		{
			session.Player = new Player(userScheme);
			if (session.Player.User.IsFirstLogin)
			{
				AvatarScheme avatarScheme = Common.Database.Avatar.Create(101, session.Player.User.Uid, session.Player.Equipment);
				session.Player.AvatarList = session.Player.AvatarList.Append(avatarScheme).ToArray();
				if (Global.config.VerboseLevel > VerboseLevel.Silent)
				{
					Logger c = session.c;
					string[] array = new string[1];
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Automatically created avatar with id: ");
					defaultInterpolatedStringHandler.AppendFormatted(avatarScheme.AvatarId);
					array[0] = defaultInterpolatedStringHandler.ToStringAndClear();
					c.Log(array);
				}
			}
			getPlayerTokenRsp = new GetPlayerTokenRsp
			{
				retcode = GetPlayerTokenRsp.Retcode.Succ,
				Uid = userScheme.Uid,
				Token = userScheme.Token,
				AccountType = decodedBody.AccountType,
				AccountUid = decodedBody.AccountUid,
				UserType = 4u,
				HoyolabAccountUid = decodedBody.AccountUid,
				FightserverIp = 0u,
				FightserverPort = 0u
			};
		}
		session.Send(Packet.FromProto(getPlayerTokenRsp, CmdId.GetPlayerTokenRsp));
	}
}
