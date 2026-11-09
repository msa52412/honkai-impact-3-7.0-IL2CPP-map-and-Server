using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetMainDataReq)]
internal class GetMainDataReqHandler : IPacketHandler
{
	// 这条日志每次登录会打好几次，只在「等级或档位变了」时才打，避免刷屏
	private static int _lastLoggedLevel = -1;
	private static uint _lastLoggedLock = uint.MaxValue;

	// 佩戴勋章同样只在变化时打一行，方便确认「登录到底下发了什么」
	private static uint _lastLoggedMedal = uint.MaxValue;

	public void Handle(Session session, Packet packet)
	{
		GetMainDataReq decodedBody = packet.GetDecodedBody<GetMainDataReq>();
		UserScheme user = session.Player.User;

		// 登录这一帧先把「按时间累积」的东西结清，再构造回包 —— 否则客户端拿到的是
		// 上一次退出时的旧值，要等下次心跳才纠正（体力条会停在原地）。
		AdventureService.SettleStamina(session.Player);
		ThemeWantedGameplay.Refresh(session.Player);
		AbyssSettlementRewards.Collect(session);
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
		// 「个性装饰」当前佩戴的挂件；0 视为未记录，回退到默认的琪亚娜夏日挂件。
		getMainDataRsp.OnPhonePendantId = user.OnPhonePendantId > 0 ? (uint)user.OnPhonePendantId : 350005u;
		// 单机版：体力锁 167（改的是存档对象，不是只改回包）
		GmLocks.Stamina(session.Player);
		getMainDataRsp.Stamina = (uint)user.Stamina;
		// 「累计登录 N 天」界面读的是这个字段（proto field 39），不是 LoginActivityData.login_days。
		// 之前一直是 0，客户端就显示成 1 天。建号时间缺失（v1 迁移账号 CreatedAt=0）时
		// 按 1970-01-01 至今算，和界面上「首次登录 1970-01-01」那句对得上。
		getMainDataRsp.TotalLoginDays = LoginActivityService.LoginDays(user.Uid, user.GetCreationTime());
		Packet.c.Log(string.Format("[MainData] uid={0} register_time={1} total_login_days={2}", user.Uid, getMainDataRsp.RegisterTime, getMainDataRsp.TotalLoginDays));
		getMainDataRsp.StaminaRecoverConfigTime = 360u;
		// 写死 360 会让体力条恒显「6 分钟回满」；这里按实际存储档算。
		getMainDataRsp.StaminaRecoverLeftTime = StaminaStorageGameplay.RecoverLeft(session.Player);
		// 装备仓容量：客户端拿它跟「已持有圣痕+武器件数」比大小来判断能不能抽卡/领取。
		// 这个存档是全物品存档（1290 圣痕 + 1268 武器），写死 1000 会让补给页一直报「仓库满」，
		// 连 GachaReq 都发不出去。改成按现持有量动态抬高，见 GachaService.EquipmentSizeLimit。
		getMainDataRsp.EquipmentSizeLimit = GachaService.EquipmentSizeLimit(session.Player.Equipment);
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
                // 舰桥天气：登录第一帧就用这份数据，必须和 GetWarshipDataRsp 一致。
                // 以前这里写死 `true / 3`（雷雨），所以登录会先闪几秒雷再被后面的包纠正。
                getMainDataRsp.WarshipTheme = Common.Database.WarshipSettings.ThemeOf(user.Uid, (uint)user.WarshipId);
		// 原来这里写死 1u，会覆盖上面按真实天数算出来的值 —— 已删除。
		getMainDataRsp.NextEvaluateTime = 0u;
		// 「收藏 → 勋章」当前佩戴的那一枚。
		// 两个字段一起写：field 24 on_medal_id（旧）+ field 43 on_medal（客户端实际读的结构体）。
		// 只写 24 的话重登后界面会变回「未佩戴」—— 客户端不看那个字段。
		Medal onMedal = MedalService.CurrentMedal(user.Uid);
		getMainDataRsp.OnMedalId = onMedal != null ? onMedal.Id : 0u;
		getMainDataRsp.OnMedal = onMedal;
		if (getMainDataRsp.OnMedalId != _lastLoggedMedal)
		{
			_lastLoggedMedal = getMainDataRsp.OnMedalId;
			Packet.c.Log("[Medal] GetMainData 下发 on_medal=" + getMainDataRsp.OnMedalId
				+ (onMedal != null ? " end_time=" + onMedal.EndTime : " (未佩戴)"));
		}
		getMainDataRsp.TodayRechargePrice = 0u;
		GetMainDataRsp proto = getMainDataRsp;
		session.Send(Packet.FromProto(proto, CmdId.GetMainDataRsp));

		// 世界加载阶段再补一枪家园小队主信息（4306）。
		// 为什么在这里：玩家登录后客户端会批量拉一堆数据，这一批一定发生在「家园页被创建」之前，
		// 而 PlayerLoginReq 那一刻模块可能还没构造好。详见 SanctuaryService.PushMainInfo。
		SanctuaryService.PushMainInfo(session);

		// 冒险委托主页（3900）同理：登录后客户端不一定立刻拉，主动推一份。
		AdventureService.Push(session);
	}
}
