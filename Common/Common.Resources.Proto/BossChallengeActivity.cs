using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class BossChallengeActivity : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "stage_list")]
	public List<BossChallengeStage> StageLists { get; } = new List<BossChallengeStage>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
