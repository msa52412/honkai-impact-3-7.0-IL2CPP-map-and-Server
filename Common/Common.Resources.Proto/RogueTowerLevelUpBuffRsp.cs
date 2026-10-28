using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RogueTowerLevelUpBuffRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4371
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NO_BUFF")]
		NoBuff,
		[ProtoEnum(Name = "COIN_LACK")]
		CoinLack,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__BuffId;

	private uint? __pbn__BuffLevel;

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

	[ProtoMember(3, Name = "buff_id")]
	public uint BuffId
	{
		get
		{
			return __pbn__BuffId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffId = value;
		}
	}

	[ProtoMember(4, Name = "buff_level")]
	public uint BuffLevel
	{
		get
		{
			return __pbn__BuffLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffLevel = value;
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

	public bool ShouldSerializeBuffId()
	{
		return __pbn__BuffId.HasValue;
	}

	public void ResetBuffId()
	{
		__pbn__BuffId = null;
	}

	public bool ShouldSerializeBuffLevel()
	{
		return __pbn__BuffLevel.HasValue;
	}

	public void ResetBuffLevel()
	{
		__pbn__BuffLevel = null;
	}
}
