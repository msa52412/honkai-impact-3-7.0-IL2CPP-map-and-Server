using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlatformShooterWeaponAffix : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__CurAffixId;

	private uint? __pbn__WaitConfirmAffixId;

	[ProtoMember(1, Name = "cur_affix_id")]
	public uint CurAffixId
	{
		get
		{
			return __pbn__CurAffixId.GetValueOrDefault();
		}
		set
		{
			__pbn__CurAffixId = value;
		}
	}

	[ProtoMember(2, Name = "wait_confirm_affix_id")]
	public uint WaitConfirmAffixId
	{
		get
		{
			return __pbn__WaitConfirmAffixId.GetValueOrDefault();
		}
		set
		{
			__pbn__WaitConfirmAffixId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCurAffixId()
	{
		return __pbn__CurAffixId.HasValue;
	}

	public void ResetCurAffixId()
	{
		__pbn__CurAffixId = null;
	}

	public bool ShouldSerializeWaitConfirmAffixId()
	{
		return __pbn__WaitConfirmAffixId.HasValue;
	}

	public void ResetWaitConfirmAffixId()
	{
		__pbn__WaitConfirmAffixId = null;
	}
}
