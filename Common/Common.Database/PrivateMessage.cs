using System.Collections.Generic;
using System.Linq;
using Common.Resources.Proto;

namespace Common.Database;

public class PrivateMessage
{
	public class PrivateMessageComparer : IEqualityComparer<PrivateMessageScheme>
	{
		public bool Equals(PrivateMessageScheme x, PrivateMessageScheme y)
		{
			return x.SenderUid == y.TargetUid || y.SenderUid == x.TargetUid;
		}

		public int GetHashCode(PrivateMessageScheme obj)
		{
			return (obj.SenderUid + obj.TargetUid).GetHashCode();
		}
	}

	public static readonly SqlTable<PrivateMessageScheme> collection = Global.Table<PrivateMessageScheme>("PrivateMessages", ("sender_uid", "INTEGER", x => x.SenderUid, false), ("target_uid", "INTEGER", x => x.TargetUid, false));

	public static PrivateMessageScheme Create(uint uid, uint targetUid, ChatMsg chatMsg)
	{
		PrivateMessageScheme privateMessageScheme = new PrivateMessageScheme
		{
			Msg = chatMsg,
			SenderUid = uid,
			TargetUid = targetUid,
			TimeSent = Global.GetUnixInSeconds()
		};
		collection.InsertOne(privateMessageScheme);
		return privateMessageScheme;
	}

	public static List<HistoryPrivateChatMsg> GetMessages(uint uid, uint targetUid = 0u)
	{
		List<HistoryPrivateChatMsg> list = new List<HistoryPrivateChatMsg>();
		if (uid == 0)
		{
			IEnumerable<IGrouping<PrivateMessageScheme, PrivateMessageScheme>> enumerable = (from x in collection.All()
				where x.SenderUid == uid || x.TargetUid == uid
				select x).ToList().GroupBy((PrivateMessageScheme x) => x, new PrivateMessageComparer());
			foreach (IGrouping<PrivateMessageScheme, PrivateMessageScheme> item in enumerable)
			{
				List<PrivateMessageScheme> source = item.ToList();
				targetUid = ((source.First().TargetUid == uid) ? source.First().SenderUid : source.First().TargetUid);
				list.Add(new HistoryPrivateChatMsg
				{
					Uid = targetUid
				});
				list.First((HistoryPrivateChatMsg x) => x.Uid == targetUid).ChatMsgLists.AddRange(source.Select((PrivateMessageScheme x) => x.Msg));
			}
		}
		else
		{
			List<PrivateMessageScheme> source2 = (from x in collection.All()
				where (x.SenderUid == uid && x.TargetUid == targetUid) || (x.SenderUid == targetUid && x.TargetUid == uid)
				select x).ToList();
			list.Add(new HistoryPrivateChatMsg
			{
				Uid = targetUid
			});
			list.First((HistoryPrivateChatMsg x) => x.Uid == targetUid).ChatMsgLists.AddRange(source2.Select((PrivateMessageScheme x) => x.Msg));
		}
		return list;
	}
}
