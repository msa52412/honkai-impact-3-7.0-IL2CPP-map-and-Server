using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.SelectNewStigmataRuneReq)]
internal class SelectNewStigmataRuneReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		SelectNewStigmataRuneReq decodedBody = packet.GetDecodedBody<SelectNewStigmataRuneReq>();
		SelectNewStigmataRuneRsp proto = new SelectNewStigmataRuneRsp
		{
			retcode = SelectNewStigmataRuneRsp.Retcode.Succ,
			SelectUniqueId = decodedBody.SelectUniqueId,
			IsSelect = decodedBody.IsSelect
		};
		uint uid = decodedBody.UniqueId;
		Stigmata stigmata = session.Player.Equipment.StigmataList.FirstOrDefault((Stigmata stig) => stig.UniqueId == uid);
		if (stigmata != null)
		{
			if (decodedBody.IsSelect && stigmata.WaitSelectRuneGroupLists != null)
			{
				stigmata.RuneLists.Clear();
				foreach (StigmataRuneGroup waitSelectRuneGroupList in stigmata.WaitSelectRuneGroupLists)
				{
					if (waitSelectRuneGroupList.UniqueId != decodedBody.SelectUniqueId)
					{
						continue;
					}
					for (int i = 0; i < waitSelectRuneGroupList.RuneLists.Count; i++)
					{
						StigmataRune stigmataRune = waitSelectRuneGroupList.RuneLists[i];
						if (stigmataRune != null)
						{
							stigmata.RuneLists.Add(stigmataRune);
						}
					}
				}
			}
			stigmata.WaitSelectRuneGroupLists?.Clear();
		}
		session.Player.Equipment.Save();
		session.Send(Packet.FromProto(proto, CmdId.SelectNewStigmataRuneRsp));
	}
}
