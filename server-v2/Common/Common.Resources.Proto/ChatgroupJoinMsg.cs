using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatgroupJoinMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__InviterUid;

	[ProtoMember(1, Name = "inviter_uid")]
	public uint InviterUid
	{
		get
		{
			return __pbn__InviterUid.GetValueOrDefault();
		}
		set
		{
			__pbn__InviterUid = value;
		}
	}

	[ProtoMember(2, Name = "member_list")]
	public List<ChatgroupMember> MemberLists { get; } = new List<ChatgroupMember>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeInviterUid()
	{
		return __pbn__InviterUid.HasValue;
	}

	public void ResetInviterUid()
	{
		__pbn__InviterUid = null;
	}
}
