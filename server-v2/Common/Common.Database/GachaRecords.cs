using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

/// <summary>
/// 抽卡记录（补给页左下角「历史记录」）。
///
/// 客户端点「历史记录」发 <c>GetGachaLogReq</c>，服务端按 gacha_type 分组返回
/// 最近若干条 —— 所以这里按「一次抽卡一行」存，读的时候再分组。
///
/// 只留最近 100 条/池：官服也是分页的，而且记录表涨得很快（十连一次 10 行），
/// 全量返回会让回包几百 KB。
/// </summary>
public class GachaRecords
{
	public class Row : IRow
	{
		public long Id { get; set; }

		public long CreatedAt { get; set; }

		public uint OwnerUid { get; set; }

		public uint GachaType { get; set; }

		/// <summary>抽到的时刻（Unix 秒），客户端历史记录里显示的就是这个。</summary>
		public uint Time { get; set; }

		public uint ItemId { get; set; }

		public uint Level { get; set; }

		public uint Num { get; set; }

		public uint SplitFragmentNum { get; set; }

		public bool IsRareDrop { get; set; }
	}

	public static readonly SqlTable<Row> collection = Global.Table<Row>("GachaRecords",
		("owner_uid", "INTEGER", x => x.OwnerUid, false),
		("gacha_type", "INTEGER", x => x.GachaType, false));

	/// <summary>一次抽卡结果写一行。调用方已经在事务里，这里不再自己开事务。</summary>
	public static void Append(uint uid, uint gachaType, GachaItem item)
	{
		if (item == null)
		{
			return;
		}
		collection.InsertOne(new Row
		{
			OwnerUid = uid,
			GachaType = gachaType,
			Time = (uint)System.DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
			ItemId = item.ItemId,
			Level = item.Level,
			Num = item.Num,
			SplitFragmentNum = item.SplitFragmentNum,
			IsRareDrop = item.IsRareDrop
		});
	}

	/// <summary>按池分组回包，每个池最多 100 条，新的在前。</summary>
	public static GetGachaLogRsp Read(uint uid)
	{
		GetGachaLogRsp rsp = new GetGachaLogRsp
		{
			retcode = GetGachaLogRsp.Retcode.Succ
		};

		IEnumerable<IGrouping<uint, Row>> groups = collection
			.Where(x => x.OwnerUid == uid)
			.GroupBy(x => x.GachaType);

		foreach (IGrouping<uint, Row> group in groups)
		{
			GetGachaLogRsp.GachaLogInfo info = new GetGachaLogRsp.GachaLogInfo
			{
				GachaType = (GachaType)(int)group.Key
			};
			foreach (Row row in group.OrderByDescending(x => x.Time).ThenByDescending(x => x.Id).Take(100))
			{
				info.LogLists.Add(new GachaLog
				{
					Time = row.Time,
					Item = new GachaItem
					{
						ItemId = row.ItemId,
						Level = row.Level,
						Num = row.Num,
						SplitFragmentNum = row.SplitFragmentNum,
						IsRareDrop = row.IsRareDrop
					}
				});
			}
			rsp.LogInfoLists.Add(info);
		}

		return rsp;
	}
}
