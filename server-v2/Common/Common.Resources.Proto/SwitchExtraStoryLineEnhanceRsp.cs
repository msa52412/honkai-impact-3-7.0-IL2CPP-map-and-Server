using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchExtraStoryLineEnhanceRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1040
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail
	}

	private IExtension __pbn__extensionData;

	private bool? __pbn__IsOpenEnhance;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_open_enhance")]
	public bool IsOpenEnhance
	{
		get
		{
			return __pbn__IsOpenEnhance.GetValueOrDefault();
		}
		set
		{
			__pbn__IsOpenEnhance = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsOpenEnhance()
	{
		return __pbn__IsOpenEnhance.HasValue;
	}

	public void ResetIsOpenEnhance()
	{
		__pbn__IsOpenEnhance = null;
	}
}
