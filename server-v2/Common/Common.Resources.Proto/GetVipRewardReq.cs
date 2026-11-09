using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetVipRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6719
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "vip_level_list")]
	public uint[] VipLevelLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
