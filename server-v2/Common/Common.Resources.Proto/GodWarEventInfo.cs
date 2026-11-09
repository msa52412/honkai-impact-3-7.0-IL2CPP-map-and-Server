using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarEventInfo : IExtensible
{
	[ProtoContract]
	public class ParamMap : IExtensible
	{
		private IExtension __pbn__extensionData;

		private uint? __pbn__Key;

		private uint? __pbn__Value;

		[ProtoMember(1, Name = "key")]
		public uint Key
		{
			get
			{
				return __pbn__Key.GetValueOrDefault();
			}
			set
			{
				__pbn__Key = value;
			}
		}

		[ProtoMember(2, Name = "value")]
		public uint Value
		{
			get
			{
				return __pbn__Value.GetValueOrDefault();
			}
			set
			{
				__pbn__Value = value;
			}
		}

		IExtension IExtensible.GetExtensionObject(bool createIfMissing)
		{
			return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
		}

		public bool ShouldSerializeKey()
		{
			return __pbn__Key.HasValue;
		}

		public void ResetKey()
		{
			__pbn__Key = null;
		}

		public bool ShouldSerializeValue()
		{
			return __pbn__Value.HasValue;
		}

		public void ResetValue()
		{
			__pbn__Value = null;
		}
	}

	[ProtoContract]
	public enum EventSettleStatus
	{
		[ProtoEnum(Name = "EFFECT")]
		Effect,
		[ProtoEnum(Name = "FLAG_NOT_VALID")]
		FlagNotValid,
		[ProtoEnum(Name = "SETTLE_FAIL")]
		SettleFail,
		[ProtoEnum(Name = "SETTLE_REPEATED")]
		SettleRepeated,
		[ProtoEnum(Name = "TALE_NOT_FOUND")]
		TaleNotFound,
		[ProtoEnum(Name = "TRIGGER_PARAM_NOT_VALID")]
		TriggerParamNotValid,
		[ProtoEnum(Name = "CANDIDATE_EMPTY")]
		CandidateEmpty,
		[ProtoEnum(Name = "FORCE_SETTLE")]
		ForceSettle
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__EventId;

	private uint? __pbn__EventType;

	private EventSettleStatus? __pbn__event_settle_status;

	private uint? __pbn__SourceEventId;

	private uint? __pbn__Fingerprint;

	private uint? __pbn__SourceType;

	[ProtoMember(1, Name = "event_id")]
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

	[ProtoMember(2, Name = "event_type")]
	public uint EventType
	{
		get
		{
			return __pbn__EventType.GetValueOrDefault();
		}
		set
		{
			__pbn__EventType = value;
		}
	}

	[ProtoMember(3)]
	[DefaultValue(EventSettleStatus.Effect)]
	public EventSettleStatus event_settle_status
	{
		get
		{
			return __pbn__event_settle_status.GetValueOrDefault();
		}
		set
		{
			__pbn__event_settle_status = value;
		}
	}

	[ProtoMember(4, Name = "event_param_list")]
	public uint[] EventParamLists { get; set; }

	[ProtoMember(5, Name = "source_event_id")]
	public uint SourceEventId
	{
		get
		{
			return __pbn__SourceEventId.GetValueOrDefault();
		}
		set
		{
			__pbn__SourceEventId = value;
		}
	}

	[ProtoMember(6, Name = "reward_data_list")]
	public List<RewardData> RewardDataLists { get; } = new List<RewardData>();


	[ProtoMember(7, Name = "fingerprint")]
	public uint Fingerprint
	{
		get
		{
			return __pbn__Fingerprint.GetValueOrDefault();
		}
		set
		{
			__pbn__Fingerprint = value;
		}
	}

	[ProtoMember(8, Name = "extra_param_list")]
	public uint[] ExtraParamLists { get; set; }

	[ProtoMember(9, Name = "source_type")]
	public uint SourceType
	{
		get
		{
			return __pbn__SourceType.GetValueOrDefault();
		}
		set
		{
			__pbn__SourceType = value;
		}
	}

	[ProtoMember(10, Name = "event_param_map_list")]
	public List<ParamMap> EventParamMapLists { get; } = new List<ParamMap>();


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

	public bool ShouldSerializeEventType()
	{
		return __pbn__EventType.HasValue;
	}

	public void ResetEventType()
	{
		__pbn__EventType = null;
	}

	public bool ShouldSerializeevent_settle_status()
	{
		return __pbn__event_settle_status.HasValue;
	}

	public void Resetevent_settle_status()
	{
		__pbn__event_settle_status = null;
	}

	public bool ShouldSerializeSourceEventId()
	{
		return __pbn__SourceEventId.HasValue;
	}

	public void ResetSourceEventId()
	{
		__pbn__SourceEventId = null;
	}

	public bool ShouldSerializeFingerprint()
	{
		return __pbn__Fingerprint.HasValue;
	}

	public void ResetFingerprint()
	{
		__pbn__Fingerprint = null;
	}

	public bool ShouldSerializeSourceType()
	{
		return __pbn__SourceType.HasValue;
	}

	public void ResetSourceType()
	{
		__pbn__SourceType = null;
	}
}
