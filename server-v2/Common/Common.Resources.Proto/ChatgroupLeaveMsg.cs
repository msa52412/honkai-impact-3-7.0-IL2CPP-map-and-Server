using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatgroupLeaveMsg : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__OperatorUid;

	private uint? __pbn__LeaveUid;

	[ProtoMember(1, Name = "operator_uid")]
	public uint OperatorUid
	{
		get
		{
			return __pbn__OperatorUid.GetValueOrDefault();
		}
		set
		{
			__pbn__OperatorUid = value;
		}
	}

	[ProtoMember(2, Name = "leave_uid")]
	public uint LeaveUid
	{
		get
		{
			return __pbn__LeaveUid.GetValueOrDefault();
		}
		set
		{
			__pbn__LeaveUid = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOperatorUid()
	{
		return __pbn__OperatorUid.HasValue;
	}

	public void ResetOperatorUid()
	{
		__pbn__OperatorUid = null;
	}

	public bool ShouldSerializeLeaveUid()
	{
		return __pbn__LeaveUid.HasValue;
	}

	public void ResetLeaveUid()
	{
		__pbn__LeaveUid = null;
	}
}
