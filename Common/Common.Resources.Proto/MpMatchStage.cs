using System.Collections.Generic;
using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class MpMatchStage : IExtensible
{
	private IExtension __pbn__extensionData;

	private uint? __pbn__StageId;

	private bool? __pbn__HasLeaderStamina;

	private bool? __pbn__HasLeaderEnterTimes;

	private bool? __pbn__HasLeaderThemeWantedTicket;

	[ProtoMember(1, Name = "stage_id")]
	public uint StageId
	{
		get
		{
			return __pbn__StageId.GetValueOrDefault();
		}
		set
		{
			__pbn__StageId = value;
		}
	}

	[ProtoMember(2, Name = "has_leader_stamina")]
	public bool HasLeaderStamina
	{
		get
		{
			return __pbn__HasLeaderStamina.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderStamina = value;
		}
	}

	[ProtoMember(3, Name = "has_leader_enter_times")]
	public bool HasLeaderEnterTimes
	{
		get
		{
			return __pbn__HasLeaderEnterTimes.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderEnterTimes = value;
		}
	}

	[ProtoMember(4, Name = "avatar_detail")]
	public AvatarDetailData AvatarDetail { get; set; }

	[ProtoMember(5, Name = "backup_avatar_list")]
	public List<AvatarDetailData> BackupAvatarLists { get; } = new List<AvatarDetailData>();


	[ProtoMember(6, Name = "has_leader_theme_wanted_ticket")]
	public bool HasLeaderThemeWantedTicket
	{
		get
		{
			return __pbn__HasLeaderThemeWantedTicket.GetValueOrDefault();
		}
		set
		{
			__pbn__HasLeaderThemeWantedTicket = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeStageId()
	{
		return __pbn__StageId.HasValue;
	}

	public void ResetStageId()
	{
		__pbn__StageId = null;
	}

	public bool ShouldSerializeHasLeaderStamina()
	{
		return __pbn__HasLeaderStamina.HasValue;
	}

	public void ResetHasLeaderStamina()
	{
		__pbn__HasLeaderStamina = null;
	}

	public bool ShouldSerializeHasLeaderEnterTimes()
	{
		return __pbn__HasLeaderEnterTimes.HasValue;
	}

	public void ResetHasLeaderEnterTimes()
	{
		__pbn__HasLeaderEnterTimes = null;
	}

	public bool ShouldSerializeHasLeaderThemeWantedTicket()
	{
		return __pbn__HasLeaderThemeWantedTicket.HasValue;
	}

	public void ResetHasLeaderThemeWantedTicket()
	{
		__pbn__HasLeaderThemeWantedTicket = null;
	}
}
