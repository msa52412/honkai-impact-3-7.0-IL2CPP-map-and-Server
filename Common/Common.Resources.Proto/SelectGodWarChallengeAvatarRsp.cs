using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SelectGodWarChallengeAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6153
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "ALREADY_SELECTED")]
		AlreadySelected,
		[ProtoEnum(Name = "AVATAR_ERROR")]
		AvatarError,
		[ProtoEnum(Name = "SUPPORT_AVATAR_ERROR")]
		SupportAvatarError,
		[ProtoEnum(Name = "ELF_ERROR")]
		ElfError,
		[ProtoEnum(Name = "AVATAR_SCHEDULE_ERROR")]
		AvatarScheduleError,
		[ProtoEnum(Name = "PUNISH_BUFF_ERROR")]
		PunishBuffError,
		[ProtoEnum(Name = "FEATURE_CLOSE")]
		FeatureClose,
		[ProtoEnum(Name = "FORCE_SETTLE")]
		ForceSettle,
		[ProtoEnum(Name = "NOT_OPEN")]
		NotOpen,
		[ProtoEnum(Name = "BOON_BUFF_ERROR")]
		BoonBuffError,
		[ProtoEnum(Name = "AVATAR_MISSION_NOT_FINISH")]
		AvatarMissionNotFinish,
		[ProtoEnum(Name = "EXTRA_ITEM_ERROR")]
		ExtraItemError
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GodWarId;

	private uint? __pbn__TaleId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "god_war_id")]
	public uint GodWarId
	{
		get
		{
			return __pbn__GodWarId.GetValueOrDefault();
		}
		set
		{
			__pbn__GodWarId = value;
		}
	}

	[ProtoMember(3, Name = "tale_id")]
	public uint TaleId
	{
		get
		{
			return __pbn__TaleId.GetValueOrDefault();
		}
		set
		{
			__pbn__TaleId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGodWarId()
	{
		return __pbn__GodWarId.HasValue;
	}

	public void ResetGodWarId()
	{
		__pbn__GodWarId = null;
	}

	public bool ShouldSerializeTaleId()
	{
		return __pbn__TaleId.HasValue;
	}

	public void ResetTaleId()
	{
		__pbn__TaleId = null;
	}
}
