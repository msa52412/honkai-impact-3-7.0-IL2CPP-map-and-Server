using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OverlapActivityMoveReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4221
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__FromGridId;

	private uint? __pbn__ToGridId;

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

	[ProtoMember(2, Name = "from_grid_id")]
	public uint FromGridId
	{
		get
		{
			return __pbn__FromGridId.GetValueOrDefault();
		}
		set
		{
			__pbn__FromGridId = value;
		}
	}

	[ProtoMember(3, Name = "to_grid_id")]
	public uint ToGridId
	{
		get
		{
			return __pbn__ToGridId.GetValueOrDefault();
		}
		set
		{
			__pbn__ToGridId = value;
		}
	}

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

	public bool ShouldSerializeFromGridId()
	{
		return __pbn__FromGridId.HasValue;
	}

	public void ResetFromGridId()
	{
		__pbn__FromGridId = null;
	}

	public bool ShouldSerializeToGridId()
	{
		return __pbn__ToGridId.HasValue;
	}

	public void ResetToGridId()
	{
		__pbn__ToGridId = null;
	}
}
