using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddFriendRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 67
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "TARGET_NOT_EXIST")]
		TargetNotExist,
		[ProtoEnum(Name = "FRIEND_FULL")]
		FriendFull,
		[ProtoEnum(Name = "TARGET_FRIEND_FULL")]
		TargetFriendFull,
		[ProtoEnum(Name = "IS_SELF")]
		IsSelf,
		[ProtoEnum(Name = "IS_FRIEND")]
		IsFriend,
		[ProtoEnum(Name = "ASK_TOO_OFTEN")]
		AskTooOften,
		[ProtoEnum(Name = "TARGET_ASK_LIST_FULL")]
		TargetAskListFull,
		[ProtoEnum(Name = "TARGET_IN_ASK_LIST")]
		TargetInAskList,
		[ProtoEnum(Name = "REDIS_MULTI_ERROR")]
		RedisMultiError,
		[ProtoEnum(Name = "NOT_ALLOW_CROSS_REGION_FRIEND")]
		NotAllowCrossRegionFriend,
		[ProtoEnum(Name = "CROSS_REGION_FRIEND_FULL")]
		CrossRegionFriendFull,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "FORBIDDEN")]
		Forbidden,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private AddFriendAction? __pbn__Action;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "action")]
	[DefaultValue(AddFriendAction.AddFriendActionAsk)]
	public AddFriendAction Action
	{
		get
		{
			return __pbn__Action ?? AddFriendAction.AddFriendActionAsk;
		}
		set
		{
			__pbn__Action = value;
		}
	}

	[ProtoMember(3, Name = "target_uid_list")]
	public uint[] TargetUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAction()
	{
		return __pbn__Action.HasValue;
	}

	public void ResetAction()
	{
		__pbn__Action = null;
	}
}
