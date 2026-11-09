using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SyncMatrixCurEventNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2334
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private uint? __pbn__RealEventParam;

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

	[ProtoMember(3, Name = "real_event_param")]
	public uint RealEventParam
	{
		get
		{
			return __pbn__RealEventParam.GetValueOrDefault();
		}
		set
		{
			__pbn__RealEventParam = value;
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

	public bool ShouldSerializeRealEventParam()
	{
		return __pbn__RealEventParam.HasValue;
	}

	public void ResetRealEventParam()
	{
		__pbn__RealEventParam = null;
	}
}
