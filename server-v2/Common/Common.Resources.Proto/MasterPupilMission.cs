using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MasterPupilMission : IExtensible
{
	[ProtoContract]
	public enum MissionStatus
	{
		[ProtoEnum(Name = "DOING")]
		Doing = 1,
		[ProtoEnum(Name = "FINISH")]
		Finish
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__Id;

	private uint? __pbn__Status;

	[ProtoMember(1, Name = "id")]
	public uint Id
	{
		get
		{
			return __pbn__Id.GetValueOrDefault();
		}
		set
		{
			__pbn__Id = value;
		}
	}

	[ProtoMember(2, Name = "status")]
	public uint Status
	{
		get
		{
			return __pbn__Status.GetValueOrDefault();
		}
		set
		{
			__pbn__Status = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeId()
	{
		return __pbn__Id.HasValue;
	}

	public void ResetId()
	{
		__pbn__Id = null;
	}

	public bool ShouldSerializeStatus()
	{
		return __pbn__Status.HasValue;
	}

	public void ResetStatus()
	{
		__pbn__Status = null;
	}
}
