using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldMsgAction : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActionId;

	private uint? __pbn__TargetUid;

	[ProtoMember(1, Name = "action_id")]
	public uint ActionId
	{
		get
		{
			return __pbn__ActionId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActionId = value;
		}
	}

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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActionId()
	{
		return __pbn__ActionId.HasValue;
	}

	public void ResetActionId()
	{
		__pbn__ActionId = null;
	}

	public bool ShouldSerializeTargetUid()
	{
		return __pbn__TargetUid.HasValue;
	}

	public void ResetTargetUid()
	{
		__pbn__TargetUid = null;
	}
}
