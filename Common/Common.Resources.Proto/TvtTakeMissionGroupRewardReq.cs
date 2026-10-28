using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class TvtTakeMissionGroupRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3305
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__MissionGroupId;

	[ProtoMember(1, Name = "mission_group_id")]
	public uint MissionGroupId
	{
		get
		{
			return __pbn__MissionGroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__MissionGroupId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeMissionGroupId()
	{
		return __pbn__MissionGroupId.HasValue;
	}

	public void ResetMissionGroupId()
	{
		__pbn__MissionGroupId = null;
	}
}
