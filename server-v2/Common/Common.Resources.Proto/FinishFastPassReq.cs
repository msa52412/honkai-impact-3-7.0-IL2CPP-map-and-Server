using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class FinishFastPassReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1490
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

	[ProtoMember(2, Name = "fast_pass_para_ow")]
	public FastPassParaOw FastPassParaOw { get; set; }

	[ProtoMember(3, Name = "fast_pass_para_weekday")]
	public FastPassParaWeekday FastPassParaWeekday { get; set; }

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
