using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class OpenworldPeriodSettleNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 4450
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "settle_map_list")]
	public List<OpenworldPeriodSettleData> SettleMapLists { get; } = new List<OpenworldPeriodSettleData>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
