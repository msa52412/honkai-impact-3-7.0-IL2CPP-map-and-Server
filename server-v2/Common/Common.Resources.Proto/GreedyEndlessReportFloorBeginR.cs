using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GreedyEndlessReportFloorBeginReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3464
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Floor;

	[ProtoMember(1, Name = "floor")]
	public uint Floor
	{
		get
		{
			return __pbn__Floor.GetValueOrDefault();
		}
		set
		{
			__pbn__Floor = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeFloor()
	{
		return __pbn__Floor.HasValue;
	}

	public void ResetFloor()
	{
		__pbn__Floor = null;
	}
}
[ProtoContract]
public class GreedyEndlessReportFloorBeginRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3465
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

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
