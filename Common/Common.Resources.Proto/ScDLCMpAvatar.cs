using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class ScDLCMpAvatar : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__ScDlcAvatarId;

	[ProtoMember(1, Name = "sc_dlc_avatar_id")]
	public uint ScDlcAvatarId
	{
		get
		{
			return __pbn__ScDlcAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__ScDlcAvatarId = value;
		}
	}

	[ProtoMember(2, Name = "talent_list")]
	public List<ScDLCMpTalent> TalentLists { get; } = new List<ScDLCMpTalent>();


	[ProtoMember(3, Name = "equip_talent_list")]
	public uint[] EquipTalentLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeScDlcAvatarId()
	{
		return __pbn__ScDlcAvatarId.HasValue;
	}

	public void ResetScDlcAvatarId()
	{
		__pbn__ScDlcAvatarId = null;
	}
}
