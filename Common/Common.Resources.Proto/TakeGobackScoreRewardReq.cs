using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGobackScoreRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1448
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ScoreId;

	[ProtoMember(1, Name = "score_id")]
	public uint ScoreId
	{
		get
		{
			return __pbn__ScoreId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScoreId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScoreId()
	{
		return __pbn__ScoreId.HasValue;
	}

	public void ResetScoreId()
	{
		__pbn__ScoreId = null;
	}
}
