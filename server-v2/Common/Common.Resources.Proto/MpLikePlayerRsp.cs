using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpLikePlayerRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 393
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_IN_TEAM")]
		NotInTeam,
		[ProtoEnum(Name = "HAS_LIKE")]
		HasLike
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetUid;

	private uint? __pbn__FromUid;

	private uint? __pbn__TargetLikeNum;

	private uint? __pbn__AddFriendsPoint;

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

	[ProtoMember(3, Name = "from_uid")]
	public uint FromUid
	{
		get
		{
			return __pbn__FromUid.GetValueOrDefault();
		}
		set
		{
			__pbn__FromUid = value;
		}
	}

	[ProtoMember(4, Name = "target_like_num")]
	public uint TargetLikeNum
	{
		get
		{
			return __pbn__TargetLikeNum.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetLikeNum = value;
		}
	}

	[ProtoMember(5, Name = "add_friends_point")]
	public uint AddFriendsPoint
	{
		get
		{
			return __pbn__AddFriendsPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__AddFriendsPoint = value;
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

	public bool ShouldSerializeFromUid()
	{
		return __pbn__FromUid.HasValue;
	}

	public void ResetFromUid()
	{
		__pbn__FromUid = null;
	}

	public bool ShouldSerializeTargetLikeNum()
	{
		return __pbn__TargetLikeNum.HasValue;
	}

	public void ResetTargetLikeNum()
	{
		__pbn__TargetLikeNum = null;
	}

	public bool ShouldSerializeAddFriendsPoint()
	{
		return __pbn__AddFriendsPoint.HasValue;
	}

	public void ResetAddFriendsPoint()
	{
		__pbn__AddFriendsPoint = null;
	}
}
