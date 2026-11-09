using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScratchReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4152
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__XPos;

	private uint? __pbn__YPos;

	private uint? __pbn__ActivityId;

	[ProtoMember(1, Name = "x_pos")]
	public uint XPos
	{
		get
		{
			return __pbn__XPos.GetValueOrDefault();
		}
		set
		{
			__pbn__XPos = value;
		}
	}

	[ProtoMember(2, Name = "y_pos")]
	public uint YPos
	{
		get
		{
			return __pbn__YPos.GetValueOrDefault();
		}
		set
		{
			__pbn__YPos = value;
		}
	}

	[ProtoMember(3, Name = "activity_id")]
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

	public bool ShouldSerializeXPos()
	{
		return __pbn__XPos.HasValue;
	}

	public void ResetXPos()
	{
		__pbn__XPos = null;
	}

	public bool ShouldSerializeYPos()
	{
		return __pbn__YPos.HasValue;
	}

	public void ResetYPos()
	{
		__pbn__YPos = null;
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
