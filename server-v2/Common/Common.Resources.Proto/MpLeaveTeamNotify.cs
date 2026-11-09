using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpLeaveTeamNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1101
	}

	private IExtension __pbn__extensionData;

	private int? __pbn__Reason;

	[ProtoMember(1, Name = "reason")]
	public int Reason
	{
		get
		{
			return __pbn__Reason.GetValueOrDefault();
		}
		set
		{
			__pbn__Reason = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeReason()
	{
		return __pbn__Reason.HasValue;
	}

	public void ResetReason()
	{
		__pbn__Reason = null;
	}
}
