using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LikeDormRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 640
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "LEVEL_LIMIT")]
		LevelLimit,
		[ProtoEnum(Name = "HAS_LIKED")]
		HasLiked,
		[ProtoEnum(Name = "SEND_LIKE_LIMIT")]
		SendLikeLimit,
		[ProtoEnum(Name = "RECEIVE_LIKE_LIMIT")]
		ReceiveLikeLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__FriendsPoint;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "target_uid")]
	public uint TargetUid
	{
		get
		{
			return __pbn__TargetUid.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetUid = value;
		}
	}

	[ProtoMember(3, Name = "friends_point")]
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

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
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
