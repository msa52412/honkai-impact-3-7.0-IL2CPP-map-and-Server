using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncTimeReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 803
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Seq;

	[ProtoMember(1, Name = "seq")]
	public uint Seq
	{
		get
		{
			return __pbn__Seq.GetValueOrDefault();
		}
		set
		{
			__pbn__Seq = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeSeq()
	{
		return __pbn__Seq.HasValue;
	}

	public void ResetSeq()
	{
		__pbn__Seq = null;
	}
}
