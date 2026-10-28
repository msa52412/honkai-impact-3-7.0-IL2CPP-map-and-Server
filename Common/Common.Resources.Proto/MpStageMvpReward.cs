using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageMvpReward : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ExtraCoin;

	private uint? __pbn__ExtraAvatarExp;

	private uint? __pbn__ExtraMpExp;

	[ProtoMember(1, Name = "extra_coin")]
	public uint ExtraCoin
	{
		get
		{
			return __pbn__ExtraCoin.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraCoin = value;
		}
	}

	[ProtoMember(2, Name = "extra_avatar_exp")]
	public uint ExtraAvatarExp
	{
		get
		{
			return __pbn__ExtraAvatarExp.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraAvatarExp = value;
		}
	}

	[ProtoMember(3, Name = "extra_mp_exp")]
	public uint ExtraMpExp
	{
		get
		{
			return __pbn__ExtraMpExp.GetValueOrDefault();
		}
		set
		{
			__pbn__ExtraMpExp = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExtraCoin()
	{
		return __pbn__ExtraCoin.HasValue;
	}

	public void ResetExtraCoin()
	{
		__pbn__ExtraCoin = null;
	}

	public bool ShouldSerializeExtraAvatarExp()
	{
		return __pbn__ExtraAvatarExp.HasValue;
	}

	public void ResetExtraAvatarExp()
	{
		__pbn__ExtraAvatarExp = null;
	}

	public bool ShouldSerializeExtraMpExp()
	{
		return __pbn__ExtraMpExp.HasValue;
	}

	public void ResetExtraMpExp()
	{
		__pbn__ExtraMpExp = null;
	}
}
