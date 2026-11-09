using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerStageEndRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6337
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_BEGIN")]
		NotBegin,
		[ProtoEnum(Name = "FLOOR_ERROR")]
		FloorError,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Score;

	private uint? __pbn__Floor;

	private uint? __pbn__SettleFloor;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "reward_data")]
	public RewardData RewardData { get; set; }

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

	[ProtoMember(4, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	[ProtoMember(5, Name = "settle_floor")]
	public uint SettleFloor
	{
		get
		{
			return __pbn__SettleFloor.GetValueOrDefault();
		}
		set
		{
			__pbn__SettleFloor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScore()
	{
		return __pbn__Score.HasValue;
	}

	public void ResetScore()
	{
		__pbn__Score = null;
	}

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}

	public bool ShouldSerializeSettleFloor()
	{
		return __pbn__SettleFloor.HasValue;
	}

	public void ResetSettleFloor()
	{
		__pbn__SettleFloor = null;
	}
}
