using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleChooseAvatarRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6605
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "OLD_AVATAR_MISMATCH")]
		OldAvatarMismatch,
		[ProtoEnum(Name = "NEW_AVATAR_LOCKED")]
		NewAvatarLocked,
		[ProtoEnum(Name = "IN_MATCH_STATUS")]
		InMatchStatus,
		[ProtoEnum(Name = "IN_READY_STATUS")]
		InReadyStatus
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__OldAvatarId;

	private uint? __pbn__NewAvatarId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "activity_id")]
	public uint ActivityId
	{
		get
		{
			return __pbn__ActivityId.GetValueOrDefault();
		}
		set
		{
			__pbn__ActivityId = value;
		}
	}

	[ProtoMember(3, Name = "old_avatar_id")]
	public uint OldAvatarId
	{
		get
		{
			return __pbn__OldAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__OldAvatarId = value;
		}
	}

	[ProtoMember(4, Name = "new_avatar_id")]
	public uint NewAvatarId
	{
		get
		{
			return __pbn__NewAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__NewAvatarId = value;
		}
	}

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeActivityId()
	{
		return __pbn__ActivityId.HasValue;
	}

	public void ResetActivityId()
	{
		__pbn__ActivityId = null;
	}

	public bool ShouldSerializeOldAvatarId()
	{
		return __pbn__OldAvatarId.HasValue;
	}

	public void ResetOldAvatarId()
	{
		__pbn__OldAvatarId = null;
	}

	public bool ShouldSerializeNewAvatarId()
	{
		return __pbn__NewAvatarId.HasValue;
	}

	public void ResetNewAvatarId()
	{
		__pbn__NewAvatarId = null;
	}
}
