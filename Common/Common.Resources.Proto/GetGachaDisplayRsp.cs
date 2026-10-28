using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetGachaDisplayRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4703
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsAll;

	private GachaType? __pbn__Type;

	private uint? __pbn__PrayGachaId;

	private uint? __pbn__GachaRandom;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_all")]
	public bool IsAll
	{
		get
		{
			return __pbn__IsAll.GetValueOrDefault();
		}
		set
		{
			__pbn__IsAll = value;
		}
	}

	[ProtoMember(3, Name = "type")]
	[DefaultValue(GachaType.GachaHcoin)]
	public GachaType Type
	{
		get
		{
			return __pbn__Type ?? GachaType.GachaHcoin;
		}
		set
		{
			__pbn__Type = value;
		}
	}

	[ProtoMember(4, Name = "pray_gacha_id")]
	public uint PrayGachaId
	{
		get
		{
			return __pbn__PrayGachaId.GetValueOrDefault();
		}
		set
		{
			__pbn__PrayGachaId = value;
		}
	}

	[ProtoMember(5, Name = "gacha_random")]
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

	[ProtoMember(6, Name = "gacha_display_info_list")]
	public List<GachaDisplayInfo> GachaDisplayInfoLists { get; } = new List<GachaDisplayInfo>();


	[ProtoMember(7, Name = "gacha_ten_discount_info_list")]
	public List<GachaTenDiscountInfo> GachaTenDiscountInfoLists { get; } = new List<GachaTenDiscountInfo>();


	[ProtoMember(8, Name = "gacha_daily_times_limit_list")]
	public List<GachaDailyTimesLimit> GachaDailyTimesLimitLists { get; } = new List<GachaDailyTimesLimit>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsAll()
	{
		return __pbn__IsAll.HasValue;
	}

	public void ResetIsAll()
	{
		__pbn__IsAll = null;
	}

	public bool ShouldSerializeType()
	{
		return __pbn__Type.HasValue;
	}

	public void ResetType()
	{
		__pbn__Type = null;
	}

	public bool ShouldSerializePrayGachaId()
	{
		return __pbn__PrayGachaId.HasValue;
	}

	public void ResetPrayGachaId()
	{
		__pbn__PrayGachaId = null;
	}

	public bool ShouldSerializeGachaRandom()
	{
		return __pbn__GachaRandom.HasValue;
	}

	public void ResetGachaRandom()
	{
		__pbn__GachaRandom = null;
	}
}
