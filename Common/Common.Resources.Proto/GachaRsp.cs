using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GachaRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4701
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "FRIENDS_POINT_LACK")]
		FriendsPointLack,
		[ProtoEnum(Name = "TICKET_LACK")]
		TicketLack,
		[ProtoEnum(Name = "EQUIPMENT_FULL")]
		EquipmentFull,
		[ProtoEnum(Name = "GACHA_CLOSED")]
		GachaClosed,
		[ProtoEnum(Name = "HCOIN_LACK")]
		HcoinLack,
		[ProtoEnum(Name = "EQUIPMENT_MATERIAL_LIMIT")]
		EquipmentMaterialLimit,
		[ProtoEnum(Name = "EQUIPMENT_FRAGMENT_LIMIT")]
		EquipmentFragmentLimit,
		[ProtoEnum(Name = "MAX_TIMES_LIMIT")]
		MaxTimesLimit,
		[ProtoEnum(Name = "GACHA_FINISHED")]
		GachaFinished,
		[ProtoEnum(Name = "USE_HCOIN_FORBID")]
		UseHcoinForbid,
		[ProtoEnum(Name = "REQUIRE_SELECT")]
		RequireSelect,
		[ProtoEnum(Name = "NOT_NEWBIE")]
		NotNewbie,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed,
		[ProtoEnum(Name = "DAILY_TIMES_LIMIT")]
		DailyTimesLimit,
		[ProtoEnum(Name = "NOT_GOBACK")]
		NotGoback,
		[ProtoEnum(Name = "FREE_TYPE_MISMATCH")]
		FreeTypeMismatch
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GachaRandom;

	private bool? __pbn__IsUseFreeGacha;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "item_list")]
	public List<GachaItem> ItemLists { get; } = new List<GachaItem>();


	[ProtoMember(4, Name = "gacha_random")]
	public uint GachaRandom
	{
		get
		{
			return __pbn__GachaRandom.GetValueOrDefault();
		}
		set
		{
			__pbn__GachaRandom = value;
		}
	}

	[ProtoMember(5, Name = "display")]
	public GetGachaDisplayRsp Display { get; set; }

	[ProtoMember(6, Name = "ex_fragment")]
	public GachaExFragment ExFragment { get; set; }

	[ProtoMember(7, Name = "is_use_free_gacha")]
	public bool IsUseFreeGacha
	{
		get
		{
			return __pbn__IsUseFreeGacha.GetValueOrDefault();
		}
		set
		{
			__pbn__IsUseFreeGacha = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGachaRandom()
	{
		return __pbn__GachaRandom.HasValue;
	}

	public void ResetGachaRandom()
	{
		__pbn__GachaRandom = null;
	}

	public bool ShouldSerializeIsUseFreeGacha()
	{
		return __pbn__IsUseFreeGacha.HasValue;
	}

	public void ResetIsUseFreeGacha()
	{
		__pbn__IsUseFreeGacha = null;
	}
}
