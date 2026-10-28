using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetMainDataReq)]
internal class GetMainDataReqHandler : IPacketHandler
{
	// 这条日志每次登录会打好几次，只在「等级或档位变了」时才打，避免刷屏
	private static int _lastLoggedLevel = -1;
	private static uint _lastLoggedLock = uint.MaxValue;

	public void Handle(Session session, Packet packet)
	{
		GetMainDataReq decodedBody = packet.GetDecodedBody<GetMainDataReq>();
		UserScheme user = session.Player.User;
		PlayerLevelData.LevelData levelData = BaseExcelReader<PlayerLevelData, PlayerLevelDataExcel>.GetInstance().CalculateLevel(user.Exp);
		GetMainDataRsp getMainDataRsp = new GetMainDataRsp();
		getMainDataRsp.retcode = GetMainDataRsp.Retcode.Succ;
		getMainDataRsp.AssistantAvatarId = (uint)user.AssistantAvatarId;
		getMainDataRsp.Birthday = (uint)user.BirthDate;
		getMainDataRsp.Nickname = user.Nick;
		getMainDataRsp.Level = (uint)levelData.Level;
		getMainDataRsp.Exp = (uint)levelData.Exp;
		getMainDataRsp.FreeHcoin = (uint)user.Hcoin;
		getMainDataRsp.Hcoin = (uint)user.Hcoin;
		getMainDataRsp.CustomHeadId = (uint)user.CustomHeadId;
		getMainDataRsp.Scoin = session.Player.Equipment.MaterialList.Where((Common.Resources.Proto.Material mat) => mat.Id == 100).FirstOrDefault()?.Num ?? 0;
		getMainDataRsp.IsAll = true;
		getMainDataRsp.RegisterTime = user.GetCreationTime();
		getMainDataRsp.PayHcoin = 0u;
		getMainDataRsp.WarshipAvatar = user.WarshipAvatar;
		getMainDataRsp.SelfDesc = user.SelfDesc;
		getMainDataRsp.UseFrameId = ((user.FrameId < 200001) ? 200001u : ((uint)user.FrameId));
		getMainDataRsp.OnPhonePendantId = 350005u;
		getMainDataRsp.Stamina = (uint)user.Stamina;
		getMainDataRsp.StaminaRecoverConfigTime = 360u;
		getMainDataRsp.StaminaRecoverLeftTime = 360u;
		getMainDataRsp.EquipmentSizeLimit = 1000u;
		getMainDataRsp.OpenPanelActivityLists = new uint[1] { 2u };
		getMainDataRsp.ChatworldActivityInfo = new ChatworldActivityInfo
		{
			IsHasNpcRedEnvelope = false,
			TreasureScheduleId = 0u
		};
		getMainDataRsp.IsAllowCostSeniorEquipOnCurDevice = true;
		getMainDataRsp.TypeLists = new uint[31]
		{
			2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u, 14u, 16u,
			17u, 18u, 19u, 20u, 21u, 22u, 23u, 24u, 26u, 27u,
			28u, 29u, 30u, 31u, 32u, 33u, 35u, 36u, 37u, 38u,
			39u
		};
		// 2026-10-22 12:xx 等级锁修复
		// ----------------------------
		// 原来是硬编码 1u（高级区，MaxLevel=80），但存档里玩家是 88 级 —— 自相矛盾。
		// 客户端拿到「88 级 + 高级区」这种组合没法判断该按哪一档渲染深渊，
		// 于是挑战面板一直按未突破处理。
		// 改成按等级自动推（Auto），88 级会解析成 2（终极区，MaxLevel=88，红框），状态自洽。
		// 想强制回高级区做对照实验：游戏内聊天打 `abyss lock 1`。
		getMainDataRsp.LevelLockId = LevelLockConfig.Resolve(levelData.Level);
		if (levelData.Level != _lastLoggedLevel || getMainDataRsp.LevelLockId != _lastLoggedLock)
		{
			_lastLoggedLevel = levelData.Level;
			_lastLoggedLock = getMainDataRsp.LevelLockId;
			Packet.c.Log("[LevelLock] GetMainData -> " + LevelLockConfig.Describe(levelData.Level));
		}
		getMainDataRsp.Mcoin = 0u;
		getMainDataRsp.MonthRechargePrice = 0u;
		// 舰桥天气固定为雷雨：WeatherID 3 = Lightning（ThemeWeatherConfig / ThemeWeatherAtmosphere）。
		getMainDataRsp.WarshipTheme = new WarshipThemeData
		{
			WarshipId = (uint)user.WarshipId,
			IsWeatherFixed = true,
			WeatherIdx = 3u
		};
		getMainDataRsp.TotalLoginDays = 1u;
		getMainDataRsp.NextEvaluateTime = 0u;
		getMainDataRsp.OnMedalId = 0u;
		getMainDataRsp.TodayRechargePrice = 0u;
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp));
	}
}
