using System.Collections.Generic;
using Common;
using Common.Resources.Proto;
using ActCfg = Common.Utils.ExcelReader.ExtraStoryActExcel;
using ActReader = Common.Utils.ExcelReader.ExtraStoryAct;
using AreaCfg = Common.Utils.ExcelReader.ExtraStoryAreaExcel;
using AreaReader = Common.Utils.ExcelReader.ExtraStoryArea;

namespace PemukulPaku.GameServer.Handlers.Three;

/// <summary>
/// 233 / 234 —— 编年史「某个章节下的活动（act）列表」。
///
/// 点了章节卡片之后客户端会发这个包，原来没 handler
/// （日志 `GetExtraStoryActivityActReq not handled!`），卡片一直转圈。
///
/// 章节 → 区域（ExtraStoryArea.chapterId）→ act（ExtraStoryAct.areaId），
/// 时间窗和 GetExtraStoryData 保持一致。
/// </summary>
[PacketCmdId(CmdId.GetExtraStoryActivityActReq)]
internal class GetExtraStoryActivityActReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		GetExtraStoryActivityActReq req = packet.GetDecodedBody<GetExtraStoryActivityActReq>();

		GetExtraStoryActivityActRsp rsp = new GetExtraStoryActivityActRsp
		{
			retcode = GetExtraStoryActivityActRsp.Retcode.Succ
		};

		List<uint> areaIds = new List<uint>();
		AreaCfg[] areas = AreaReader.GetInstance().All;
		if (areas != null)
		{
			foreach (AreaCfg area in areas)
			{
				if ((uint)area.ChapterId == req.ChapterId)
				{
					areaIds.Add((uint)area.AreaId);
				}
			}
		}

		ActCfg[] acts = ActReader.GetInstance().All;
		if (acts != null && areaIds.Count > 0)
		{
			foreach (ActCfg act in acts)
			{
				if (!areaIds.Contains((uint)act.AreaId))
				{
					continue;
				}
				rsp.ActLists.Add(new ExtraStoryAct
				{
					ActId = (uint)act.ActId,
					BeginTime = GetExtraStoryDataReqHandler.BeginTime,
					EndTime = GetExtraStoryDataReqHandler.EndTime
				});
			}
		}

		Packet.c.Log($"[ExtraStory] ActivityAct chapter={req.ChapterId} areas={areaIds.Count} "
			+ $"acts={rsp.ActLists.Count}");
		session.Send(Packet.FromProto(rsp, CmdId.GetExtraStoryActivityActRsp));
	}
}
