using System;
using System.Linq;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Commands;

[CommandHandler("give", "<sel> [#] [id]", CommandType.Player, new string[] { "gold 123456789", "stigs 1", "weaps", "mats 999 2008", "valks", "outfits", "elves" })]
internal class GiveCommand : Command
{
	public override void Run(Session session, string[] args)
	{
		Run(session.Player, args);
		session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
		session.ProcessPacket(Packet.FromProto(new GetAvatarDataReq
		{
			AvatarIdLists = new uint[1]
		}, CmdId.GetAvatarDataReq));
	}

	public override void Run(Player player, string[] args)
	{
		string text = args[0];
		int? num = ((args.Length >= 2 && args[1] != null) ? new int?(int.Parse(args[1])) : null);
		string text2 = text;
		string text3 = text2;
		if (text3 != null)
		{
			AvatarDataExcel[] all;
			DressDataExcel[] all2;
			MaterialDataExcel[] all3;
			StigmataDataExcel[] all4;
			int materialId;
			int? num2;
			AvatarDataExcel[] all5;
			WeaponDataExcel[] all6;
			StigmataDataExcel[] all7;
			ElfDataExcel[] all8;
			WeaponDataExcel[] all9;
			switch (text3.Length)
			{
			case 7:
			{
				char c = text3[0];
				if (c != 'a')
				{
					if (c != 'o')
					{
						if (c != 'w' || !(text3 == "weapons"))
						{
							break;
						}
						goto IL_0535;
					}
					if (!(text3 == "outfits"))
					{
						break;
					}
					goto IL_08cd;
				}
				if (!(text3 == "avatars"))
				{
					break;
				}
				goto IL_03ad;
			}
			case 5:
				switch (text3[0])
				{
				case 'c':
					break;
				case 'v':
					goto IL_01fa;
				case 'e':
					goto IL_0210;
				case 'w':
					goto IL_0226;
				case 's':
					goto IL_023c;
				case 'd':
					goto IL_0252;
				default:
					goto end_IL_004a;
				}
				if (!(text3 == "chars"))
				{
					break;
				}
				goto IL_03ad;
			case 9:
				switch (text3[0])
				{
				case 'v':
					break;
				case 'w':
					goto IL_027e;
				case 's':
					goto IL_0294;
				case 'm':
					goto IL_02aa;
				default:
					goto end_IL_004a;
				}
				if (!(text3 == "valkyries"))
				{
					break;
				}
				goto IL_03ad;
			case 4:
			{
				char c = text3[0];
				if (c != 'e')
				{
					if (c != 'g')
					{
						if (c != 'm' || (!(text3 == "mats") && !(text3 == "matz")))
						{
							break;
						}
						goto IL_07ad;
					}
					if (!(text3 == "gold"))
					{
						break;
					}
					goto IL_0821;
				}
				if (!(text3 == "elfs"))
				{
					break;
				}
				goto IL_0439;
			}
			case 11:
			{
				char c = text3[0];
				if (c != 'm')
				{
					if (c != 'w' || !(text3 == "weapons-all"))
					{
						break;
					}
					goto IL_05df;
				}
				if (!(text3 == "material-id"))
				{
					break;
				}
				goto IL_0821;
			}
			case 10:
				if (!(text3 == "characters"))
				{
					break;
				}
				goto IL_03ad;
			case 3:
				if (!(text3 == "elf"))
				{
					break;
				}
				goto IL_0439;
			case 15:
				if (!(text3 == "avatars-scuffed"))
				{
					break;
				}
				goto IL_04a6;
			case 17:
				if (!(text3 == "valkyries-scuffed"))
				{
					break;
				}
				goto IL_04a6;
			case 8:
				if (!(text3 == "stigmata"))
				{
					break;
				}
				goto IL_0671;
			case 12:
				{
					if (!(text3 == "stigmata-all"))
					{
						break;
					}
					goto IL_071b;
				}
				IL_04a6:
				all = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All;
				foreach (AvatarDataExcel avatarDataExcel in all)
				{
					if (avatarDataExcel.AvatarId >= 9000 || avatarDataExcel.AvatarId == 316)
					{
						AvatarScheme element = Common.Database.Avatar.Create(avatarDataExcel.AvatarId, player.User.Uid, player.Equipment);
						player.AvatarList = player.AvatarList.Append(element).ToArray();
					}
				}
				goto IL_098f;
				IL_0226:
				if (!(text3 == "weaps"))
				{
					break;
				}
				goto IL_0535;
				IL_08cd:
				all2 = BaseExcelReader<DressData, DressDataExcel>.GetInstance().All;
				foreach (DressDataExcel dressDataExcel in all2)
				{
					int[] avatarIdList = dressDataExcel.AvatarIdList;
					foreach (int avatarId in avatarIdList)
					{
						AvatarScheme avatarScheme = player.AvatarList.ToList().Find((AvatarScheme av) => av.AvatarId == avatarId);
						if (avatarScheme != null)
						{
							avatarScheme.DressLists = avatarScheme.DressLists.Append((uint)dressDataExcel.DressId).ToArray();
							avatarScheme.Save();
						}
					}
				}
				goto IL_098f;
				IL_02aa:
				if (!(text3 == "materials"))
				{
					break;
				}
				goto IL_07ad;
				IL_07ad:
				all3 = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().All;
				foreach (MaterialDataExcel materialDataExcel in all3)
				{
					player.Equipment.AddMaterial(materialDataExcel.Id, (num.HasValue && num != 0) ? num.Value : materialDataExcel.QuantityLimit);
				}
				goto IL_098f;
				IL_0294:
				if (!(text3 == "stigs-all"))
				{
					break;
				}
				goto IL_071b;
				IL_027e:
				if (!(text3 == "weaps-all"))
				{
					break;
				}
				goto IL_05df;
				IL_071b:
				all4 = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().All;
				foreach (StigmataDataExcel stigmataDataExcel in all4)
				{
					Stigmata stigmata = player.Equipment.AddStigmata(stigmataDataExcel.Id);
					stigmata.Level = (uint)((num.HasValue && num <= 0) ? stigmataDataExcel.MaxLv : (num.HasValue ? num.Value : stigmataDataExcel.MaxLv));
				}
				goto IL_098f;
				IL_0821:
				materialId = ((args.Length >= 3 && args[2] != null) ? int.Parse(args[2]) : 100);
				num2 = BaseExcelReader<MaterialData, MaterialDataExcel>.GetInstance().All.FirstOrDefault((MaterialDataExcel m) => m.Id == materialId)?.QuantityLimit;
				if (num2.HasValue)
				{
					player.Equipment.AddMaterial(materialId, (num.HasValue && num != 0) ? num.Value : num2.Value);
				}
				goto IL_098f;
				IL_01fa:
				if (!(text3 == "valks"))
				{
					break;
				}
				goto IL_03ad;
				IL_03ad:
				all5 = BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All;
				foreach (AvatarDataExcel avatarDataExcel2 in all5)
				{
					if (avatarDataExcel2.AvatarId < 9000 && avatarDataExcel2.AvatarId != 316 && avatarDataExcel2.AvatarId != 2001)
					{
						AvatarScheme element2 = Common.Database.Avatar.Create(avatarDataExcel2.AvatarId, player.User.Uid, player.Equipment);
						player.AvatarList = player.AvatarList.Append(element2).ToArray();
					}
				}
				goto IL_098f;
				IL_05df:
				all6 = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().All;
				foreach (WeaponDataExcel weaponDataExcel in all6)
				{
                    if(!GachaEligibility.IsAllowedWeapon(weaponDataExcel.Id))continue;
					Weapon weapon = player.Equipment.AddWeapon(weaponDataExcel.Id);
					weapon.Level = (uint)((num.HasValue && num <= 0) ? weaponDataExcel.MaxLv : (num.HasValue ? num.Value : weaponDataExcel.MaxLv));
				}
				goto IL_098f;
				IL_023c:
				if (!(text3 == "stigs"))
				{
					break;
				}
				goto IL_0671;
				IL_0671:
				all7 = BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().All;
				foreach (StigmataDataExcel stigmataDataExcel2 in all7)
				{
					if (stigmataDataExcel2.EvoId == 0)
					{
						Stigmata stigmata2 = player.Equipment.AddStigmata(stigmataDataExcel2.Id);
						stigmata2.Level = (uint)((num.HasValue && num <= 0) ? stigmataDataExcel2.MaxLv : (num.HasValue ? num.Value : stigmataDataExcel2.MaxLv));
					}
				}
				goto IL_098f;
				IL_0252:
				if (!(text3 == "dress"))
				{
					break;
				}
				goto IL_08cd;
				IL_098f:
				player.User.Save();
				return;
				IL_0439:
				all8 = BaseExcelReader<ElfData, ElfDataExcel>.GetInstance().All;
				foreach (ElfDataExcel elfDataExcel in all8)
				{
					if (elfDataExcel.ElfId < 120)
					{
						ElfsScheme elfsScheme = Elfs.Create(player.User.Uid);
						elfsScheme.AddElf(elfDataExcel.ElfId);
						player.Elfs = elfsScheme;
					}
				}
				goto IL_098f;
				IL_0535:
				all9 = BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().All;
				foreach (WeaponDataExcel weaponDataExcel2 in all9)
				{
                    if(!GachaEligibility.IsAllowedWeapon(weaponDataExcel2.Id))continue;
					if (weaponDataExcel2.EvoId == 0)
					{
						Weapon weapon2 = player.Equipment.AddWeapon(weaponDataExcel2.Id);
						weapon2.Level = (uint)((num.HasValue && num <= 0) ? weaponDataExcel2.MaxLv : (num.HasValue ? num.Value : weaponDataExcel2.MaxLv));
					}
				}
				goto IL_098f;
				IL_0210:
				if (!(text3 == "elves"))
				{
					break;
				}
				goto IL_0439;
				end_IL_004a:
				break;
			}
		}
		throw new ArgumentException("Unrecognized action");
	}
}
