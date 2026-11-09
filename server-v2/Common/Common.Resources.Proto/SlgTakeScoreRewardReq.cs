using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SlgTakeScoreRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6059
	}

	private IExtension __pbn__extensionData;

	private SlgScoreRewardType? __pbn__ScoreRewardType;

	[ProtoMember(1, Name = "score_reward_type")]
	[DefaultValue(SlgScoreRewardType.SlgScoreRewardTypeNone)]
	public SlgScoreRewardType ScoreRewardType
	{
		get
		{
			return __pbn__ScoreRewardType.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreRewardType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoreRewardType()
	{
		return __pbn__ScoreRewardType.HasValue;
	}

	public void ResetScoreRewardType()
	{
		__pbn__ScoreRewardType = null;
	}
}
