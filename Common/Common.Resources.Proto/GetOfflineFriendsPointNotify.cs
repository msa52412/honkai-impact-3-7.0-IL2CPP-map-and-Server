using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetOfflineFriendsPointNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 81
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FriendsPoint;

	[ProtoMember(1, Name = "friends_point")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFriendsPoint()
	{
		return __pbn__FriendsPoint.HasValue;
	}

	public void ResetFriendsPoint()
	{
		__pbn__FriendsPoint = null;
	}
}
