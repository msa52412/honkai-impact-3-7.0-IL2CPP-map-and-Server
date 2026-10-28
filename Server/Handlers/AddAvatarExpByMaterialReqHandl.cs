using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.AddAvatarExpByMaterialReq)]
internal class AddAvatarExpByMaterialReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		AddAvatarExpByMaterialReq Data = packet.GetDecodedBody<AddAvatarExpByMaterialReq>();
		MaterialDataExcel materialData = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().FromId(Data.MaterialId);
		AvatarScheme avatarScheme = session.Player.AvatarList.FirstOrDefault((AvatarScheme avatar) => avatar.AvatarId == Data.AvatarId);
		AddAvatarExpByMaterialRsp addAvatarExpByMaterialRsp = new AddAvatarExpByMaterialRsp
		{
			retcode = AddAvatarExpByMaterialRsp.Retcode.Succ
		};
		if (avatarScheme != null && materialData != null)
		{
			session.Player.Equipment.AddMaterial(materialData.Id, (int)(0 - Data.MaterialNum));
			GetEquipmentDataRsp getEquipmentDataRsp = new GetEquipmentDataRsp
			{
				retcode = GetEquipmentDataRsp.Retcode.Succ,
				VitalityValue = 0u,
				IsAll = true
			};
			getEquipmentDataRsp.MaterialLists.Add(session.Player.Equipment.MaterialList.First((Common.Resources.Proto.Material mat) => mat.Id == materialData.Id));
			avatarScheme.AddExp((uint)materialData.CharacterExpProvide * Data.MaterialNum);
			session.Send(Packet.FromProto(getEquipmentDataRsp, CmdId.GetEquipmentDataRsp));
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { avatarScheme.AvatarId }
			}, CmdId.GetAvatarDataReq));
		}
		else
		{
			addAvatarExpByMaterialRsp.retcode = AddAvatarExpByMaterialRsp.Retcode.AvatarNotExist;
		}
		session.Send(Packet.FromProto(addAvatarExpByMaterialRsp, CmdId.AddAvatarExpByMaterialRsp));
	}
}
