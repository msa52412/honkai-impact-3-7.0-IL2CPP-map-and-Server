using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldGetPrayInfoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2452
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

	private uint? __pbn__PrayId;

	private bool? __pbn__IsHasPrayed;

	private uint? __pbn__HasTakenCharacterPrayNum;

	private uint? __pbn__TotalCharacterPrayNum;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "pray_id")]
	public uint PrayId
	{
		get
		{
			return __pbn__PrayId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrayId = value;
		}
	}

	[ProtoMember(3, Name = "is_has_prayed")]
	public bool IsHasPrayed
	{
		get
		{
			return __pbn__IsHasPrayed.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasPrayed = value;
		}
	}

	[ProtoMember(4, Name = "has_taken_character_pray_num")]
	public uint HasTakenCharacterPrayNum
	{
		get
		{
			return __pbn__HasTakenCharacterPrayNum.GetValueOrDefault();
		}
		set
		{
			__pbn__HasTakenCharacterPrayNum = value;
		}
	}

	[ProtoMember(5, Name = "total_character_pray_num")]
	public uint TotalCharacterPrayNum
	{
		get
		{
			return __pbn__TotalCharacterPrayNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TotalCharacterPrayNum = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePrayId()
	{
		return __pbn__PrayId.HasValue;
	}

	public void ResetPrayId()
	{
		__pbn__PrayId = null;
	}

	public bool ShouldSerializeIsHasPrayed()
	{
		return __pbn__IsHasPrayed.HasValue;
	}

	public void ResetIsHasPrayed()
	{
		__pbn__IsHasPrayed = null;
	}

	public bool ShouldSerializeHasTakenCharacterPrayNum()
	{
		return __pbn__HasTakenCharacterPrayNum.HasValue;
	}

	public void ResetHasTakenCharacterPrayNum()
	{
		__pbn__HasTakenCharacterPrayNum = null;
	}

	public bool ShouldSerializeTotalCharacterPrayNum()
	{
		return __pbn__TotalCharacterPrayNum.HasValue;
	}

	public void ResetTotalCharacterPrayNum()
	{
		__pbn__TotalCharacterPrayNum = null;
	}
}
