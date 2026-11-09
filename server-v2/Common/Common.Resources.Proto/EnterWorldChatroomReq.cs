using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class EnterWorldChatroomReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2231
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatroomId;

	private ActivityWorldChatroomType? __pbn__ActivityType;

	private uint? __pbn__Para;

	private uint? __pbn__PlayerLevel;

	private string __pbn__PlayerRegion;

	[ProtoMember(1, Name = "chatroom_id")]
	public uint ChatroomId
	{
		get
		{
			return __pbn__ChatroomId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatroomId = value;
		}
	}

	[ProtoMember(2, Name = "activity_type")]
	[DefaultValue(ActivityWorldChatroomType.ActivityWorldChatroomTypeNone)]
	public ActivityWorldChatroomType ActivityType
	{
		get
		{
			return __pbn__ActivityType.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityType = value;
		}
	}

	[ProtoMember(3, Name = "para")]
	public uint Para
	{
		get
		{
			return __pbn__Para.GetValueOrDefault();
		}
		set
		{
			__pbn__Para = value;
		}
	}

	[ProtoMember(4, Name = "player_level")]
	public uint PlayerLevel
	{
		get
		{
			return __pbn__PlayerLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PlayerLevel = value;
		}
	}

	[ProtoMember(5, Name = "player_region")]
	[DefaultValue("")]
	public string PlayerRegion
	{
		get
		{
			return __pbn__PlayerRegion ?? "";
		}
		set
		{
			__pbn__PlayerRegion = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatroomId()
	{
		return __pbn__ChatroomId.HasValue;
	}

	public void ResetChatroomId()
	{
		__pbn__ChatroomId = null;
	}

	public bool ShouldSerializeActivityType()
	{
		return __pbn__ActivityType.HasValue;
	}

	public void ResetActivityType()
	{
		__pbn__ActivityType = null;
	}

	public bool ShouldSerializePara()
	{
		return __pbn__Para.HasValue;
	}

	public void ResetPara()
	{
		__pbn__Para = null;
	}

	public bool ShouldSerializePlayerLevel()
	{
		return __pbn__PlayerLevel.HasValue;
	}

	public void ResetPlayerLevel()
	{
		__pbn__PlayerLevel = null;
	}

	public bool ShouldSerializePlayerRegion()
	{
		return __pbn__PlayerRegion != null;
	}

	public void ResetPlayerRegion()
	{
		__pbn__PlayerRegion = null;
	}
}
