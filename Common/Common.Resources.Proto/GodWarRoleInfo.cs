using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class GodWarRoleInfo : IExtensible
{
	private IExtension __pbn__extensionData;

	[ProtoMember(1, Name = "role_relation_list")]
	public List<GodWarRoleRelation> RoleRelationLists { get; } = new List<GodWarRoleRelation>();


	[ProtoMember(2, Name = "avatar_exp_up")]
	public GodWarAvatarExpUp AvatarExpUp { get; set; }

	[ProtoMember(3, Name = "main_avatar_id_list")]
	public uint[] MainAvatarIdLists { get; set; }

	[ProtoMember(4, Name = "support_avatar_id_list")]
	public uint[] SupportAvatarIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}
}
