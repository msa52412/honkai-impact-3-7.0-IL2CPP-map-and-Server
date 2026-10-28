using ProtoBuf;

namespace Common.Resources.Proto;

[ProtoContract]
public class AvatarArtifactLevelUpRsp : IExtensible
{
	[ProtoContract]
	public enum CmdId
	{
		[ProtoEnum(Name = "CMD_ID")]
		CmdId = 1447
	}

	[ProtoContract]
	public enum Retcode
	{
		[ProtoEnum(Name = "SUCC")]
		Succ,
		[ProtoEnum(Name = "FAIL")]
		Fail,
		[ProtoEnum(Name = "AVATAR_LEVEL_LACK")]
		AvatarLevelLack,
		[ProtoEnum(Name = "AVATAR_STAR_LACK")]
		AvatarStarLack,
		[ProtoEnum(Name = "MATERIAL_LACK")]
		MaterialLack,
		[ProtoEnum(Name = "FEATURE_CLOSED")]
		FeatureClosed
	}

	private IExtension __pbn__extensionData;

	private uint? __pbn__AvatarId;

	private uint? __pbn__ArtifactId;

	[ProtoMember(1, IsRequired = true)]
	public Retcode retcode { get; set; }

	[ProtoMember(2, Name = "avatar_id")]
	public uint AvatarId
	{
		get
		{
			return __pbn__AvatarId.GetValueOrDefault();
		}
		set
		{
			__pbn__AvatarId = value;
		}
	}

	[ProtoMember(3, Name = "artifact_id")]
	public uint ArtifactId
	{
		get
		{
			return __pbn__ArtifactId.GetValueOrDefault();
		}
		set
		{
			__pbn__ArtifactId = value;
		}
	}

	[ProtoMember(4, Name = "avatar_detail")]
	public AvatarDetailData AvatarDetail { get; set; }

	[ProtoMember(5, Name = "reward_data")]
	public RewardData RewardData { get; set; }

	IExtension IExtensible.GetExtensionObject(bool createIfMissing)
	{
		return Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);
	}

	public bool ShouldSerializeAvatarId()
	{
		return __pbn__AvatarId.HasValue;
	}

	public void ResetAvatarId()
	{
		__pbn__AvatarId = null;
	}

	public bool ShouldSerializeArtifactId()
	{
		return __pbn__ArtifactId.HasValue;
	}

	public void ResetArtifactId()
	{
		__pbn__ArtifactId = null;
	}
}
