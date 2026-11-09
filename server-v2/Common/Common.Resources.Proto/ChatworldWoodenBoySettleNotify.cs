using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldWoodenBoySettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2482
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsWin;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, Name = "is_win")]
	public bool IsWin
	{
		get
		{
			return __pbn__IsWin.GetValueOrDefault();
		}
		set
		{
			__pbn__IsWin = value;
		}
	}

	[ProtoMember(2, Name = "activity_id")]
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsWin()
	{
		return __pbn__IsWin.HasValue;
	}

	public void ResetIsWin()
	{
		__pbn__IsWin = null;
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}
}
