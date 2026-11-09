using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class PlayerCardMsgData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MsgIndex;

	private uint? __pbn__MsgConfig;

	[ProtoMember(1, Name = "msg_index")]
	public uint MsgIndex
	{
		get
		{
			return __pbn__MsgIndex.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgIndex = value;
		}
	}

	[ProtoMember(2, Name = "msg_config")]
	public uint MsgConfig
	{
		get
		{
			return __pbn__MsgConfig.GetValueOrDefault();
		}
		set
		{
			__pbn__MsgConfig = value;
		}
	}

	[ProtoMember(3, Name = "msg_list")]
	public List<PlayerCardMsg> MsgLists { get; } = new List<PlayerCardMsg>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMsgIndex()
	{
		return __pbn__MsgIndex.HasValue;
	}

	public void ResetMsgIndex()
	{
		__pbn__MsgIndex = null;
	}

	public bool ShouldSerializeMsgConfig()
	{
		return __pbn__MsgConfig.HasValue;
	}

	public void ResetMsgConfig()
	{
		__pbn__MsgConfig = null;
	}
}
