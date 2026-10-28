using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class RewardData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Exp;

	private uint? __pbn__Scoin;

	private uint? __pbn__Hcoin;

	private uint? __pbn__Stamina;

	private uint? __pbn__FriendsPoint;

	private uint? __pbn__ExpConvertScoin;

	[ProtoMember(1, Name = "exp")]
	public uint Exp
	{
		get
		{
			return __pbn__Exp.GetValueOrDefault();
		}
		set
		{
			__pbn__Exp = value;
		}
	}

	[ProtoMember(2, Name = "scoin")]
	public uint Scoin
	{
		get
		{
			return __pbn__Scoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Scoin = value;
		}
	}

	[ProtoMember(3, Name = "hcoin")]
	public uint Hcoin
	{
		get
		{
			return __pbn__Hcoin.GetValueOrDefault();
		}
		set
		{
			__pbn__Hcoin = value;
		}
	}

	[ProtoMember(4, Name = "stamina")]
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

	[ProtoMember(6, Name = "friends_point")]
	public uint FriendsPoint
	{
		get
		{
			return __pbn__FriendsPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendsPoint = value;
		}
	}

	[ProtoMember(7, Name = "item_list")]
	public List<RewardItemData> ItemLists { get; } = new List<RewardItemData>();


	[ProtoMember(9, Name = "exp_convert_scoin")]
	public uint ExpConvertScoin
	{
		get
		{
			return __pbn__ExpConvertScoin.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpConvertScoin = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeExp()
	{
		return __pbn__Exp.HasValue;
	}

	public void ResetExp()
	{
		__pbn__Exp = null;
	}

	public bool ShouldSerializeScoin()
	{
		return __pbn__Scoin.HasValue;
	}

	public void ResetScoin()
	{
		__pbn__Scoin = null;
	}

	public bool ShouldSerializeHcoin()
	{
		return __pbn__Hcoin.HasValue;
	}

	public void ResetHcoin()
	{
		__pbn__Hcoin = null;
	}

	public bool ShouldSerializeStamina()
	{
		return __pbn__Stamina.HasValue;
	}

	public void ResetStamina()
	{
		__pbn__Stamina = null;
	}

	public bool ShouldSerializeFriendsPoint()
	{
		return __pbn__FriendsPoint.HasValue;
	}

	public void ResetFriendsPoint()
	{
		__pbn__FriendsPoint = null;
	}

	public bool ShouldSerializeExpConvertScoin()
	{
		return __pbn__ExpConvertScoin.HasValue;
	}

	public void ResetExpConvertScoin()
	{
		__pbn__ExpConvertScoin = null;
	}
}
