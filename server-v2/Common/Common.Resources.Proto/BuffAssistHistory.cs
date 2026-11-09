using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BuffAssistHistory : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__AssistType;

	private string __pbn__PeerNickname;

	private uint? __pbn__PeerAssistLevel;

	private uint? __pbn__AssistStageId;

	[ProtoMember(1, Name = "assist_type")]
	public uint AssistType
	{
		get
		{
			return __pbn__AssistType.GetValueOrDefault();
		}
		set
		{
			__pbn__AssistType = value;
		}
	}

	[ProtoMember(2, Name = "peer_nickname")]
	[DefaultValue("")]
	public string PeerNickname
	{
		get
		{
			return __pbn__PeerNickname ?? "";
		}
		set
		{
			__pbn__PeerNickname = value;
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAssistType()
	{
		return __pbn__AssistType.HasValue;
	}

	public void ResetAssistType()
	{
		__pbn__AssistType = null;
	}

	public bool ShouldSerializePeerNickname()
	{
		return __pbn__PeerNickname != null;
	}

	public void ResetPeerNickname()
	{
		__pbn__PeerNickname = null;
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
}
