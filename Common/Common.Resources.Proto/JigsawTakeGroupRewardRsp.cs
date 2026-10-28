using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class JigsawTakeGroupRewardRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4282
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "JIGSAW_NOT_BEGIN")]
		JigsawNotBegin,
		[ProtoEnum(Name = "PRE_JIGSAW_NOT_FINISHED")]
		PreJigsawNotFinished,
		[ProtoEnum(Name = "NOT_FINISHED")]
		NotFinished,
		[ProtoEnum(Name = "HAS_TAKEN")]
		HasTaken
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__JigsawId;

	private uint? __pbn__GroupId;

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

	[ProtoMember(3, Name = "jigsaw_id")]
	public uint JigsawId
	{
		get
		{
			return __pbn__JigsawId.GetValueOrDefault();
		}
		set
		{
			__pbn__JigsawId = value;
		}
	}

	[ProtoMember(4, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(5, Name = "reward")]
	public RewardData Reward { get; set; }

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

	public bool ShouldSerializeJigsawId()
	{
		return __pbn__JigsawId.HasValue;
	}

	public void ResetJigsawId()
	{
		__pbn__JigsawId = null;
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}
}
