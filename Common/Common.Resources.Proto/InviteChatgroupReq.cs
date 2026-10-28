using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class InviteChatgroupReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2202
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatgroupId;

	[ProtoMember(1, Name = "chatgroup_id")]
	public uint ChatgroupId
	{
		get
		{
			return __pbn__ChatgroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatgroupId = value;
		}
	}

	[ProtoMember(2, Name = "friend_list")]
	public uint[] FriendLists { get; set; }

	[ProtoMember(3, Name = "master_pupil_list")]
	public uint[] MasterPupilLists { get; set; }

	[ProtoMember(4, Name = "stranger_uid_list")]
	public uint[] StrangerUidLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatgroupId()
	{
		return __pbn__ChatgroupId.HasValue;
	}

	public void ResetChatgroupId()
	{
		__pbn__ChatgroupId = null;
	}
}
