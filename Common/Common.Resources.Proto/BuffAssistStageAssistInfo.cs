using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistStageAssistInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ProducerUid;

	private uint? __pbn__PeerUid;

	private uint? __pbn__PeerAssistLevel;

	private uint? __pbn__AssistStageId;

	private uint? __pbn__BuffPoolUniqueId;

	[ProtoMember(1, Name = "producer_uid")]
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

	[ProtoMember(2, Name = "peer_uid")]
	public uint PeerUid
	{
		get
		{
			return __pbn__PeerUid.GetValueOrDefault();
		}
		set
		{
			__pbn__PeerUid = value;
		}
	}

	[ProtoMember(3, Name = "peer_assist_level")]
	public uint PeerAssistLevel
	{
		get
		{
			return __pbn__PeerAssistLevel.GetValueOrDefault();
		}
		set
		{
			__pbn__PeerAssistLevel = value;
		}
	}

	[ProtoMember(4, Name = "assist_stage_id")]
	public uint AssistStageId
	{
		get
		{
			return __pbn__AssistStageId.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistStageId = value;
		}
	}

	[ProtoMember(5, Name = "assist_word_id_list")]
	public uint[] AssistWordIdLists { get; set; }

	[ProtoMember(6, Name = "buff_slot_list")]
	public List<BuffAssistBuffSlot> BuffSlotLists { get; } = new List<BuffAssistBuffSlot>();


	[ProtoMember(7, Name = "buff_pool_unique_id")]
	public uint BuffPoolUniqueId
	{
		get
		{
			return __pbn__BuffPoolUniqueId.GetValueOrDefault();
		}
		set
		{
			__pbn__BuffPoolUniqueId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeProducerUid()
	{
		return __pbn__ProducerUid.HasValue;
	}

	public void ResetProducerUid()
	{
		__pbn__ProducerUid = null;
	}

	public bool ShouldSerializePeerUid()
	{
		return __pbn__PeerUid.HasValue;
	}

	public void ResetPeerUid()
	{
		__pbn__PeerUid = null;
	}

	public bool ShouldSerializePeerAssistLevel()
	{
		return __pbn__PeerAssistLevel.HasValue;
	}

	public void ResetPeerAssistLevel()
	{
		__pbn__PeerAssistLevel = null;
	}

	public bool ShouldSerializeAssistStageId()
	{
		return __pbn__AssistStageId.HasValue;
	}

	public void ResetAssistStageId()
	{
		__pbn__AssistStageId = null;
	}

	public bool ShouldSerializeBuffPoolUniqueId()
	{
		return __pbn__BuffPoolUniqueId.HasValue;
	}

	public void ResetBuffPoolUniqueId()
	{
		__pbn__BuffPoolUniqueId = null;
	}
}
