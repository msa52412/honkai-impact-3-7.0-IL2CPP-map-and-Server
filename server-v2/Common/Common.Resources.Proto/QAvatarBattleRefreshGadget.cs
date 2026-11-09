using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QAvatarBattleRefreshGadget : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__GadgetId;

	private uint? __pbn__SpawnPointId;

	[ProtoMember(1, Name = "gadget_id")]
	public uint GadgetId
	{
		get
		{
			return __pbn__GadgetId.GetValueOrDefault();
		}
		set
		{
			__pbn__GadgetId = value;
		}
	}

	[ProtoMember(2, Name = "spawn_point_id")]
	public uint SpawnPointId
	{
		get
		{
			return __pbn__SpawnPointId.GetValueOrDefault();
		}
		set
		{
			__pbn__SpawnPointId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGadgetId()
	{
		return __pbn__GadgetId.HasValue;
	}

	public void ResetGadgetId()
	{
		__pbn__GadgetId = null;
	}

	public bool ShouldSerializeSpawnPointId()
	{
		return __pbn__SpawnPointId.HasValue;
	}

	public void ResetSpawnPointId()
	{
		__pbn__SpawnPointId = null;
	}
}
