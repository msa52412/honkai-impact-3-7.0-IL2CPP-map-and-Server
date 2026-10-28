using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetMissionRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 114
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "mission_id_list")]
	public uint[] MissionIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
