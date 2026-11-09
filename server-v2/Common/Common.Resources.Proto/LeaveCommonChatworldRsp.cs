using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class LeaveCommonChatworldRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2403
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

	private bool? __pbn__IsKicked;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "is_kicked")]
	public bool IsKicked
	{
		get
		{
			return __pbn__IsKicked.GetValueOrDefault();
		}
		set
		{
			__pbn__IsKicked = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeIsKicked()
	{
		return __pbn__IsKicked.HasValue;
	}

	public void ResetIsKicked()
	{
		__pbn__IsKicked = null;
	}
}
