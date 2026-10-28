using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteChatgroupRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2203
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "NOT_CREATER")]
		NotCreater,
		[ProtoEnum(Name = "EXCEED_MAX_MEMBER_NUM")]
		ExceedMaxMemberNum,
		[ProtoEnum(Name = "GROUP_NOT_FOUND")]
		GroupNotFound,
		[ProtoEnum(Name = "NOT_IN_GROUP")]
		NotInGroup,
		[ProtoEnum(Name = "UID_NOT_EXIST")]
		UidNotExist,
		[ProtoEnum(Name = "FRIEND_ERROR")]
		FriendError,
		[ProtoEnum(Name = "MASTER_PUPIL_ERROR")]
		MasterPupilError,
		[ProtoEnum(Name = "FEATURE_LIMIT")]
		FeatureLimit,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "succ_uid_list")]
	public uint[] SuccUidLists { get; set; }

	[ProtoMember(3, Name = "succ_stranger_uid_list")]
	public uint[] SuccStrangerUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
