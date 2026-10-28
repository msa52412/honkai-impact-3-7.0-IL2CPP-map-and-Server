using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarCultivateActivityRewardLineRecord : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__MaxScore;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "max_score")]
	public uint MaxScore
	{
		get
		{
			return __pbn__MaxScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MaxScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeMaxScore()
	{
		return __pbn__MaxScore.HasValue;
	}

	public void ResetMaxScore()
	{
		__pbn__MaxScore = null;
	}
}
