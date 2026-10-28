using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ReplayGetRecommendBriefReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 5418
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "key_list")]
	public List<ReplayRecommendBriefKey> KeyLists { get; } = new List<ReplayRecommendBriefKey>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
