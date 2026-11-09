using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReportTvtBattleStageMessageNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3355
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "message_list")]
	public List<TvtBattleStageMessage> MessageLists { get; } = new List<TvtBattleStageMessage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
