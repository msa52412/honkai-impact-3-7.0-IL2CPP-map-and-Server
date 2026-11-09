using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetPictureActivityRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4267
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private bool? __pbn__IsShare;

	private bool? __pbn__IsTakeShareReward;

	private uint? __pbn__Score;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
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

	[ProtoMember(3, Name = "step_list")]
	public List<PictureStep> StepLists { get; } = new List<PictureStep>();


	[ProtoMember(4, Name = "is_share")]
	public bool IsShare
	{
		get
		{
			return __pbn__IsShare.GetValueOrDefault();
		}
		set
		{
			__pbn__IsShare = value;
		}
	}

	[ProtoMember(5, Name = "is_take_share_reward")]
	public bool IsTakeShareReward
	{
		get
		{
			return __pbn__IsTakeShareReward.GetValueOrDefault();
		}
		set
		{
			__pbn__IsTakeShareReward = value;
		}
	}

	[ProtoMember(6, Name = "score")]
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

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeIsShare()
	{
		return __pbn__IsShare.HasValue;
	}

	public void ResetIsShare()
	{
		__pbn__IsShare = null;
	}

	public bool ShouldSerializeIsTakeShareReward()
	{
		return __pbn__IsTakeShareReward.HasValue;
	}

	public void ResetIsTakeShareReward()
	{
		__pbn__IsTakeShareReward = null;
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
