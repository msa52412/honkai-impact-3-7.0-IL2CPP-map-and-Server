using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaExFragment : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ExId;

	private uint? __pbn__ExEnergy;

	private uint? __pbn__ExNum;

	private uint? __pbn__ExEnergyAdd;

	private uint? __pbn__FragmentAdd;

	private uint? __pbn__ExEnergyAddOrig;

	private uint? __pbn__ExEnergyAddBonus;

	private uint? __pbn__AvatarId;

	[ProtoMember(1, Name = "ex_id")]
	public uint ExId
	{
		get
		{
			return __pbn__ExId.GetValueOrDefault();
		}
		set
		{
			__pbn__ExId = value;
		}
	}

	[ProtoMember(2, Name = "ex_energy")]
	public uint ExEnergy
	{
		get
		{
			return __pbn__ExEnergy.GetValueOrDefault();
		}
		set
		{
			__pbn__ExEnergy = value;
		}
	}

	[ProtoMember(3, Name = "ex_num")]
	public uint ExNum
	{
		get
		{
			return __pbn__ExNum.GetValueOrDefault();
		}
		set
		{
			__pbn__ExNum = value;
		}
	}

	[ProtoMember(4, Name = "ex_energy_add")]
	public uint ExEnergyAdd
	{
		get
		{
			return __pbn__ExEnergyAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__ExEnergyAdd = value;
		}
	}

	[ProtoMember(5, Name = "fragment_add")]
	public uint FragmentAdd
	{
		get
		{
			return __pbn__FragmentAdd.GetValueOrDefault();
		}
		set
		{
			__pbn__FragmentAdd = value;
		}
	}

	[ProtoMember(6, Name = "ex_energy_add_orig")]
	public uint ExEnergyAddOrig
	{
		get
		{
			return __pbn__ExEnergyAddOrig.GetValueOrDefault();
		}
		set
		{
			__pbn__ExEnergyAddOrig = value;
		}
	}

	[ProtoMember(7, Name = "ex_energy_add_bonus")]
	public uint ExEnergyAddBonus
	{
		get
		{
			return __pbn__ExEnergyAddBonus.GetValueOrDefault();
		}
		set
		{
			__pbn__ExEnergyAddBonus = value;
		}
	}

	[ProtoMember(8, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExId()
	{
		return __pbn__ExId.HasValue;
	}

	public void ResetExId()
	{
		__pbn__ExId = null;
	}

	public bool ShouldSerializeExEnergy()
	{
		return __pbn__ExEnergy.HasValue;
	}

	public void ResetExEnergy()
	{
		__pbn__ExEnergy = null;
	}

	public bool ShouldSerializeExNum()
	{
		return __pbn__ExNum.HasValue;
	}

	public void ResetExNum()
	{
		__pbn__ExNum = null;
	}

	public bool ShouldSerializeExEnergyAdd()
	{
		return __pbn__ExEnergyAdd.HasValue;
	}

	public void ResetExEnergyAdd()
	{
		__pbn__ExEnergyAdd = null;
	}

	public bool ShouldSerializeFragmentAdd()
	{
		return __pbn__FragmentAdd.HasValue;
	}

	public void ResetFragmentAdd()
	{
		__pbn__FragmentAdd = null;
	}

	public bool ShouldSerializeExEnergyAddOrig()
	{
		return __pbn__ExEnergyAddOrig.HasValue;
	}

	public void ResetExEnergyAddOrig()
	{
		__pbn__ExEnergyAddOrig = null;
	}

	public bool ShouldSerializeExEnergyAddBonus()
	{
		return __pbn__ExEnergyAddBonus.HasValue;
	}

	public void ResetExEnergyAddBonus()
	{
		__pbn__ExEnergyAddBonus = null;
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}
}
