using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GeneralActivityGetStageRankReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4052
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private uint? __pbn__RankId;

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

	[ProtoMember(2, Name = "rank_id")]
	public uint RankId
	{
		get
		{
			return __pbn__RankId.GetValueOrDefault();
		}
		set
		{
			__pbn__RankId = value;
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

	public bool ShouldSerializeRankId()
	{
		return __pbn__RankId.HasValue;
	}

	public void ResetRankId()
	{
		__pbn__RankId = null;
	}
}
