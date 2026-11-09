using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBulletinActivityMissionReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4321
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "activity_id_list")]
	public uint[] ActivityIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
