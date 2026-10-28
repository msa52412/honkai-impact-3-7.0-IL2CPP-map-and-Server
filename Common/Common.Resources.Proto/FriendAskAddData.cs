using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FriendAskAddData : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AskTime;

	[ProtoMember(1, Name = "brief")]
	public PlayerFriendBriefData Brief { get; set; }

	[ProtoMember(2, Name = "ask_time")]
	public uint AskTime
	{
		get
		{
			return __pbn__AskTime.GetValueOrDefault();
		}
		set
		{
			__pbn__AskTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAskTime()
	{
		return __pbn__AskTime.HasValue;
	}

	public void ResetAskTime()
	{
		__pbn__AskTime = null;
	}
}
