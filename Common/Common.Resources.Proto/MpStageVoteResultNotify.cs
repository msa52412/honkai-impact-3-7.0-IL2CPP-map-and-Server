using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpStageVoteResultNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1032
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Option;

	[ProtoMember(1, Name = "option")]
	public uint Option
	{
		get
		{
			return __pbn__Option.GetValueOrDefault();
		}
		set
		{
			__pbn__Option = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeOption()
	{
		return __pbn__Option.HasValue;
	}

	public void ResetOption()
	{
		__pbn__Option = null;
	}
}
