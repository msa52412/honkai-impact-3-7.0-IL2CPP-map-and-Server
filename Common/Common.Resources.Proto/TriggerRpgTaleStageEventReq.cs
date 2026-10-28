using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TriggerRpgTaleStageEventReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 2317
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TriggerId;

	private uint? __pbn__TaleId;

	[ProtoMember(1, Name = "trigger_id")]
	public uint TriggerId
	{
		get
		{
			return __pbn__TriggerId.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerId = value;
		}
	}

	[ProtoMember(2, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTriggerId()
	{
		return __pbn__TriggerId.HasValue;
	}

	public void ResetTriggerId()
	{
		__pbn__TriggerId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}
}
