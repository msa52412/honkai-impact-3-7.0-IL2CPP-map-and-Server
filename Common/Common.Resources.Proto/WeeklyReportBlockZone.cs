using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WeeklyReportBlockZone : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TotalScore;

	[ProtoMember(1, Name = "total_score")]
	public uint TotalScore
	{
		get
		{
			return __pbn__TotalScore.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalScore = value;
		}
	}

	[ProtoMember(2, Name = "reward_id_list")]
	public uint[] RewardIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTotalScore()
	{
		return __pbn__TotalScore.HasValue;
	}

	public void ResetTotalScore()
	{
		__pbn__TotalScore = null;
	}
}
