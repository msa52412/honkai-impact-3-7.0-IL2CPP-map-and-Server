using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MatrixTriggerEventReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2330
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	[ProtoMember(1, Name = "pos")]
	public MatrixGridPos Pos { get; set; }

	[ProtoMember(2, Name = "event_id")]
	public uint EventId
	{
		get
		{
			return __pbn__EventId.GetValueOrDefault();
		}
		set
		{
			__pbn__EventId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeEventId()
	{
		return __pbn__EventId.HasValue;
	}

	public void ResetEventId()
	{
		__pbn__EventId = null;
	}
}
