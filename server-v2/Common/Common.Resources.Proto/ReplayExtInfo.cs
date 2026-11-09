using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayExtInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "ex_boss_info")]
	public ReplayExBossInfo ExBossInfo { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
