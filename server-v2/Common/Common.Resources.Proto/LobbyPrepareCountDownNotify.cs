using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LobbyPrepareCountDownNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1028
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__CountDownTime;

	[ProtoMember(1, Name = "count_down_time")]
	public uint CountDownTime
	{
		get
		{
			return __pbn__CountDownTime.GetValueOrDefault();
		}
		set
		{
			__pbn__CountDownTime = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeCountDownTime()
	{
		return __pbn__CountDownTime.HasValue;
	}

	public void ResetCountDownTime()
	{
		__pbn__CountDownTime = null;
	}
}
