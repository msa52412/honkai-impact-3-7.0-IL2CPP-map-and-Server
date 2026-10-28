using System.IO;
using Common;
using Common.Database;
using Common.Resources.Proto;
using ProtoBuf;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.PlayerLoginReq)]
internal class PlayerLoginReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		UserScheme user = session.Player.User;
		if (Login.UserLogin(user.Uid))
		{
			session.Player.ResetAvatarsTodayGoodfeel();
		}
		PlayerLoginRsp proto = new PlayerLoginRsp
		{
			retcode = PlayerLoginRsp.Retcode.Succ,
			IsFirstLogin = user.IsFirstLogin,
			RegionName = Global.config.Gameserver.RegionName,
			CgType = (user.IsFirstLogin ? CGType.CgStart : CGType.CgSevenChapter),
			RegionId = 248u,
			LoginSessionToken = 1u,
			PsychoKey = 0u
		};
		session.Send(Packet.FromProto(proto, CmdId.PlayerLoginRsp), Packet.FromProto(new GetMpDataRsp
		{
			retcode = GetMpDataRsp.Retcode.Succ,
			DataType = MpDataType.MpDataPunishTime,
			op_type = GetMpDataRsp.OpType.UpdateData,
			PunishEndTime = 0u
		}, CmdId.GetMpDataRsp));

		// 2026-10-21 玩法入口实验：官方协议 SwitchDataNotify(3700) 是功能开关推送，
		// 但 v1/v2 服务端从未下发过。这里登录时补发 ENDLESS 开关（EndlessType=Openworld 2），
		// 探测迪拉克之海入口（WorldMap.json EntryID 1012 / FeatureType 108 / Cover_11）能否被点亮。
		// 依据：官方 SeaRel 协议定义 + EndlessSwitchData{endless_type, player_group}。
		// 风险评估：仅新增一条登录推送，不触碰 GetWorldMapDataRsp blob；删除此块即可完全回退。
		var endlessBuf = new MemoryStream();
		Serializer.Serialize(endlessBuf, new EndlessSwitchData
		{
			EndlessType = 2u,   // Openworld = 迪拉克之海
			PlayerGroup = 1u
		});
		SwitchDataNotify switches = new SwitchDataNotify();
		switches.DataLists.Add(new SwitchData
		{
			Type = FeatureSwitchType.FeatureSwitchEndless,
			Data = endlessBuf.ToArray(),
			MinLevel = 0u,
			MaxLevel = 99u,
			BeginTime = 0u,
			EndTime = 4102444800u   // 2100-01-01，避开一切过期判定
		});
		session.Send(Packet.FromProto(switches, CmdId.SwitchDataNotify));
	}
}
