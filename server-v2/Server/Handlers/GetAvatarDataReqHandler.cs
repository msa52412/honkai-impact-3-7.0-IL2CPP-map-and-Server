using PemukulPaku.GameServer.Game;
using System.Collections.Generic;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.GetAvatarDataReq)]
internal class GetAvatarDataReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetAvatarDataReq Packet = packet.GetDecodedBody<GetAvatarDataReq>();
        if(Packet.AvatarIdLists==null || Packet.AvatarIdLists.Length==0)Packet.AvatarIdLists=new uint[]{0};
		GetAvatarDataRsp getAvatarDataRsp = new GetAvatarDataRsp
		{
			retcode = GetAvatarDataRsp.Retcode.Succ
		};
		if (Packet.AvatarIdLists.Contains(0u))
		{
			IEnumerable<Common.Resources.Proto.Avatar> collection = session.Player.AvatarList.Select(delegate(AvatarScheme avatar)
			{
				Common.Resources.Proto.Avatar avatar3 = new Common.Resources.Proto.Avatar
				{
					AvatarId = avatar.AvatarId,
					AvatarArtifact = avatar.AvatarArtifact,
					DressId = avatar.DressId,
					DressLists = avatar.DressLists,
					Level = avatar.Level,
					Exp = avatar.Exp,
					Fragment = avatar.Fragment,
					Mode = avatar.Mode,
					StageGoodfeel = avatar.StageGoodfeel,
					Star = avatar.Star,
					StigmataUniqueId1 = avatar.StigmataUniqueId1,
					StigmataUniqueId2 = avatar.StigmataUniqueId2,
					StigmataUniqueId3 = avatar.StigmataUniqueId3,
					SubStar = avatar.SubStar,
					TodayHasAddGoodfeel = avatar.TodayHasAddGoodfeel,
					TouchGoodfeel = avatar.TouchGoodfeel,
					WeaponUniqueId = avatar.WeaponUniqueId
				};
				avatar3.SkillLists.AddRange((avatar.SkillLists??new List<AvatarScheme.AvatarSkill>()).Select(delegate(AvatarScheme.AvatarSkill skill)
				{
					AvatarSkill avatarSkill2 = new AvatarSkill
					{
						SkillId = skill.SkillId
					};
					avatarSkill2.SubSkillLists.AddRange(skill.SubSkillLists??new List<AvatarSubSkill>());
					return avatarSkill2;
				}));
				return avatar3;
			});
			getAvatarDataRsp.IsAll = true;
			getAvatarDataRsp.AvatarLists.AddRange(collection);
		}
		else
		{
			IEnumerable<Common.Resources.Proto.Avatar> collection2 = session.Player.AvatarList.Where((AvatarScheme avatar) => Packet.AvatarIdLists.Contains(avatar.AvatarId)).Select(delegate(AvatarScheme avatar)
			{
				Common.Resources.Proto.Avatar avatar2 = new Common.Resources.Proto.Avatar
				{
					AvatarId = avatar.AvatarId,
					AvatarArtifact = avatar.AvatarArtifact,
					DressId = avatar.DressId,
					DressLists = avatar.DressLists,
					Level = avatar.Level,
					Exp = avatar.Exp,
					Fragment = avatar.Fragment,
					Mode = avatar.Mode,
					StageGoodfeel = avatar.StageGoodfeel,
					Star = avatar.Star,
					StigmataUniqueId1 = avatar.StigmataUniqueId1,
					StigmataUniqueId2 = avatar.StigmataUniqueId2,
					StigmataUniqueId3 = avatar.StigmataUniqueId3,
					SubStar = avatar.SubStar,
					TodayHasAddGoodfeel = avatar.TodayHasAddGoodfeel,
					TouchGoodfeel = avatar.TouchGoodfeel,
					WeaponUniqueId = avatar.WeaponUniqueId
				};
				avatar2.SkillLists.AddRange((avatar.SkillLists??new List<AvatarScheme.AvatarSkill>()).Select(delegate(AvatarScheme.AvatarSkill skill)
				{
					AvatarSkill avatarSkill = new AvatarSkill
					{
						SkillId = skill.SkillId
					};
					avatarSkill.SubSkillLists.AddRange(skill.SubSkillLists??new List<AvatarSubSkill>());
					return avatarSkill;
				}));
				return avatar2;
			});
			getAvatarDataRsp.AvatarLists.AddRange(collection2);
			getAvatarDataRsp.IsAll = false;
		}
		session.Send(PemukulPaku.GameServer.Packet.FromProto(getAvatarDataRsp, CmdId.GetAvatarDataRsp));
		AdventureService.Push(session);

	}
}
