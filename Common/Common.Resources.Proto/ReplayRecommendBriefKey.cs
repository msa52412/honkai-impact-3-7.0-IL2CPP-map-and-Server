using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayRecommendBriefKey : IExtensible
{
	private IExtension __pbn__extensionData;

	private ReplayType? __pbn__Type;

	private uint? __pbn__StageOrBossId;

	private uint? __pbn__Rank;

	[ProtoMember(1, Name = "type")]
	[DefaultValue(ReplayType.ReplayNone)]
	public ReplayType Type
	{
		get
		{
			return __pbn__Type.GetValueOrDefault();
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(2, Name = "stage_or_boss_id")]
	public uint StageOrBossId
	{
		get
		{
			return __pbn__StageOrBossId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageOrBossId = value;
		}
	}

	[ProtoMember(3, Name = "rank")]
	public uint Rank
	{
		get
		{
			return __pbn__Rank.GetValueOrDefault();
		}
		set
		{
			__pbn__Rank = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializeStageOrBossId()
	{
		return __pbn__StageOrBossId.HasValue;
	}

	public void ResetStageOrBossId()
	{
		__pbn__StageOrBossId = null;
	}

	public bool ShouldSerializeRank()
	{
		return __pbn__Rank.HasValue;
	}

	public void ResetRank()
	{
		__pbn__Rank = null;
	}
}
