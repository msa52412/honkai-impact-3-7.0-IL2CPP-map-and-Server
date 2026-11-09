using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__Uid;

	private ulong? __pbn__TimeMsU64;

	private float? __pbn__ClientTime;

	private bool? __pbn__IsSystemMsg;

	[ProtoMember(1, Name = "item_list")]
	public List<ChatworldMsgItem> ItemLists { get; } = new List<ChatworldMsgItem>();


	[ProtoMember(2, Name = "uid")]
	public uint Uid
	{
		get
		{
			return __pbn__Uid.GetValueOrDefault();
		}
		set
		{
			__pbn__Uid = value;
		}
	}

	[ProtoMember(3, Name = "time_ms_u64")]
	public ulong TimeMsU64
	{
		get
		{
			return __pbn__TimeMsU64.GetValueOrDefault();
		}
		set
		{
			__pbn__TimeMsU64 = value;
		}
	}

	[ProtoMember(4, Name = "client_time")]
	public float ClientTime
	{
		get
		{
			return __pbn__ClientTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ClientTime = value;
		}
	}

	[ProtoMember(5, Name = "is_system_msg")]
	public bool IsSystemMsg
	{
		get
		{
			return __pbn__IsSystemMsg.GetValueOrDefault();
		}
		set
		{
			__pbn__IsSystemMsg = value;
		}
	}

	[ProtoMember(6, Name = "check_result")]
	public ChatMsgSensitiveCheckResult CheckResult { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeUid()
	{
		return __pbn__Uid.HasValue;
	}

	public void ResetUid()
	{
		__pbn__Uid = null;
	}

	public bool ShouldSerializeTimeMsU64()
	{
		return __pbn__TimeMsU64.HasValue;
	}

	public void ResetTimeMsU64()
	{
		__pbn__TimeMsU64 = null;
	}

	public bool ShouldSerializeClientTime()
	{
		return __pbn__ClientTime.HasValue;
	}

	public void ResetClientTime()
	{
		__pbn__ClientTime = null;
	}

	public bool ShouldSerializeIsSystemMsg()
	{
		return __pbn__IsSystemMsg.HasValue;
	}

	public void ResetIsSystemMsg()
	{
		__pbn__IsSystemMsg = null;
	}
}
