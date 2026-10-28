using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PupilStep : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StepId;

	private uint? __pbn__MasterId;

	private uint? __pbn__Star;

	private string __pbn__Comment;

	private uint? __pbn__Level;

	[ProtoMember(1, Name = "step_id")]
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

	[ProtoMember(2, Name = "master_id")]
	public uint MasterId
	{
		get
		{
			return __pbn__MasterId.GetValueOrDefault();
		}
		set
		{
			__pbn__MasterId = value;
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

	[ProtoMember(4, Name = "comment")]
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

	[ProtoMember(5, Name = "level")]
	public uint Level
	{
		get
		{
			return __pbn__Level.GetValueOrDefault();
		}
		set
		{
			__pbn__Level = value;
		}
	}

	[ProtoMember(6, Name = "pool_reward")]
	public MasterPoolData PoolReward { get; set; }

	[ProtoMember(7, Name = "exam_mission_id_list")]
	public uint[] ExamMissionIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStepId()
	{
		return __pbn__StepId.HasValue;
	}

	public void ResetStepId()
	{
		__pbn__StepId = null;
	}

	public bool ShouldSerializeMasterId()
	{
		return __pbn__MasterId.HasValue;
	}

	public void ResetMasterId()
	{
		__pbn__MasterId = null;
	}

	public bool ShouldSerializeStar()
	{
		return __pbn__Star.HasValue;
	}

	public void ResetStar()
	{
		__pbn__Star = null;
	}

	public bool ShouldSerializeComment()
	{
		return __pbn__Comment != null;
	}

	public void ResetComment()
	{
		__pbn__Comment = null;
	}

	public bool ShouldSerializeLevel()
	{
		return __pbn__Level.HasValue;
	}

	public void ResetLevel()
	{
		__pbn__Level = null;
	}
}
