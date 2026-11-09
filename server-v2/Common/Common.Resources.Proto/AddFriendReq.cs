using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AddFriendReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 66
	}

	private IExtension __pbn__extensionData;

	private AddFriendAction? __pbn__Action;

	[ProtoMember(1, Name = "action")]
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

	[ProtoMember(2, Name = "target_uid_list")]
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
