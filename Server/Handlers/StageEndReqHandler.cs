using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using ProtoBuf;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.StageEndReq)]
internal class StageEndReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		StageEndReq decodedBody = packet.GetDecodedBody<StageEndReq>();
		StageEndReqBody stageEndReqBody = Serializer.Deserialize<StageEndReqBody>(decodedBody.Body.AsSpan());
		StageDataExcel stageDataExcel = BaseExcelReader<StageData, StageDataExcel>.GetInstance().FromId((int)stageEndReqBody.StageId);
		if (stageDataExcel == null)
		{
			session.c.Error("StageData Excel Is Bad Please Fix");
			session.Send(Packet.FromProto(new StageEndRsp
			{
				retcode = StageEndRsp.Retcode.StageError
			}, CmdId.StageEndRsp));
			return;
		}
		if (stageDataExcel.LevelId == 10101)
		{
			session.Player.User.IsFirstLogin = false;
		}
		StageEndRsp stageEndRsp = new StageEndRsp
		{
			retcode = StageEndRsp.Retcode.Succ,
			StageId = stageEndReqBody.StageId,
			EndStatus = stageEndReqBody.EndStatus
		};
		if (stageEndReqBody.EndStatus == StageEndStatus.StageWin)
		{
			EquipmentScheme equipment = session.Player.Equipment;
			foreach (DropItem dropItemList in stageEndReqBody.DropItemLists)
			{
				equipment.AddMaterial((int)dropItemList.ItemId, (int)dropItemList.Num);
			}
			if (stageEndReqBody.ChallengeIndexLists != null)
			{
				session.Player.User.Hcoin += stageEndReqBody.ChallengeIndexLists.Length * 5;
				stageEndRsp.ChallengeLists.AddRange(stageEndReqBody.ChallengeIndexLists.Select((uint challengeIndex) => new StageChallengeData
				{
					ChallengeIndex = challengeIndex,
					Reward = new Common.Resources.Proto.RewardData
					{
						Hcoin = 5u
					}
				}));
			}
			session.Player.User.AddExp(100);
			session.ProcessPacket(Packet.FromProto(new GetMainDataReq(), CmdId.GetMainDataReq));
			session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
			session.ProcessPacket(Packet.FromProto(new GetWorldMapDataReq(), CmdId.GetWorldMapDataReq));
			session.ProcessPacket(Packet.FromProto(new GetStageDataReq(), CmdId.GetStageDataReq));
			stageEndRsp.PlayerExpReward = 100u;
			stageEndRsp.AvatarExpReward = stageEndReqBody.AvatarExpReward;
			stageEndRsp.ScoinReward = stageEndReqBody.ScoinReward;
		}
		session.Send(Packet.FromProto(stageEndRsp, CmdId.StageEndRsp));
	}
}
