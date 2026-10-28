using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalPollingSupportReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5813
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetId;

	[ProtoMember(1, Name = "target_id")]
	public uint TargetId
	{
		get
		{
			return __pbn__TargetId.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetId()
	{
		return __pbn__TargetId.HasValue;
	}

	public void ResetTargetId()
	{
		__pbn__TargetId = null;
	}
}
