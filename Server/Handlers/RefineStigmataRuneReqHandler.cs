using System;
using System.Linq;
using Common.Resources.Proto;

namespace PemukulPaku.GameServer.Handlers;

[PacketCmdId(CmdId.RefineStigmataRuneReq)]
internal class RefineStigmataRuneReqHandler : IPacketHandler
{
	private static readonly uint[] AffixIds = new uint[30]
	{
		30010u, 30020u, 30030u, 30040u, 30050u, 30060u, 30070u, 30080u, 30090u, 30100u,
		30110u, 30120u, 30130u, 30140u, 30150u, 30160u, 30170u, 30180u, 30190u, 30200u,
		30210u, 30220u, 40010u, 40020u, 40030u, 40040u, 50010u, 50020u, 50030u, 50040u
	};

	public void Handle(Session session, Packet packet)
	{
		RefineStigmataRuneReq decodedBody = packet.GetDecodedBody<RefineStigmataRuneReq>();
		RefineStigmataRuneRsp refineStigmataRuneRsp = new RefineStigmataRuneRsp
		{
			retcode = RefineStigmataRuneRsp.Retcode.Succ,
			TimesType = StigmataRefineTimesType.StigmataRefineTimesOne
		};
		uint uid = decodedBody.UniqueId;
		Stigmata stigmata = session.Player.Equipment.StigmataList.FirstOrDefault((Stigmata stig) => stig.UniqueId == uid);
		if (stigmata == null)
		{
			return;
		}
		StigmataRune item = GenerateRune();
		StigmataRune item2 = GenerateRune();
		StigmataRuneGroup stigmataRuneGroup = new StigmataRuneGroup
		{
			UniqueId = uid
		};
		switch (decodedBody.Type)
		{
		case StigmataRefineType.StigmataRefineNormal:
			if (decodedBody.IsRetry)
			{
				stigmata.WaitSelectRuneGroupLists.Clear();
			}
			if (stigmata.SlotNum < 2 && new Random().Next(0, 10) == 9)
			{
				stigmata.SlotNum++;
			}
			if (stigmata.SlotNum >= 1)
			{
				stigmataRuneGroup.RuneLists.Add(item);
			}
			if (stigmata.SlotNum == 2)
			{
				stigmataRuneGroup.RuneLists.Add(item2);
			}
			stigmata.WaitSelectRuneGroupLists.Add(stigmataRuneGroup);
			refineStigmataRuneRsp.RuneGroupLists.Add(stigmataRuneGroup);
			break;
		case StigmataRefineType.StigmataRefineAddSlot:
			if (stigmata.SlotNum >= 2)
			{
				throw new Exception("Cannot have more than 2 slots");
			}
			if (stigmata.SlotNum == 1)
			{
				stigmata.SlotNum++;
				stigmataRuneGroup.RuneLists.Add(stigmata.RuneLists.FirstOrDefault());
			}
			else
			{
				stigmata.SlotNum++;
			}
			stigmataRuneGroup.RuneLists.Add(item);
			stigmata.WaitSelectRuneGroupLists.Add(stigmataRuneGroup);
			refineStigmataRuneRsp.RuneGroupLists.Add(stigmataRuneGroup);
			break;
		case StigmataRefineType.StigmataRefineSpecial:
			item = GenerateRuneSpecial(1u, decodedBody.SpecialId);
			item2 = GenerateRuneSpecial(2u, decodedBody.SpecialId);
			stigmataRuneGroup.RuneLists.Add(item);
			stigmataRuneGroup.RuneLists.Add(item2);
			stigmata.WaitSelectRuneGroupLists.Add(stigmataRuneGroup);
			refineStigmataRuneRsp.RuneGroupLists.Add(stigmataRuneGroup);
			break;
		case StigmataRefineType.StigmataRefineLock:
			if (decodedBody.IsRetry)
			{
			}
			throw new NotImplementedException("RefineStigmataRuneReqHandler StigmataRefineType.StigmataRefineLock");
		}
		session.Player.Equipment.Save();
		session.ProcessPacket(Packet.FromProto(new GetEquipmentDataReq(), CmdId.GetEquipmentDataReq));
		session.Send(Packet.FromProto(refineStigmataRuneRsp, CmdId.RefineStigmataRuneRsp));
	}

	private StigmataRune GenerateRune(int min = 1, int max = 4)
	{
		return new StigmataRune
		{
			RuneId = GenerateAffixId(min, max),
			StrengthPercent = GenerateAffixStrength()
		};
	}

	private StigmataRune GenerateRuneSpecial(uint slot, uint id = 1u)
	{
		return new StigmataRune
		{
			RuneId = GenerateAffixIdSpecial(id, slot),
			StrengthPercent = GenerateAffixStrengthSpecial(id, slot)
		};
	}

	private uint GenerateAffixId(int min, int max)
	{
		return AffixIds[new Random().Next(0, AffixIds.Length - 1)] + (uint)new Random().Next((min >= max || min <= 0) ? 1 : min, (max > min && max < 5) ? max : 4);
	}

	private uint GenerateAffixStrength(int min = 1, int max = 101)
	{
		return (uint)new Random().Next((min <= 0 || min >= max) ? 1 : min, (max > min && max < 102) ? max : 101);
	}

	private uint GenerateAffixIdSpecial(uint id, uint slot)
	{
		switch (id)
		{
		case 14u:
			return 30013u;
		case 4u:
		case 8u:
			return 30063u;
		case 5u:
		case 9u:
			return 30073u;
		case 6u:
		case 10u:
			return 30083u;
		case 7u:
		case 11u:
			return 30193u;
		case 12u:
		case 13u:
			return 30213u;
		case 15u:
			return (slot == 2) ? 50043u : 50033u;
		default:
			return 50043u;
		}
	}

	private uint GenerateAffixStrengthSpecial(uint id, uint slot)
	{
		switch (id)
		{
		case 3u:
		case 14u:
			return GenerateAffixStrength();
		case 8u:
		case 9u:
		case 10u:
		case 11u:
		case 13u:
			return GenerateAffixStrength(7, 66);
		case 15u:
			return (slot == 2) ? 59u : 45u;
		default:
			return 100u;
		}
	}
}
