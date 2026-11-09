using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldWoodenBoy : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private ChatworldWoodenBoyStatus? __pbn__Status;

	private uint? __pbn__StatusEndTime;

	private bool? __pbn__IsNpcWin;

	[ProtoMember(1, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(2, Name = "status")]
	[DefaultValue(ChatworldWoodenBoyStatus.ChatworldWoodenBoyNone)]
	public ChatworldWoodenBoyStatus Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	[ProtoMember(3, Name = "status_end_time")]
	public uint StatusEndTime
	{
		get
		{
			return __pbn__StatusEndTime.GetValueOrDefault();
		}
		set
		{
			__pbn__StatusEndTime = value;
		}
	}

	[ProtoMember(4, Name = "is_npc_win")]
	public bool IsNpcWin
	{
		get
		{
			return __pbn__IsNpcWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsNpcWin = value;
		}
	}

	[ProtoMember(5, Name = "player_list")]
	public List<ChatworldWoodenBoyPlayer> PlayerLists { get; } = new List<ChatworldWoodenBoyPlayer>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}

	public bool ShouldSerializeStatusEndTime()
	{
		return __pbn__StatusEndTime.HasValue;
	}

	public void ResetStatusEndTime()
	{
		__pbn__StatusEndTime = null;
	}

	public bool ShouldSerializeIsNpcWin()
	{
		return __pbn__IsNpcWin.HasValue;
	}

	public void ResetIsNpcWin()
	{
		__pbn__IsNpcWin = null;
	}
}
