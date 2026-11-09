using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class VirtualTrainEvoRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 3523
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
		[ProtoEnum(Name = "NO_NEXT_AVATAR")]
		NoNextAvatar,
		[ProtoEnum(Name = "CAN_NOT_EVO")]
		CanNotEvo,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "EVO_LIMIT")]
		EvoLimit
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__GroupId;

	private uint? __pbn__EvoAvatarId;

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

	[ProtoMember(3, Name = "evo_avatar_id")]
	public uint EvoAvatarId
	{
		get
		{
			return __pbn__EvoAvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__EvoAvatarId = value;
		}
	}

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

	public bool ShouldSerializeEvoAvatarId()
	{
		return __pbn__EvoAvatarId.HasValue;
	}

	public void ResetEvoAvatarId()
	{
		__pbn__EvoAvatarId = null;
	}
}
