using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PvzActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ChallengeMaxFinishWave;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "unlocked_qavatar_list")]
	public List<PvzQAvatar> UnlockedQavatarLists { get; } = new List<PvzQAvatar>();


	[ProtoMember(3, Name = "site_info_list")]
	public List<PvzSiteInfo> SiteInfoLists { get; } = new List<PvzSiteInfo>();


	[ProtoMember(4, Name = "challenge_max_finish_wave")]
	public uint ChallengeMaxFinishWave
	{
		get
		{
			return __pbn__ChallengeMaxFinishWave.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeMaxFinishWave = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeChallengeMaxFinishWave()
	{
		return __pbn__ChallengeMaxFinishWave.HasValue;
	}

	public void ResetChallengeMaxFinishWave()
	{
		__pbn__ChallengeMaxFinishWave = null;
	}
}
