using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BwWorldCampActivityAddAssistContributionNotify : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 7681
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "wait_take_assist_contribution")]
	public uint WaitTakeAssistContribution { get; set; }

	[ProtoMember(2, Name = "total_assist_contribution")]
	public uint TotalAssistContribution { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
