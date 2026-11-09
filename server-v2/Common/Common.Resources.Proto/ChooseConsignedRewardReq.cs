using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ChooseConsignedRewardReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 521
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "optional_reward_list")]
	public uint[] OptionalRewardLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
