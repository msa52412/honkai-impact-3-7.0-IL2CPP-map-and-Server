using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetArmadaDataRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2602
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack
	}

	private IExtension __pbn__extensionData;

	private ArmadaPlayerStatus? __pbn__Status;

	private uint? __pbn__JoinCdFinishTime;

	private bool? __pbn__IsNeedRecommend;

	private uint? __pbn__LastMailTime;

	private bool? __pbn__IsNew;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "status")]
	[DefaultValue(ArmadaPlayerStatus.ArmadaPlayerNotJoin)]
	public ArmadaPlayerStatus Status
	{
		get
		{
			return __pbn__Status ?? ArmadaPlayerStatus.ArmadaPlayerNotJoin;
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "armada")]
	public ArmadaData Armada { get; set; }

	[ProtoMember(4, Name = "apply_list")]
	public List<ArmadaBriefData> ApplyLists { get; } = new List<ArmadaBriefData>();


	[ProtoMember(5, Name = "join_cd_finish_time")]
	public uint JoinCdFinishTime
	{
		get
		{
			return __pbn__JoinCdFinishTime.GetValueOrDefault();
		}
		set
		{
			__pbn__JoinCdFinishTime = value;
		}
	}

	[ProtoMember(6, Name = "is_need_recommend")]
	public bool IsNeedRecommend
	{
		get
		{
			return __pbn__IsNeedRecommend.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNeedRecommend = value;
		}
	}

	[ProtoMember(7, Name = "last_mail_time")]
	public uint LastMailTime
	{
		get
		{
			return __pbn__LastMailTime.GetValueOrDefault();
		}
		set
		{
			__pbn__LastMailTime = value;
		}
	}

	[ProtoMember(8, Name = "recommend_armada")]
	public ArmadaBriefData RecommendArmada { get; set; }

	[ProtoMember(9, Name = "is_new")]
	public bool IsNew
	{
		get
		{
			return __pbn__IsNew.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNew = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeJoinCdFinishTime()
	{
		return __pbn__JoinCdFinishTime.HasValue;
	}

	public void ResetJoinCdFinishTime()
	{
		__pbn__JoinCdFinishTime = null;
	}

	public bool ShouldSerializeIsNeedRecommend()
	{
		return __pbn__IsNeedRecommend.HasValue;
	}

	public void ResetIsNeedRecommend()
	{
		__pbn__IsNeedRecommend = null;
	}

	public bool ShouldSerializeLastMailTime()
	{
		return __pbn__LastMailTime.HasValue;
	}

	public void ResetLastMailTime()
	{
		__pbn__LastMailTime = null;
	}

	public bool ShouldSerializeIsNew()
	{
		return __pbn__IsNew.HasValue;
	}

	public void ResetIsNew()
	{
		__pbn__IsNew = null;
	}
}
