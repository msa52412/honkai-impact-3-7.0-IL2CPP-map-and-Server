using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;
using Common.Utils.ExcelReader;
using PemukulPaku.GameServer.Game;

namespace PemukulPaku.GameServer.Handlers.Three;

/// <summary>
/// 收藏 → 活动相册（照片）。
///
/// 照片 id 来自 CityEventPhoto.json（11000 ~ 1714007），按 PhotoType 分桶：
///   1 城市活动 / 3 RPG剧情 / 4 农场 / 5 王国战争 / 6 DLC / 9 章节隐藏 /
///   13 开放世界章节 / 15 开放世界狩猎 …
/// type = 0（PHOTO_TYPE_ALL）回全量。
///
/// 影像（CG）那一栏按反编译的 MonoWikiCGPanel / EFEDADMICAM 来看，解锁类型是
/// CGUnlockType.Level / Material / OWStory 三种，服务端这边能影响它的只有「已获得 id」与
/// 本响应里的 photo_id_list，所以这两个通道都补上 CG id：
/// 相册自己的 CityEventPhoto id 保留在最前面（缺了相册会整页渲染失败只剩锁图标），
/// CG id 追加在后面，真 id 不够再按 collection_cg.txt 里的区间补。
/// 用运行目录 collection_cgphoto.txt 写 0 可以关掉这里的 CG 追加。
/// </summary>
[PacketCmdId(CmdId.GetPhotoDataReq)]
internal class GetPhotoDataReqHandler : IPacketHandler
{
	private static readonly Dictionary<uint, uint[]> Cache = new Dictionary<uint, uint[]>();

	public void Handle(Session session, Packet packet)
	{
		GetPhotoDataReq decodedBody = packet.GetDecodedBody<GetPhotoDataReq>();
        if(decodedBody.Type==12){GodWarProgression.PushCollections(session);return;}

		List<uint> ids = new List<uint>(IdsOf(decodedBody.Type));

		int albumCount = ids.Count;
		int cgCount = 0;

		if (CollectionPad.Switch("collection_cgphoto.txt", true))
		{
			HashSet<uint> seen = new HashSet<uint>(ids);
			foreach (uint cgId in BaseExcelReader<CgData, CgDataExcel>.GetInstance().AllIds())
			{
				if (seen.Add(cgId))
				{
					ids.Add(cgId);
				}
			}

			foreach (uint cgId in CollectionPad.CgRange())
			{
				if (seen.Add(cgId))
				{
					ids.Add(cgId);
				}
			}

			cgCount = ids.Count - albumCount;
		}

		GetPhotoDataRsp godwarPhotos=null;
        if(decodedBody.Type==0){godwarPhotos=GodWarProgression.CollectionsReply(session.Player);ids=GodWarProgression.FilterCollectionPhotos(ids.ToArray(),godwarPhotos).ToList();albumCount=ids.Count-cgCount;}

		Packet.c.Log(string.Format("[Collection] Photo(活动相册) type={0} 相册{1} + CG{2} = {3} 个",
			decodedBody.Type, albumCount, cgCount, ids.Count));

		session.Send(Packet.FromProto(new GetPhotoDataRsp
		{
			retcode = GetPhotoDataRsp.Retcode.Succ,
			Type = decodedBody.Type,
			PhotoIdLists = ids.ToArray()
		}, CmdId.GetPhotoDataRsp));
        if(godwarPhotos!=null)session.Send(Packet.FromProto(godwarPhotos,CmdId.GetPhotoDataRsp));
	}

	private static uint[] IdsOf(uint type)
	{
		lock (Cache)
		{
			if (Cache.TryGetValue(type, out uint[]? cached))
			{
				return cached;
			}

			CityEventPhoto reader = BaseExcelReader<CityEventPhoto, CityEventPhotoExcel>.GetInstance();
			uint[] ids = type == 0u ? reader.AllIds() : reader.IdsOfType(type);
			Cache[type] = ids;
			return ids;
		}
	}
}
