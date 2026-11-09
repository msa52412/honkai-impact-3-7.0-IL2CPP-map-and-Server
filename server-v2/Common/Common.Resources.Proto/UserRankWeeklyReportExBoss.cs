using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class UserRankWeeklyReportExBoss : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__RankPercent;

	[ProtoMember(1, Name = "finished_boss_info_list")]
	public List<FinishedExBossInfo> FinishedBossInfoLists { get; } = new List<FinishedExBossInfo>();


	[ProtoMember(2, Name = "rank_percent")]
	public uint RankPercent
	{
		get
		{
			return __pbn__RankPercent.GetValueOrDefault();
		}
		set
		{
			__pbn__RankPercent = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRankPercent()
	{
		return __pbn__RankPercent.HasValue;
	}

	public void ResetRankPercent()
	{
		__pbn__RankPercent = null;
	}
}
