using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class HuntActivityMachine : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__MachineId;

	private uint? __pbn__State;

	[ProtoMember(1, Name = "machine_id")]
	public uint MachineId
	{
		get
		{
			return __pbn__MachineId.GetValueOrDefault();
		}
		set
		{
			__pbn__MachineId = value;
		}
	}

	[ProtoMember(2, Name = "state")]
	public uint State
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

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMachineId()
	{
		return __pbn__MachineId.HasValue;
	}

	public void ResetMachineId()
	{
		__pbn__MachineId = null;
	}

	public bool ShouldSerializeState()
	{
		return __pbn__State.HasValue;
	}

	public void ResetState()
	{
		__pbn__State = null;
	}
}
