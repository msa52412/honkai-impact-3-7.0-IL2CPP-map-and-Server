using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class DelMissionNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 116
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionId;

	[ProtoMember(1, Name = "mission_id")]
	public uint MissionId
	{
		get
		{
			return __pbn__MissionId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMissionId()
	{
		return __pbn__MissionId.HasValue;
	}

	public void ResetMissionId()
	{
		__pbn__MissionId = null;
	}
}
