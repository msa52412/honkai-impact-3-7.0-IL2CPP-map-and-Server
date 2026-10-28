using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GlobalPollingVoteReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5815
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TargetId;

	private uint? __pbn__Num;

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

	[ProtoMember(2, Name = "num")]
	public uint Num
	{
		get
		{
			return __pbn__Num.GetValueOrDefault();
		}
		set
		{
			__pbn__Num = value;
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

	public bool ShouldSerializeNum()
	{
		return __pbn__Num.HasValue;
	}

	public void ResetNum()
	{
		__pbn__Num = null;
	}
}
