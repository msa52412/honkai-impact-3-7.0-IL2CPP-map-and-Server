using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TakeGobackFundRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1653
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ProgressId;

	[ProtoMember(1, Name = "progress_id")]
	public uint ProgressId
	{
		get
		{
			return __pbn__ProgressId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProgressId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProgressId()
	{
		return __pbn__ProgressId.HasValue;
	}

	public void ResetProgressId()
	{
		__pbn__ProgressId = null;
	}
}
