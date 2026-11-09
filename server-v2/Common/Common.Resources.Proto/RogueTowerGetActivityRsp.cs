using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueTowerGetActivityRsp : IExtensible
{
	[ProtoContract]
	public class RandomSiteStage : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__SiteId;

		private uint? __pbn__StageId;

		[ProtoMember(1, Name = "site_id")]
		public uint SiteId
		{
			get
			{
				return __pbn__SiteId.GetValueOrDefault();
			}
			set
			{
				__pbn__SiteId = value;
			}
		}

		[ProtoMember(2, Name = "stage_id")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeSiteId()
		{
			return __pbn__SiteId.HasValue;
		}

		public void ResetSiteId()
		{
			__pbn__SiteId = null;
		}

		public bool ShouldSerializeStageId()
		{
			return __pbn__StageId.HasValue;
		}

		public void ResetStageId()
		{
			__pbn__StageId = null;
		}
	}

	[ProtoContract]
	public class RogueTowerBuffStatus : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__RefreshBuffTimes;

		[ProtoMember(1, Name = "refresh_buff_times")]
		public uint RefreshBuffTimes
		{
			get
			{
				return __pbn__RefreshBuffTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__RefreshBuffTimes = value;
			}
		}

		[ProtoMember(2, Name = "buff_list")]
		public List<RogueBuff> BuffLists { get; } = new List<RogueBuff>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeRefreshBuffTimes()
		{
			return __pbn__RefreshBuffTimes.HasValue;
		}

		public void ResetRefreshBuffTimes()
		{
			__pbn__RefreshBuffTimes = null;
		}
	}

	[ProtoContract]
	public class RogueTowerCandidate : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__CurPoolId;

		[ProtoMember(1, Name = "cur_pool_id")]
		public uint CurPoolId
		{
			get
			{
				return __pbn__CurPoolId.GetValueOrDefault();
			}
			set
			{
				__pbn__CurPoolId = value;
			}
		}

		[ProtoMember(2, Name = "cur_candidate_id_list")]
		public uint[] CurCandidateIdLists { get; set; }

		[ProtoMember(3, Name = "wait_pool_list")]
		public List<WaitPool> WaitPoolLists { get; } = new List<WaitPool>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeCurPoolId()
		{
			return __pbn__CurPoolId.HasValue;
		}

		public void ResetCurPoolId()
		{
			__pbn__CurPoolId = null;
		}
	}

	[ProtoContract]
	public class RogueTowerChallengeStatus : IExtensible
	{
		private IExtension __pbn__extensionData;

		private bool? __pbn__IsChallengeFail;

		private uint? __pbn__CurAreaId;

		[ProtoMember(1, Name = "avatar_list")]
		public List<ChallengeModeAvatar> AvatarLists { get; } = new List<ChallengeModeAvatar>();


		[ProtoMember(2, Name = "is_challenge_fail")]
		public bool IsChallengeFail
		{
			get
			{
				return __pbn__IsChallengeFail.GetValueOrDefault();
			}
			set
			{
				__pbn__IsChallengeFail = value;
			}
		}

		[ProtoMember(3, Name = "cur_area_id")]
		public uint CurAreaId
		{
			get
			{
				return __pbn__CurAreaId.GetValueOrDefault();
			}
			set
			{
				__pbn__CurAreaId = value;
			}
		}

		[ProtoMember(4, Name = "site_stage_list")]
		public List<RandomSiteStage> SiteStageLists { get; } = new List<RandomSiteStage>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeIsChallengeFail()
		{
			return __pbn__IsChallengeFail.HasValue;
		}

		public void ResetIsChallengeFail()
		{
			__pbn__IsChallengeFail = null;
		}

		public bool ShouldSerializeCurAreaId()
		{
			return __pbn__CurAreaId.HasValue;
		}

		public void ResetCurAreaId()
		{
			__pbn__CurAreaId = null;
		}
	}

	[ProtoContract]
	public class RogueTowerEndlessScore : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__AvatarId;

		private uint? __pbn__Score;

		[ProtoMember(1, Name = "avatar_id")]
		public uint AvatarId
		{
			get
			{
				return __pbn__AvatarId.GetValueOrDefault();
			}
			set
			{
				__pbn__AvatarId = value;
			}
		}

		[ProtoMember(2, Name = "score")]
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

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeAvatarId()
		{
			return __pbn__AvatarId.HasValue;
		}

		public void ResetAvatarId()
		{
			__pbn__AvatarId = null;
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

	[ProtoContract]
	public class RogueTowerEndlessStatus : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__EndlessScore;

		private uint? __pbn__LastSettleRewardScore;

		private uint? __pbn__EndlessFloor;

		[ProtoMember(1, Name = "endless_score")]
		public uint EndlessScore
		{
			get
			{
				return __pbn__EndlessScore.GetValueOrDefault();
			}
			set
			{
				__pbn__EndlessScore = value;
			}
		}

		[ProtoMember(2, Name = "last_settle_reward_score")]
		public uint LastSettleRewardScore
		{
			get
			{
				return __pbn__LastSettleRewardScore.GetValueOrDefault();
			}
			set
			{
				__pbn__LastSettleRewardScore = value;
			}
		}

		[ProtoMember(3, Name = "endless_floor")]
		public uint EndlessFloor
		{
			get
			{
				return __pbn__EndlessFloor.GetValueOrDefault();
			}
			set
			{
				__pbn__EndlessFloor = value;
			}
		}

		[ProtoMember(4, Name = "avatar_max_score_list")]
		public List<RogueTowerEndlessScore> AvatarMaxScoreLists { get; } = new List<RogueTowerEndlessScore>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeEndlessScore()
		{
			return __pbn__EndlessScore.HasValue;
		}

		public void ResetEndlessScore()
		{
			__pbn__EndlessScore = null;
		}

		public bool ShouldSerializeLastSettleRewardScore()
		{
			return __pbn__LastSettleRewardScore.HasValue;
		}

		public void ResetLastSettleRewardScore()
		{
			__pbn__LastSettleRewardScore = null;
		}

		public bool ShouldSerializeEndlessFloor()
		{
			return __pbn__EndlessFloor.HasValue;
		}

		public void ResetEndlessFloor()
		{
			__pbn__EndlessFloor = null;
		}
	}

	[ProtoContract]
	public class RogueTowerSnapshot : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__StagePassCount;

		private uint? __pbn__Score;

		[ProtoMember(1, Name = "stage_pass_count")]
		public uint StagePassCount
		{
			get
			{
				return __pbn__StagePassCount.GetValueOrDefault();
			}
			set
			{
				__pbn__StagePassCount = value;
			}
		}

		[ProtoMember(2, Name = "score")]
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

		[ProtoMember(3, Name = "avatar_id_list")]
		public uint[] AvatarIdLists { get; set; }

		[ProtoMember(4, Name = "selected_buff_list")]
		public List<RogueBuff> SelectedBuffLists { get; } = new List<RogueBuff>();


		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeStagePassCount()
		{
			return __pbn__StagePassCount.HasValue;
		}

		public void ResetStagePassCount()
		{
			__pbn__StagePassCount = null;
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

	[ProtoContract]
	public class WaitPool : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__PoolId;

		private uint? __pbn__LeftTimes;

		[ProtoMember(1, Name = "pool_id")]
		public uint PoolId
		{
			get
			{
				return __pbn__PoolId.GetValueOrDefault();
			}
			set
			{
				__pbn__PoolId = value;
			}
		}

		[ProtoMember(2, Name = "left_times")]
		public uint LeftTimes
		{
			get
			{
				return __pbn__LeftTimes.GetValueOrDefault();
			}
			set
			{
				__pbn__LeftTimes = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializePoolId()
		{
			return __pbn__PoolId.HasValue;
		}

		public void ResetPoolId()
		{
			__pbn__PoolId = null;
		}

		public bool ShouldSerializeLeftTimes()
		{
			return __pbn__LeftTimes.HasValue;
		}

		public void ResetLeftTimes()
		{
			__pbn__LeftTimes = null;
		}
	}

	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4361
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__LeftSupportAvatarLevelupTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(3, Name = "challenge_status")]
	public RogueTowerChallengeStatus ChallengeStatus { get; set; }

	[ProtoMember(4, Name = "buff_status")]
	public RogueTowerBuffStatus BuffStatus { get; set; }

	[ProtoMember(5, Name = "buff_candidate")]
	public RogueTowerCandidate BuffCandidate { get; set; }

	[ProtoMember(6, Name = "endless_status")]
	public RogueTowerEndlessStatus EndlessStatus { get; set; }

	[ProtoMember(7, Name = "save_list")]
	public List<RogueTowerSnapshot> SaveLists { get; } = new List<RogueTowerSnapshot>();


	[ProtoMember(8, Name = "left_support_avatar_levelup_times")]
	public uint LeftSupportAvatarLevelupTimes
	{
		get
		{
			return __pbn__LeftSupportAvatarLevelupTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__LeftSupportAvatarLevelupTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeLeftSupportAvatarLevelupTimes()
	{
		return __pbn__LeftSupportAvatarLevelupTimes.HasValue;
	}

	public void ResetLeftSupportAvatarLevelupTimes()
	{
		__pbn__LeftSupportAvatarLevelupTimes = null;
	}
}
