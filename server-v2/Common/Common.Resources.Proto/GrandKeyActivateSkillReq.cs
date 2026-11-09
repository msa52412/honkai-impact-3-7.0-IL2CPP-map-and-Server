using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GrandKeyActivateSkillReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 759
	}

	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "key_list")]
	public List<GrandKeySkill> KeyLists { get; } = new List<GrandKeySkill>();


	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
