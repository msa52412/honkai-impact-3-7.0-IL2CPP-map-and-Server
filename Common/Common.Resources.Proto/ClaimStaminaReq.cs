using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ClaimStaminaReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 649
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Stamina;

	[ProtoMember(1, Name = "stamina")]
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
