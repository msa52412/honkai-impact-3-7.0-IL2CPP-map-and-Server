using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class WishWellRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4711
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "EQUIP_NUM_ERROR")]
		EquipNumError,
		[ProtoEnum(Name = "MATERIAL_NOT_ENOUGH")]
		MaterialNotEnough,
		[ProtoEnum(Name = "WISH_TIMES_LIMIT")]
		WishTimesLimit,
		[ProtoEnum(Name = "DEVICE_ERROR")]
		DeviceError,
		[ProtoEnum(Name = "MATERIAL_ERROR")]
		MaterialError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__WishId;

	private GachaType? __pbn__GachaType;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "wish_id")]
	public uint WishId
	{
		get
		{
			return __pbn__WishId.GetValueOrDefault();
		}
		set
		{
			__pbn__WishId = value;
		}
	}

	[ProtoMember(3, Name = "reward_list")]
	public List<RewardData> RewardLists { get; } = new List<RewardData>();


	[ProtoMember(4, Name = "gacha_type")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeWishId()
	{
		return __pbn__WishId.HasValue;
	}

	public void ResetWishId()
	{
		__pbn__WishId = null;
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
