using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeOpenworldActivityLevelRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4531
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityLevel;

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

	[ProtoMember(2, Name = "activity_level")]
	public uint ActivityLevel
	{
		get
		{
			return __pbn__ActivityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityLevel = value;
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

	public bool ShouldSerializeActivityLevel()
	{
		return __pbn__ActivityLevel.HasValue;
	}

	public void ResetActivityLevel()
	{
		__pbn__ActivityLevel = null;
	}
}
[ProtoContract]
public class TakeOpenworldActivityLevelRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4532
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LACK")]
		LevelLack,
		[ProtoEnum(Name = "HAVE_TAKEN")]
		HaveTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__ActivityLevel;

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

	[ProtoMember(3, Name = "activity_level")]
	public uint ActivityLevel
	{
		get
		{
			return __pbn__ActivityLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityLevel = value;
		}
	}

	[ProtoMember(4, Name = "reward_data_list")]
	public List<RewardData> RewardDataLists { get; } = new List<RewardData>();


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

	public bool ShouldSerializeActivityLevel()
	{
		return __pbn__ActivityLevel.HasValue;
	}

	public void ResetActivityLevel()
	{
		__pbn__ActivityLevel = null;
	}
}
