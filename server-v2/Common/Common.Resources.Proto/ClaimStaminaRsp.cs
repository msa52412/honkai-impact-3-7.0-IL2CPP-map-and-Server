using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClaimStaminaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 650
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FACILITY_LOCK")]
		FacilityLock,
		[ProtoEnum(Name = "STORE_LACK")]
		StoreLack,
		[ProtoEnum(Name = "POWER_LACK")]
		PowerLack,
		[ProtoEnum(Name = "STAMINA_EXCEED")]
		StaminaExceed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Stamina;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "stamina")]
	public uint Stamina
	{
		get
		{
			return __pbn__Stamina.GetValueOrDefault();
		}
		set
		{
			__pbn__Stamina = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStamina()
	{
		return __pbn__Stamina.HasValue;
	}

	public void ResetStamina()
	{
		__pbn__Stamina = null;
	}
}
