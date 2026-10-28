using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("avatar", "<sel> <id> [Prop] [#]", CommandType.Player, new string[] { "add 406", "modify -1 star 5" })]
internal class AvatarCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		string text = args[0];
		int avatarId = int.Parse(args[1]);
		Run(session.Player, args);
		session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
		if (avatarId == -1)
		{
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1]
			}, CmdId.GetAvatarDataReq));
		}
		else
		{
			session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
			{
				AvatarIdLists = new uint[1] { (uint)avatarId }
			}, CmdId.GetAvatarDataReq));
		}
		if (!(text.ToLower() == "modify") && !(text.ToLower() == "mod"))
		{
			return;
		}
		List<uint> list = new List<uint>();
		if (avatarId == -1)
		{
			AvatarScheme[] avatarList = session.Player.AvatarList;
			foreach (AvatarScheme avatarScheme in avatarList)
			{
				list.Add(avatarScheme.AvatarId);
			}
		}
		else
		{
			AvatarScheme avatarScheme2 = session.Player.AvatarList.FirstOrDefault((AvatarScheme av) => av.AvatarId == avatarId);
			if (avatarScheme2 != null)
			{
				list.Add(avatarScheme2.AvatarId);
			}
		}
		session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
		{
			AvatarIdLists = list.ToArray()
		}, CmdId.GetAvatarDataReq));
	}

	public override void Run(Player player, string[] args)
	{
		string text = args[0];
		int avatarId = int.Parse(args[1]);
		string str = "";
		int num = 0;
		if (args.Length > 3)
		{
			str = args[2];
			num = int.Parse(args[3]);
		}
		AvatarScheme avatarScheme = null;
		TextInfo textInfo = new CultureInfo("en-US", useUserOverride: false).TextInfo;
		switch (text.ToLower())
		{
		case "add":
			if (avatarId == -1)
			{
				AvatarDataExcel[] all = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All;
				foreach (AvatarDataExcel avatarDataExcel in all)
				{
					if (avatarDataExcel.AvatarId < 9000 && avatarDataExcel.AvatarId != 316)
					{
						avatarScheme = Common.Database.Avatar.Create(avatarDataExcel.AvatarId, player.User.Uid, player.Equipment);
						player.AvatarList = player.AvatarList.Append(avatarScheme).ToArray();
					}
				}
			}
			else
			{
				avatarScheme = Common.Database.Avatar.Create(avatarId, player.User.Uid, player.Equipment);
				player.AvatarList = player.AvatarList.Append(avatarScheme).ToArray();
			}
			player.Equipment.Save();
			break;
		case "modify":
		case "mod":
			if (avatarId == -1)
			{
				AvatarScheme[] avatarList = player.AvatarList;
				foreach (AvatarScheme avatarScheme2 in avatarList)
				{
					avatarScheme2.GetType()?.GetProperty(textInfo.ToTitleCase(str))?.SetValue(avatarScheme2, (uint)num, null);
					avatarScheme2.Save();
				}
				break;
			}
			avatarScheme = player.AvatarList.FirstOrDefault((AvatarScheme av) => av.AvatarId == avatarId);
			if (avatarScheme != null)
			{
				avatarScheme.GetType()?.GetProperty(textInfo.ToTitleCase(str))?.SetValue(avatarScheme, (uint)num, null);
				avatarScheme.Save();
				break;
			}
			throw new ArgumentException("Avatar not found");
		default:
			throw new ArgumentException("Unrecognized action");
		}
	}
}
