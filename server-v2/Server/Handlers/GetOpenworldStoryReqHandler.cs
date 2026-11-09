using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Database;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;

namespace PemukulPaku.GameServer.Handlers.Openworld;

[PacketCmdId(CmdId.GetOpenworldStoryReq)]
internal class GetOpenworldStoryReqHandler : IPacketHandler
{
	public void Handle(Session session, Packet packet)
	{
		// is_all = true 时客户端走 CreateOrUpDateAllStories 的「全量重建」分支：
		//   cur_story_list 里每条       -> new KFOBMMKEFFA(story, 1)    // 1 = 进行中
		//   finish_story_id_list 里每条 -> new KFOBMMKEFFA(storyId, 2)  // 2 = 已完成
		// 两者都塞进 mapDataDict；IsStoryFinish(storyID) 就是取 allStoryDict 里那条的
		// +0xA4 状态字段判断 == 2，找不到直接返回 false。
		//
		// 原来这里 finish 只发存档里 IsDone 的（存档是空列表），cur 发全部未完成的，
		// 于是所有剧情在客户端都停留在「进行中」——
		// 而 OpenWorldMap.UnlockStoryId 的门禁（天命 map 2 要 308 = 樱色轮回 3-8）
		// 就是靠 IsStoryFinish 判定，必然失败，地图永远显示未解锁。
		//
		// 现在按调参（run\openworld.json 的 all_story_done，缺省 true）把整张
		// OpenWorldStoryData（1337 条）当作已完成全量下发。
		GetOpenworldStoryRsp rsp = new GetOpenworldStoryRsp
		{
			retcode = GetOpenworldStoryRsp.Retcode.Succ,
			IsAll = true
		};

		if (OwMapTuning.AllStoryDone)
		{
			rsp.FinishStoryIdLists = BaseExcelReader<OpenWorldStoryData, OpenWorldStoryDataExcel>
				.GetInstance().All
				.Select((OpenWorldStoryDataExcel x) => (uint)x.StoryId)
				.Where((uint id) => id != 0u)
				.Distinct()
				.ToArray();

			// 存档同步补齐，避免其它 handler（接取 / 领奖 / 进度上报）读存档时又变成「没做过」。
			uint now = (uint)Global.GetUnixInSeconds();
			HashSet<uint> done = new HashSet<uint>(session.Player.User.OpenWorldStory
				.Where((UserScheme.OpenWorldStoryScheme x) => x.IsDone)
				.Select((UserScheme.OpenWorldStoryScheme x) => x.StoryId));
			bool dirty = false;
			foreach (uint id in rsp.FinishStoryIdLists)
			{
				if (done.Contains(id))
				{
					continue;
				}
				session.Player.User.OpenWorldStory.Add(new UserScheme.OpenWorldStoryScheme
				{
					StoryId = id,
					StoryProgress = 0u,
					AcceptTime = now,
					IsDone = true
				});
				dirty = true;
			}
			if (dirty)
			{
				session.Player.User.Save();
			}

			Packet.c.Log($"[OW] GetOpenworldStory -> all done, finish={rsp.FinishStoryIdLists.Length}");
		}
		else
		{
			rsp.FinishStoryIdLists = (from x in session.Player.User.OpenWorldStory
				where x.IsDone
				select x.StoryId).ToArray();
			rsp.CurStoryLists.AddRange(from x in session.Player.User.OpenWorldStory
				where !x.IsDone
				select x.ToProto());
			Packet.c.Log($"[OW] GetOpenworldStory -> finish={rsp.FinishStoryIdLists.Length} cur={rsp.CurStoryLists.Count}");
		}

		session.Send(Packet.FromProto(rsp, CmdId.GetOpenworldStoryRsp));
	}
}
