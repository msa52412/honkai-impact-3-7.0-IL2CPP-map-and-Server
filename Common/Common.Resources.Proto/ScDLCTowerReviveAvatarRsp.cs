using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCTowerReviveAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6344
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "REVIVE_TIMES_LACK")]
		ReviveTimesLack,
		[ProtoEnum(Name = "NOT_IN_TOWER")]
		NotInTower,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ReviveTimes;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "revive_times")]
	public uint ReviveTimes
	{
		get
		{
			return __pbn__ReviveTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__ReviveTimes = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReviveTimes()
	{
		return __pbn__ReviveTimes.HasValue;
	}

	public void ResetReviveTimes()
	{
		__pbn__ReviveTimes = null;
	}
}
