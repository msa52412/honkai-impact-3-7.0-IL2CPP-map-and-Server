using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChatMsgWorldBeast : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ChatworldId;

	private uint? __pbn__SceneId;

	private ulong? __pbn__BeastUniqueIdU64;

	private uint? __pbn__StageId;

	[ProtoMember(1, Name = "chatworld_id")]
	public uint ChatworldId
	{
		get
		{
			return __pbn__ChatworldId.GetValueOrDefault();
		}
		set
		{
			__pbn__ChatworldId = value;
		}
	}

	[ProtoMember(2, Name = "scene_id")]
	public uint SceneId
	{
		get
		{
			return __pbn__SceneId.GetValueOrDefault();
		}
		set
		{
			__pbn__SceneId = value;
		}
	}

	[ProtoMember(3, Name = "beast_unique_id_u64")]
	public ulong BeastUniqueIdU64
	{
		get
		{
			return __pbn__BeastUniqueIdU64.GetValueOrDefault();
		}
		set
		{
			__pbn__BeastUniqueIdU64 = value;
		}
	}

	[ProtoMember(4, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeChatworldId()
	{
		return __pbn__ChatworldId.HasValue;
	}

	public void ResetChatworldId()
	{
		__pbn__ChatworldId = null;
	}

	public bool ShouldSerializeSceneId()
	{
		return __pbn__SceneId.HasValue;
	}

	public void ResetSceneId()
	{
		__pbn__SceneId = null;
	}

	public bool ShouldSerializeBeastUniqueIdU64()
	{
		return __pbn__BeastUniqueIdU64.HasValue;
	}

	public void ResetBeastUniqueIdU64()
	{
		__pbn__BeastUniqueIdU64 = null;
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}
}
