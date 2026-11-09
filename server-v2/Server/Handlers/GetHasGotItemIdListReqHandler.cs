using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 图鉴/收藏的「已获得物品」列表 —— 收藏界面的进度就吃这份数据。
///
/// 按需求「无需记录进度，全部满」：这里不再读玩家库存，而是直接把配置表里
/// 所有武器 / 圣痕 / 女武神（含角色卡）/ 服装的 ID 全量返回，
/// 客户端据此把每一格都画成已解锁、收藏进度 100%。
///
/// 想改回「按实际库存计算」的话，把 AllOwned = true 改成 false 即可，
/// 那时会退回 武器+圣痕+材料+女武神 的实际持有集合。
/// </summary>
[PacketCmdId(CmdId.GetHasGotItemIdListReq)]
internal class GetHasGotItemIdListReqHandler : IPacketHandler
{
	/// <summary>true = 收藏全部拉满（当前需求）；false = 按实际持有计算。</summary>
	private const bool AllOwned = true;

	/// <summary>
	/// 是否把武器/圣痕的整段 id 区间都发下去（补客户端表比服务端多出来的那 3%）。
	/// 可以用运行目录下的 collection_pad.txt 临时切换（内容 0 = 不补洞），不用重启服务端，
	/// 打开一次收藏界面就会重新读。排查「补洞的假 id 会不会打断影像栏」时用。
	/// </summary>
	private static bool PadGaps
	{
		get
		{
			try
			{
				string path = Path.Combine(AppContext.BaseDirectory, "collection_pad.txt");
				if (File.Exists(path))
				{
					string text = File.ReadAllText(path).Trim();
					return text != "0";
				}
			}
			catch
			{
				// 读不到就当开启
			}

			return true;
		}
	}

	private static void AddRange(HashSet<uint> ids, uint from, uint to)
	{
		for (uint id = from; id <= to; id++)
		{
			ids.Add(id);
		}
	}

	/// <summary>
	/// 7.0 客户端的 CgID 空间是 50001 起步的（6.6 的 40001-50000 那段在 7.0 已经整体后移）。
	///
	/// 实证：只发 6.6 配置里的 40001-50126 时，收藏界面的 CG 一直停在 6%，
	/// 6% ≈ 125 / 约 2000，正好等于「50001-50126 这 125 个 id 被客户端认出来」的比例；
	/// 40001-50000 那 1592 个 id 客户端一条都不认，发了也没用。
	///
	/// 所以这里直接把 50001 以后整段补下去，客户端拿自己的表去匹配。
	/// 区间写在运行目录 collection_cgpad.txt（格式 50001-53500），改完重新打开收藏即生效。
	/// </summary>
	private static string CgPadText()
	{
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, "collection_cgpad.txt");
			if (File.Exists(path))
			{
				return File.ReadAllText(path).Trim();
			}
		}
		catch
		{
			// 读不到就用默认区间
		}

		return "50001-60300";
	}

	private static void AddCgPad(HashSet<uint> ids, string text)
	{
		string[] parts = text.Split('-');
		if (parts.Length != 2)
		{
			return;
		}

		if (!uint.TryParse(parts[0].Trim(), out uint from) || !uint.TryParse(parts[1].Trim(), out uint to))
		{
			return;
		}

		if (to >= from && to - from <= 100000u)
		{
			AddRange(ids, from, to);
		}
	}

	private static uint[]? _paddedIds;
	private static uint[]? _plainIds;
	private static string _paddedKey = string.Empty;

	public void Handle(Session session, Packet packet)
	{
		bool pad = PadGaps;
		uint[] ids = CompetitiveMedalService.Collection(session.Player.User.Uid, AllOwned ? AllConfigIds(pad) : OwnedIds(session));

		GetHasGotItemIdListRsp getHasGotItemIdListRsp = new GetHasGotItemIdListRsp
		{
			retcode = GetHasGotItemIdListRsp.Retcode.Succ,
			ItemIdLists = ids
		};

		Packet.c.Log(string.Format("[Collection] uid={0} has_got_items={1} mode={2} pad={3} 约{4}KB",
			session.Player.User.Uid, ids.Length, AllOwned ? "all" : "owned", pad ? 1 : 0, EstimatedBytes(ids) / 1024));
		session.Send(Packet.FromProto(getHasGotItemIdListRsp, CmdId.GetHasGotItemIdListRsp));

		// 比 PlayerLoginReq 晚、比玩家能点进家园早，是命中率最高的一枪。
	}

	/// <summary>配置表全量 ID（补洞 / 不补洞 各缓存一份）。</summary>
	private static uint[] AllConfigIds(bool pad)
	{
		string cgPad = pad ? CgPadText() : string.Empty;
		string key = pad + "|" + cgPad;

		if (pad && _paddedIds != null && _paddedKey == key)
		{
			return _paddedIds;
		}

		if (!pad && _plainIds != null)
		{
			return _plainIds;
		}

		// 保持顺序非常重要：客户端处理这份列表时是按顺序吃的，
		// 影像（CG）这一栏之前无论怎么加都还是 0%，先把 CgID 放在最前面再试一次。
		List<uint> ordered = new List<uint>(24576);
		HashSet<uint> seen = new HashSet<uint>(24576);

		void Push(IEnumerable<uint> source)
		{
			foreach (uint id in source)
			{
				if (id > 0 && seen.Add(id))
				{
					ordered.Add(id);
				}
			}
		}

		// ── 影像（CG）：优先 ────────────────────────────────────────────────
		// 只发 50001 之后的：6.6 的 40001-50000 这段在 7.0 客户端里没有对应 CgID，
		// 发了纯占包体（实测这 1592 个 id 一个都没被认出来）。
		Push(BaseExcelReader<CgData, CgDataExcel>.GetInstance().AllIds().Where(id => id >= 50001u));

		// 7.0 客户端的 CgID 空间整段补洞（默认 50001-53500，见 CgPadText）。
		HashSet<uint> cgPadIds = new HashSet<uint>();
		if (pad)
		{
			AddCgPad(cgPadIds, cgPad);
		}

		Push(cgPadIds);

		Push(BaseExcelReader<WeaponData, WeaponDataExcel>.GetInstance().All
			.Select((WeaponDataExcel weapon) => (uint)weapon.Id));

		Push(BaseExcelReader<StigmataData, StigmataDataExcel>.GetInstance().All
			.Select((StigmataDataExcel stigmata) => (uint)stigmata.Id));

		foreach (AvatarDataExcel avatar in BaseExcelReader<AvatarData, AvatarDataExcel>.GetInstance().All)
		{
			// 女武神本体用 avatarID，图鉴里还有一张角色卡 avatarCardID，两个都要。
			if (avatar.AvatarId > 0)
			{
				Push(new uint[1] { (uint)avatar.AvatarId });
			}

			if (avatar.AvatarCardId > 0)
			{
				Push(new uint[1] { (uint)avatar.AvatarCardId });
			}
		}

		Push(BaseExcelReader<DressData, DressDataExcel>.GetInstance().All
			.Select((DressDataExcel dress) => (uint)dress.DressId));

		// ── 收藏的其它几栏 ──────────────────────────────────────────────────
		// 勋章这一栏吃的是这份列表（实测：加上 medalID 后 0% → 96%）。
		Push(BaseExcelReader<MedalData, MedalDataExcel>.GetInstance().AllIds());
		Push(BaseExcelReader<MonsterWikiData, MonsterWikiDataExcel>.GetInstance().AllIds());

		// ── 补洞 ────────────────────────────────────────────────────────────
		// 实测：把配置表里全部 id 发下去，武器/圣痕收藏仍然停在 97%。
		// 差别不在我们少发了配置里的 id，而在「客户端内置表比服务端这份 7.0 导出多约 3%」——
		// 那些 id 服务端配置里根本没有，只能靠区间补洞把整段 id 空间都发下去，
		// 客户端拿它自己的表去匹配，缺的那部分自然也能命中。
		// 代价：多出约 1.5 万个 id，包体约 45KB（客户端上限 64KB，仍在范围内）。
		// 如果哪天客户端因为包太大丢包，把 PadGaps 改成 false 就回到只发配置 id。
		if (pad)
		{
			HashSet<uint> gaps = new HashSet<uint>();
			AddRange(gaps, 20001u, 29900u);   // 武器
			AddRange(gaps, 30001u, 35500u);   // 圣痕
			AddRange(gaps, 101001u, 104010u); // 勋章（配置只到 104003，客户端表略多，整段补满）
			Push(gaps);
		}

		uint[] result = ordered.ToArray();
		if (pad)
		{
			_paddedIds = result;
			_paddedKey = key;
		}
		else
		{
			_plainIds = result;
		}

		return result;
	}

	/// <summary>粗略估算 protobuf 编码后的大小（varint 3 字节居多），用于盯着 64KB 上限。</summary>
	private static int EstimatedBytes(uint[] ids)
	{
		int total = 0;
		foreach (uint id in ids)
		{
			total += id < 0x80u ? 2 : (id < 0x4000u ? 3 : 4);
		}

		return total;
	}

	/// <summary>实际持有的物品 ID。</summary>
	private static uint[] OwnedIds(Session session)
	{
		HashSet<uint> ids = new HashSet<uint>();

		foreach (Weapon weapon in session.Player.Equipment.WeaponList ?? new Weapon[0])
		{
			ids.Add(weapon.Id);
		}

		foreach (Stigmata stigmata in session.Player.Equipment.StigmataList ?? new Stigmata[0])
		{
			ids.Add(stigmata.Id);
		}

		foreach (Common.Database.AvatarScheme avatar in session.Player.AvatarList ?? new Common.Database.AvatarScheme[0])
		{
			ids.Add(avatar.AvatarId);
		}

		return ids.ToArray();
		AdventureService.Push(session);

	}
}