using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers;

/// <summary>
/// 影像（CG）真正解锁的那条链路。
///
/// 结论来自反编译 + 逐条指令核对（MoleMole.MonoCgModule，token ODNDNLLOJBP）：
///   1. 收藏界面里每个 CG 格子的锁定态 = CgDataItem.FNKCPONDMHO()
///      （0x1816C7F80）。UnlockType=Level 时它做的是
///      `return !MonoCgModule.IENAKGAAPCK(cgId)`，
///      而 IENAKGAAPCK 就是 `HashSet&lt;int&gt;(模块+0x48).Contains(cgId)`——「这个 CG 解锁了没」。
///   2. 这个集合只有一个服务端入口：`IOJLPHAEDLN(GetFinishGuideDataRsp)`
///      （0x181400E30）。它遍历 rsp.guide_id_list，对每个 id 调 HADMDPGEICA(id)，
///      而 HADMDPGEICA（0x1814007F0）就是 `if (id &gt; 门槛) { set.Add(id); FireNotify(0x2D8); }`
///      —— 所以 guide_id_list 里放的其实是被当成「已解锁 CG」的 CgID。
///   3. 其余填充点全是客户端本地播片回调
///      （PlayVideo / OnCgFinishedCallback / MarkCGIDFinished …），服务端插不上手。
///
/// 而客户端启动时（以及进收藏时）会发 GetFinishGuideDataReq，我们之前没有 handler，
/// 日志里全是 “GetFinishGuideDataReq not handled!”，于是这个集合永远是空的 ——
/// 这就是 CG 一栏「0%/全锁、提示通关xx解锁」的真正原因。
///
/// 7.0 客户端的 CgID 空间是 50001 起步的（6.6 的 40001-50000 那段在 7.0 已整体后移，
/// 实测只发旧 id 时收藏 CG 卡在 6%），所以这里直接按区间整段回。
/// 区间写在运行目录 guide_cgpad.txt（默认 40001-61000），改完重新登录即生效。
/// </summary>
[PacketCmdId(CmdId.GetFinishGuideDataReq)]
internal class GetFinishGuideDataReqHandler : IPacketHandler
{
	private static string _key = string.Empty;
	private static uint[] _ids = new uint[0];

	public void Handle(Session session, Packet packet)
	{
		uint[] ids = Ids();

		Packet.c.Log(string.Format("[Collection] FinishGuide(影像解锁) uid={0} 回 {1} 个 id 约{2}KB",
			session.Player.User.Uid, ids.Length, EstimatedBytes(ids) / 1024));

		session.Send(Packet.FromProto(new GetFinishGuideDataRsp
		{
			retcode = GetFinishGuideDataRsp.Retcode.Succ,
			GuideIdLists = ids
		}, CmdId.GetFinishGuideDataRsp));
	}

	private static uint[] Ids()
	{
		string text = Config("40001-61000");
		if (_key == text && _ids.Length > 0)
		{
			return _ids;
		}

		string[] parts = text.Split('-');
		if (parts.Length != 2
			|| !uint.TryParse(parts[0].Trim(), out uint from)
			|| !uint.TryParse(parts[1].Trim(), out uint to)
			|| to < from
			|| to - from > 100000u)
		{
			return new uint[0];
		}

		List<uint> list = new List<uint>((int)(to - from) + 1);
		for (uint id = from; id <= to; id++)
		{
			list.Add(id);
		}

		// 服务端配置里如果有落在区间外的 CgID（例如 80001），也一起带上。
		foreach (uint id in BaseExcelReader<CgData, CgDataExcel>.GetInstance().AllIds())
		{
			if (id < from || id > to)
			{
				list.Add(id);
			}
		}

		_key = text;
		_ids = list.ToArray();
		return _ids;
	}

	private static string Config(string fallback)
	{
		try
		{
			string path = Path.Combine(AppContext.BaseDirectory, "guide_cgpad.txt");
			if (File.Exists(path))
			{
				return File.ReadAllText(path).Trim();
			}
		}
		catch
		{
			// 读不到就用默认区间
		}

		return fallback;
	}

	private static int EstimatedBytes(uint[] ids)
	{
		int total = 0;
		foreach (uint id in ids)
		{
			total += id < 0x80u ? 2 : (id < 0x4000u ? 3 : 4);
		}

		return total;
	}
}
