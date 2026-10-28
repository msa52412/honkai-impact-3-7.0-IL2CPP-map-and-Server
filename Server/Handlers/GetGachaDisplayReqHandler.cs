using System.Linq;
using Common;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetGachaDisplayReq)]
internal class GetGachaDisplayReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetGachaDisplayRsp getGachaDisplayRsp = new GetGachaDisplayRsp
		{
			retcode = GetGachaDisplayRsp.Retcode.Succ,
			IsAll = true,
			Type = GachaType.GachaTypeError,
			GachaRandom = (uint)Global.GetUnixInSeconds()
		};
		getGachaDisplayRsp.GachaDisplayInfoLists.Add(new GachaDisplayInfo
		{
			GachaType = GachaType.GachaCustomAvatar,
			CommonData = new GachaDisplayCommonData
			{
				TitleImage = "SpriteOutput/Gacha/TitleKakin3",
				Title = "5.18~6.16",
				Content = "To feed ur gacha addiction",
				IsEnablePrompt = true,
				GachaId = 30115600u,
				DataBeginTime = 1684353600u,
				DataEndTime = 2684353600u,
				UpAvatarLists = (from avatar in BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All
					where avatar.AvatarId < 9000
					select (uint)avatar.AvatarId).ToArray()
			},
			CustomGachaData = new CustomGachaData
			{
				TicketHcoinCost = 280u,
				TicketMaterialId = 1103u,
				IsEnableBaodi = true,
				GachaType = GachaType.GachaCustomAvatar,
				GachaTimes = 0u,
				DisplayMaxTimes = 100u,
				NoProtectGachaTimes = 0u,
				DisplayVideoAvatar = 0u,
				ShiningType = 1u,
				ExId = 1u,
				ProtectDisplayInfo = new GachaProtectDisplayInfo
				{
					NoProtectGachaTimes = 0u,
					DisplayKeyAvatar = 0u,
					protect_display_type = GachaProtectDisplayInfo.ProtectDisplayType.NoDisplay
				}
			}
		});
		session.Send(Packet.FromProto(getGachaDisplayRsp, CmdId.GetGachaDisplayRsp));
	}
}
