using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaDisplayInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private GachaType? __pbn__GachaType;

	[ProtoMember(1, Name = "gacha_type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType GachaType
	{
		get
		{
			return __pbn__GachaType ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__GachaType = value;
		}
	}

	[ProtoMember(2, Name = "common_data")]
	public GachaDisplayCommonData CommonData { get; set; }

	[ProtoMember(3, Name = "hcoin_gacha_data")]
	public HcoinGachaData HcoinGachaData { get; set; }

	[ProtoMember(4, Name = "friends_point_gacha_data")]
	public FriendsPointGachaData FriendsPointGachaData { get; set; }

	[ProtoMember(5, Name = "activity_gacha_data")]
	public ActivityGachaData ActivityGachaData { get; set; }

	[ProtoMember(6, Name = "custom_gacha_data")]
	public CustomGachaData CustomGachaData { get; set; }

	[ProtoMember(7, Name = "unique_gacha_data")]
	public UniqueGachaData UniqueGachaData { get; set; }

	[ProtoMember(8, Name = "box_gacha_data")]
	public BoxGachaData BoxGachaData { get; set; }

	[ProtoMember(9, Name = "pray_gacha_data")]
	public PrayGachaData PrayGachaData { get; set; }

	[ProtoMember(10, Name = "controllable_box_gacha_data")]
	public ControllableBoxGachaData ControllableBoxGachaData { get; set; }

	[ProtoMember(11, Name = "optional_gacha_data")]
	public OptionalGachaData OptionalGachaData { get; set; }

	[ProtoMember(12, Name = "adventure_gacha_data")]
	public AdventureGachaData AdventureGachaData { get; set; }

	[ProtoMember(13, Name = "adventure_equip_gacha_data")]
	public AdventureEquipGachaData AdventureEquipGachaData { get; set; }

	[ProtoMember(14, Name = "sp_equip_gacha_data")]
	public SpEquipGachaData SpEquipGachaData { get; set; }

	[ProtoMember(15, Name = "special_gacha_data")]
	public SpecialCommonGachaData SpecialGachaData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaType()
	{
		return __pbn__GachaType.HasValue;
	}

	public void ResetGachaType()
	{
		__pbn__GachaType = null;
	}
}
