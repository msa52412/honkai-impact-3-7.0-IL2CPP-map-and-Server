using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldChapterEndlessChallenge : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChallengeId;

	private bool? __pbn__IsPassed;

	private uint? __pbn__MaxScore;

	[ProtoMember(1, Name = "challenge_id")]
	public uint ChallengeId
	{
		get
		{
			return __pbn__ChallengeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeId = value;
		}
	}

	[ProtoMember(2, Name = "is_passed")]
	public bool IsPassed
	{
		get
		{
			return __pbn__IsPassed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsPassed = value;
		}
	}

	[ProtoMember(3, Name = "max_score")]
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

	public bool ShouldSerializeChallengeId()
	{
		return __pbn__ChallengeId.HasValue;
	}

	public void ResetChallengeId()
	{
		__pbn__ChallengeId = null;
	}

	public bool ShouldSerializeIsPassed()
	{
		return __pbn__IsPassed.HasValue;
	}

	public void ResetIsPassed()
	{
		__pbn__IsPassed = null;
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
[ProtoContract]
public class OpenworldChapterEndlessChallengeBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4632
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__ChallengeId;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "challenge_id")]
	public uint ChallengeId
	{
		get
		{
			return __pbn__ChallengeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeId = value;
		}
	}

	[ProtoMember(3, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeChallengeId()
	{
		return __pbn__ChallengeId.HasValue;
	}

	public void ResetChallengeId()
	{
		__pbn__ChallengeId = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
[ProtoContract]
public class OpenworldChapterEndlessChallengeBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4633
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CHALLENGE_ERROR")]
		ChallengeError,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "PRE_CHALLENGE_NOT_FINISH")]
		PreChallengeNotFinish,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
[ProtoContract]
public class OpenworldChapterEndlessChallengeEndReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4634
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MapId;

	private uint? __pbn__ChallengeId;

	private uint? __pbn__Score;

	private string __pbn__ReportExt;

	[ProtoMember(1, Name = "map_id")]
	public uint MapId
	{
		get
		{
			return __pbn__MapId.GetValueOrDefault();
		}
		set
		{
			__pbn__MapId = value;
		}
	}

	[ProtoMember(2, Name = "challenge_id")]
	public uint ChallengeId
	{
		get
		{
			return __pbn__ChallengeId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChallengeId = value;
		}
	}

	[ProtoMember(3, Name = "score")]
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

	[ProtoMember(4, Name = "report_ext")]
	[DefaultValue("")]
	public string ReportExt
	{
		get
		{
			return __pbn__ReportExt ?? "";
		}
		set
		{
			__pbn__ReportExt = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMapId()
	{
		return __pbn__MapId.HasValue;
	}

	public void ResetMapId()
	{
		__pbn__MapId = null;
	}

	public bool ShouldSerializeChallengeId()
	{
		return __pbn__ChallengeId.HasValue;
	}

	public void ResetChallengeId()
	{
		__pbn__ChallengeId = null;
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeReportExt()
	{
		return __pbn__ReportExt != null;
	}

	public void ResetReportExt()
	{
		__pbn__ReportExt = null;
	}
}
[ProtoContract]
public class OpenworldChapterEndlessChallengeEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4635
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "CHALLENGE_ERROR")]
		ChallengeError,
		[ProtoEnum(Name = "MAP_ERROR")]
		MapError,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "NOT_BEGIN")]
		NotBegin
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
