using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetFastPassDataReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1555
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__FastPassType;

	[ProtoMember(1, Name = "fast_pass_type")]
	public uint FastPassType
	{
		get
		{
			return __pbn__FastPassType.GetValueOrDefault();
		}
		set
		{
			__pbn__FastPassType = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFastPassType()
	{
		return __pbn__FastPassType.HasValue;
	}

	public void ResetFastPassType()
	{
		__pbn__FastPassType = null;
	}
}
