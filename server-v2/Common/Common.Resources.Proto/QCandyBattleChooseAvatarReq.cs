using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class QCandyBattleChooseAvatarReq : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 6604
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__ActivityId;

	private uint? __pbn__OldAvatarId;

	private uint? __pbn__NewAvatarId;

	[ProtoMember(1, Name = "activity_id")]
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

	[ProtoMember(2, Name = "old_avatar_id")]
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

	[ProtoMember(3, Name = "new_avatar_id")]
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
