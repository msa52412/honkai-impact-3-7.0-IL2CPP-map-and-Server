using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetRaffleActivityReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3898
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "raffle_id_list")]
	public uint[] RaffleIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
