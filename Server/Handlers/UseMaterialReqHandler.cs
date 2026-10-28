using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.UseMaterialReq)]
internal class UseMaterialReqHandler : IPacketHandler
{
	private struct ParameterData
	{
		public int RewardId { get; set; }

		public int Num { get; set; }

		public ParameterData(int rewardId, int num)
		{
			Num = num;
			RewardId = rewardId;
		}
	}

	public void Handle(Session session, Packet packet)
	{
		UseMaterialReq decodedBody = packet.GetDecodedBody<UseMaterialReq>();
		MaterialDataExcel materialDataExcel = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(decodedBody.MaterialId);
		MaterialUseDataExcel materialUseDataExcel = BaseExcelReader<MaterialUseData, MaterialUseDataExcel>.GetInstance().FromId(materialDataExcel?.UseId ?? 0);
		UseMaterialRsp useMaterialRsp = new UseMaterialRsp
		{
			retcode = UseMaterialRsp.Retcode.ConsumeItemNotExist
		};
		Common.Resources.Proto.Material item = session.Player.Equipment.AddMaterial((int)decodedBody.MaterialId, (int)(0 - decodedBody.Num));
		GetEquipmentDataRsp getEquipmentDataRsp = new GetEquipmentDataRsp
		{
			retcode = GetEquipmentDataRsp.Retcode.Succ,
			VitalityValue = 5900u
		};
		getEquipmentDataRsp.MaterialLists.Add(item);
		if (materialUseDataExcel != null)
		{
			ParameterData parameterData = GetParameterData(materialUseDataExcel, (int)decodedBody.Parameter);
			RewardDataExcel rewardDataExcel = BaseExcelReader<Common.Utils.ExcelReader.RewardData, RewardDataExcel>.GetInstance().FromId(parameterData.RewardId);
			useMaterialRsp.retcode = UseMaterialRsp.Retcode.Succ;
			Common.Resources.Proto.RewardData rewardData = new Common.Resources.Proto.RewardData();
			int useType = materialUseDataExcel.UseType;
			int num = useType;
			if (num == 25)
			{
				AvatarDataExcel avatarData = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All.FirstOrDefault((AvatarDataExcel x) => x.AvatarFragmentId == parameterData.RewardId);
				if (avatarData != null)
				{
					rewardData.ItemLists.Add(new RewardItemData
					{
						Id = (uint)parameterData.RewardId,
						Num = (uint)parameterData.Num * decodedBody.Num
					});
					session.Player.AvatarList.FirstOrDefault((AvatarScheme avatar) => avatar.AvatarId == avatarData.AvatarId)?.AddFragment((uint)parameterData.Num * decodedBody.Num);
					session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
					{
						AvatarIdLists = new uint[1] { (uint)avatarData.AvatarId }
					}, CmdId.GetAvatarDataReq));
				}
			}
			else
			{
				useMaterialRsp.retcode = UseMaterialRsp.Retcode.FeatureClosed;
			}
			useMaterialRsp.GiftRewardLists.Add(rewardData);
		}
		session.Send(Packet.FromProto(getEquipmentDataRsp, CmdId.GetEquipmentDataRsp), Packet.FromProto(useMaterialRsp, CmdId.UseMaterialRsp));
	}

	private static ParameterData GetParameterData(MaterialUseDataExcel useData, int clientPara)
	{
		int num = 1;
		if (clientPara == 0)
		{
			if (useData.ParaStr[0].Contains(":"))
			{
				num = int.Parse(useData.ParaStr[0].Split(":")[1]);
				clientPara = int.Parse(useData.ParaStr[0].Split(":")[0]);
			}
			else
			{
				clientPara = int.Parse(useData.ParaStr[0]);
			}
		}
		return new ParameterData(clientPara, num);
	}
}
