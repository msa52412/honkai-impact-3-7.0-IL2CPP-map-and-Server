using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GetBuffEffectReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 476
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "effect_id_list")]
	public uint[] EffectIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
