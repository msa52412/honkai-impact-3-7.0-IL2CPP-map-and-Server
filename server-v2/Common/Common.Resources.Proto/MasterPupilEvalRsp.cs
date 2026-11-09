using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilEvalRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 575
	}

	private IExtension __pbn__extensionData;

	private MasterPupilRetcode.Retcode? __pbn__Retcode;

	private uint? __pbn__Uid;

	private uint? __pbn__Star;

	private uint? __pbn__StepId;

	private string __pbn__Comment;

	private uint? __pbn__MasterScore;

	[ProtoMember(1, Name = "retcode")]
	[DefaultValue(MasterPupilRetcode.Retcode.Succ)]
	public MasterPupilRetcode.Retcode Retcode
	{
		get
		{
			return __pbn__Retcode.GetValueOrDefault();
		}
		set
		{
			__pbn__Retcode = value;
		}
	}

	[ProtoMember(2, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(3, Name = "star")]
	public uint Star
	{
		get
		{
			return __pbn__Star.GetValueOrDefault();
		}
		set
		{
			__pbn__Star = value;
		}
	}

	[ProtoMember(4, Name = "step_id")]
	public uint StepId
	{
		get
		{
			return __pbn__StepId.GetValueOrDefault();
		}
		set
		{
			__pbn__StepId = value;
		}
	}

	[ProtoMember(5, Name = "comment")]
	[DefaultValue("")]
	public string Comment
	{
		get
		{
			return __pbn__Comment ?? "";
		}
		set
		{
			__pbn__Comment = value;
		}
	}

	[ProtoMember(6, Name = "pool_reward")]
	public MasterPoolData PoolReward { get; set; }

	[ProtoMember(7, Name = "master_score")]
	public uint MasterScore
	{
		get
		{
			return __pbn__MasterScore.GetValueOrDefault();
		}
		set
		{
			__pbn__MasterScore = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeRetcode()
	{
		return __pbn__Retcode.HasValue;
	}

	public void ResetRetcode()
	{
		__pbn__Retcode = null;
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeStepId()
	{
		return __pbn__StepId.HasValue;
	}

	public void ResetStepId()
	{
		__pbn__StepId = null;
	}

	public bool ShouldSerializeComment()
	{
		return __pbn__Comment != null;
	}

	public void ResetComment()
	{
		__pbn__Comment = null;
	}

	public bool ShouldSerializeMasterScore()
	{
		return __pbn__MasterScore.HasValue;
	}

	public void ResetMasterScore()
	{
		__pbn__MasterScore = null;
	}
}
