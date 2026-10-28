using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatworldActivityInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__TreasureScheduleId;

	private bool? __pbn__IsHasNpcRedEnvelope;

	[ProtoMember(1, Name = "treasure_schedule_id")]
	public uint TreasureScheduleId
	{
		get
		{
			return __pbn__TreasureScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TreasureScheduleId = value;
		}
	}

	[ProtoMember(2, Name = "is_has_npc_red_envelope")]
	public bool IsHasNpcRedEnvelope
	{
		get
		{
			return __pbn__IsHasNpcRedEnvelope.GetValueOrDefault();
		}
		set
		{
			__pbn__IsHasNpcRedEnvelope = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTreasureScheduleId()
	{
		return __pbn__TreasureScheduleId.HasValue;
	}

	public void ResetTreasureScheduleId()
	{
		__pbn__TreasureScheduleId = null;
	}

	public bool ShouldSerializeIsHasNpcRedEnvelope()
	{
		return __pbn__IsHasNpcRedEnvelope.HasValue;
	}

	public void ResetIsHasNpcRedEnvelope()
	{
		__pbn__IsHasNpcRedEnvelope = null;
	}
}
