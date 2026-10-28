using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FriendBondNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 394
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FriendUid;

	private uint? __pbn__Bond;

	[ProtoMember(1, Name = "friend_uid")]
	public uint FriendUid
	{
		get
		{
			return __pbn__FriendUid.GetValueOrDefault();
		}
		set
		{
			__pbn__FriendUid = value;
		}
	}

	[ProtoMember(2, Name = "bond")]
	public uint Bond
	{
		get
		{
			return __pbn__Bond.GetValueOrDefault();
		}
		set
		{
			__pbn__Bond = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFriendUid()
	{
		return __pbn__FriendUid.HasValue;
	}

	public void ResetFriendUid()
	{
		__pbn__FriendUid = null;
	}

	public bool ShouldSerializeBond()
	{
		return __pbn__Bond.HasValue;
	}

	public void ResetBond()
	{
		__pbn__Bond = null;
	}
}
