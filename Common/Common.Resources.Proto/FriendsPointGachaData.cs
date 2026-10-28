using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FriendsPointGachaData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__FriendsPointCost;

	[ProtoMember(2, Name = "friends_point_cost")]
	public uint FriendsPointCost
	{
		get
		{
			return __pbn__FriendsPointCost.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendsPointCost = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFriendsPointCost()
	{
		return __pbn__FriendsPointCost.HasValue;
	}

	public void ResetFriendsPointCost()
	{
		__pbn__FriendsPointCost = null;
	}
}
