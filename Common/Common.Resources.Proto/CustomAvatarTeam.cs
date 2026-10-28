using System.ComponentModel;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class CustomAvatarTeam : IExtensible
{
	[ProtoContract]
	public enum Enum
	{
		[ProtoEnum(Name = "MIN_TEAM_ID")]
		MinTeamId = 1,
		[ProtoEnum(Name = "MAX_TEAM_ID")]
		MaxTeamId = 10,
		[ProtoEnum(Name = "MAX_NAME_LEN")]
		MaxNameLen = 24
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__TeamId;

	private string __pbn__Name;

	[ProtoMember(1, Name = "team_id")]
	public uint TeamId
	{
		get
		{
			return __pbn__TeamId.GetValueOrDefault();
		}
		set
		{
			__pbn__TeamId = value;
		}
	}

	[ProtoMember(2, Name = "name")]
	[DefaultValue("")]
	public string Name
	{
		get
		{
			return __pbn__Name ?? "";
		}
		set
		{
			__pbn__Name = value;
		}
	}

	[ProtoMember(3, Name = "avatar_id_list")]
	public uint[] AvatarIdLists { get; set; }

	[ProtoMember(4, Name = "elf_id_list")]
	public uint[] ElfIdLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeTeamId()
	{
		return __pbn__TeamId.HasValue;
	}

	public void ResetTeamId()
	{
		__pbn__TeamId = null;
	}

	public bool ShouldSerializeName()
	{
		return __pbn__Name != null;
	}

	public void ResetName()
	{
		__pbn__Name = null;
	}
}
