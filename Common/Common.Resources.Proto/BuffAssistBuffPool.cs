using System.Collections.Generic;
using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistBuffPool : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__PoolId;

	private uint? __pbn__UniqueId;

	private uint? __pbn__ProducerUid;

	private string __pbn__ProducerNickname;

	private uint? __pbn__ExpireTime;

	private uint? __pbn__BuffScheduleId;

	private uint? __pbn__ProducerType;

	private uint? __pbn__ProducerHeadId;

	private uint? __pbn__ProducerAssistLevel;

	[ProtoMember(1, Name = "pool_id")]
	public uint PoolId
	{
		get
		{
			return __pbn__PoolId.GetValueOrDefault();
		}
		set
		{
			__pbn__PoolId = value;
		}
	}

	[ProtoMember(2, Name = "unique_id")]
	public uint UniqueId
	{
		get
		{
			return __pbn__UniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__UniqueId = value;
		}
	}

	[ProtoMember(3, Name = "producer_uid")]
	public uint ProducerUid
	{
		get
		{
			return __pbn__ProducerUid.GetValueOrDefault();
		}
		set
		{
			__pbn__ProducerUid = value;
		}
	}

	[ProtoMember(4, Name = "producer_nickname")]
	[DefaultValue("")]
	public string ProducerNickname
	{
		get
		{
			return __pbn__ProducerNickname ?? "";
		}
		set
		{
			__pbn__ProducerNickname = value;
		}
	}

	[ProtoMember(5, Name = "buff_slot_list")]
	public List<BuffAssistBuffSlot> BuffSlotLists { get; } = new List<BuffAssistBuffSlot>();


	[ProtoMember(6, Name = "expire_time")]
	public uint ExpireTime
	{
		get
		{
			return __pbn__ExpireTime.GetValueOrDefault();
		}
		set
		{
			__pbn__ExpireTime = value;
		}
	}

	[ProtoMember(7, Name = "assist_word_id_list")]
	public uint[] AssistWordIdLists { get; set; }

	[ProtoMember(8, Name = "buff_schedule_id")]
	public uint BuffScheduleId
	{
		get
		{
			return __pbn__BuffScheduleId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffScheduleId = value;
		}
	}

	[ProtoMember(9, Name = "producer_type")]
	public uint ProducerType
	{
		get
		{
			return __pbn__ProducerType.GetValueOrDefault();
		}
		set
		{
			__pbn__ProducerType = value;
		}
	}

	[ProtoMember(10, Name = "producer_head_id")]
	public uint ProducerHeadId
	{
		get
		{
			return __pbn__ProducerHeadId.GetValueOrDefault();
		}
		set
		{
			__pbn__ProducerHeadId = value;
		}
	}

	[ProtoMember(11, Name = "producer_assist_level")]
	public uint ProducerAssistLevel
	{
		get
		{
			return __pbn__ProducerAssistLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__ProducerAssistLevel = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializePoolId()
	{
		return __pbn__PoolId.HasValue;
	}

	public void ResetPoolId()
	{
		__pbn__PoolId = null;
	}

	public bool ShouldSerializeUniqueId()
	{
		return __pbn__UniqueId.HasValue;
	}

	public void ResetUniqueId()
	{
		__pbn__UniqueId = null;
	}

	public bool ShouldSerializeProducerUid()
	{
		return __pbn__ProducerUid.HasValue;
	}

	public void ResetProducerUid()
	{
		__pbn__ProducerUid = null;
	}

	public bool ShouldSerializeProducerNickname()
	{
		return __pbn__ProducerNickname != null;
	}

	public void ResetProducerNickname()
	{
		__pbn__ProducerNickname = null;
	}

	public bool ShouldSerializeExpireTime()
	{
		return __pbn__ExpireTime.HasValue;
	}

	public void ResetExpireTime()
	{
		__pbn__ExpireTime = null;
	}

	public bool ShouldSerializeBuffScheduleId()
	{
		return __pbn__BuffScheduleId.HasValue;
	}

	public void ResetBuffScheduleId()
	{
		__pbn__BuffScheduleId = null;
	}

	public bool ShouldSerializeProducerType()
	{
		return __pbn__ProducerType.HasValue;
	}

	public void ResetProducerType()
	{
		__pbn__ProducerType = null;
	}

	public bool ShouldSerializeProducerHeadId()
	{
		return __pbn__ProducerHeadId.HasValue;
	}

	public void ResetProducerHeadId()
	{
		__pbn__ProducerHeadId = null;
	}

	public bool ShouldSerializeProducerAssistLevel()
	{
		return __pbn__ProducerAssistLevel.HasValue;
	}

	public void ResetProducerAssistLevel()
	{
		__pbn__ProducerAssistLevel = null;
	}
}
