using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class KingdomWarVoteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3054
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetPoint;

	[ProtoMember(1, Name = "target_point")]
	public uint TargetPoint
	{
		get
		{
			return __pbn__TargetPoint.GetValueOrDefault();
		}
		set
		{
			__pbn__TargetPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTargetPoint()
	{
		return __pbn__TargetPoint.HasValue;
	}

	public void ResetTargetPoint()
	{
		__pbn__TargetPoint = null;
	}
}
