using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerScheduleRewardNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6340
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	[ProtoMember(1, Name = "score")]
	public uint Score
	{
		get
		{
			return __pbn__Score.GetValueOrDefault();
		}
		set
		{
			__pbn__Score = value;
		}
	}

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}
}
