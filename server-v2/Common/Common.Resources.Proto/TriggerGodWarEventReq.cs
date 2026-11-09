using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TriggerGodWarEventReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6161
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	private uint? __pbn__TriggerType;

	[ProtoMember(1, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
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

	[ProtoMember(3, Name = "event_id_list")]
	public uint[] EventIdLists { get; set; }

	[ProtoMember(4, Name = "trigger_type")]
	public uint TriggerType
	{
		get
		{
			return __pbn__TriggerType.GetValueOrDefault();
		}
		set
		{
			__pbn__TriggerType = value;
		}
	}

	[ProtoMember(5, Name = "trigger_param_list")]
	public uint[] TriggerParamLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}

	public bool ShouldSerializeTriggerType()
	{
		return __pbn__TriggerType.HasValue;
	}

	public void ResetTriggerType()
	{
		__pbn__TriggerType = null;
	}
}
