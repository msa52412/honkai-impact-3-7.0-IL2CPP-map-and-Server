using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class SetVirtualAvatarTeamRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3505
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_NOT_EXIST")]
		AvatarNotExist,
		[ProtoEnum(Name = "AVATAR_REPEATED")]
		AvatarRepeated,
		[ProtoEnum(Name = "AVATAR_EMPTY")]
		AvatarEmpty,
		[ProtoEnum(Name = "TEAM_SIZE_LIMIT")]
		TeamSizeLimit,
		[ProtoEnum(Name = "LOCK")]
		Lock
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "group_id")]
	public uint GroupId
	{
		get
		{
			return __pbn__GroupId.GetValueOrDefault();
		}
		set
		{
			__pbn__GroupId = value;
		}
	}

	[ProtoMember(3, Name = "avatar_team_list")]
	public uint[] AvatarTeamLists { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeGroupId()
	{
		return __pbn__GroupId.HasValue;
	}

	public void ResetGroupId()
	{
		__pbn__GroupId = null;
	}
}
