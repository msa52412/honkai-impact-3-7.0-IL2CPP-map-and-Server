using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SwitchDynamicHardLvRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 240
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

	private bool? __pbn__IsDynamicHardLv;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_dynamic_hard_lv")]
	public bool IsDynamicHardLv
	{
		get
		{
			return __pbn__IsDynamicHardLv.GetValueOrDefault();
		}
		set
		{
			__pbn__IsDynamicHardLv = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsDynamicHardLv()
	{
		return __pbn__IsDynamicHardLv.HasValue;
	}

	public void ResetIsDynamicHardLv()
	{
		__pbn__IsDynamicHardLv = null;
	}
}
