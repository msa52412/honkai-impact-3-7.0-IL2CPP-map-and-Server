using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HuntActivityHunter : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__HunterId;

	private HuntActivityHunterState? __pbn__State;

	private string __pbn__SpawnPoint;

	[ProtoMember(1, Name = "hunter_id")]
	public uint HunterId
	{
		get
		{
			return __pbn__HunterId.GetValueOrDefault();
		}
		set
		{
			__pbn__HunterId = value;
		}
	}

	[ProtoMember(2, Name = "state")]
	[DefaultValue(HuntActivityHunterState.HunterStateNone)]
	public HuntActivityHunterState State
	{
		get
		{
			return __pbn__State.GetValueOrDefault();
		}
		set
		{
			__pbn__State = value;
		}
	}

	[ProtoMember(3, Name = "spawn_point")]
	[DefaultValue("")]
	public string SpawnPoint
	{
		get
		{
			return __pbn__SpawnPoint ?? "";
		}
		set
		{
			__pbn__SpawnPoint = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeHunterId()
	{
		return __pbn__HunterId.HasValue;
	}

	public void ResetHunterId()
	{
		__pbn__HunterId = null;
	}

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}

	public bool ShouldSerializeSpawnPoint()
	{
		return __pbn__SpawnPoint != null;
	}

	public void ResetSpawnPoint()
	{
		__pbn__SpawnPoint = null;
	}
}
